using Dapper;
using System.Collections;
using System.Globalization;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Utilities;
using UniManage.Core.Models.Resources;

namespace UniManage.Core.Services
{
    public class ResourceManager
    {
        private Hashtable? _Table;

        public string GetString(string resourceName, CultureInfo? culture = null)
        {
            if (_Table == null || _Table.Count == 0)
            {
                SetResourceStore();
            }

            var langCode = (culture ?? Thread.CurrentThread.CurrentUICulture).Name;

            if (_Table == null || !_Table.ContainsKey(langCode)) return string.Empty;

            var tbResource = _Table[langCode] as Hashtable;
            if (tbResource == null) return string.Empty;

            var langData = tbResource[resourceName]?.ToString();

            if (!string.IsNullOrEmpty(langData))
            {
                return langData;
            }

            langData = TranslateResource(resourceName, langCode);

            return langData ?? string.Empty;
        }

        public Dictionary<string, string?> GetResources(CultureInfo? culture = null)
        {
            if (_Table == null || _Table.Count == 0)
            {
                SetResourceStore();
            }

            var langCode = (culture ?? Thread.CurrentThread.CurrentUICulture).Name;

            if (_Table == null || !_Table.ContainsKey(langCode)) return new Dictionary<string, string?>();

            string defaultLang = GetDefaultLanguage();

            if (!_Table.ContainsKey(defaultLang)) return new Dictionary<string, string?>();

            var defaultResources = _Table[defaultLang] as Hashtable;
            var currentResources = _Table[langCode] as Hashtable;

            if (defaultResources == null || currentResources == null)
                return new Dictionary<string, string?>();

            var result = new Dictionary<string, string?>();

            foreach (DictionaryEntry entry in defaultResources)
            {
                var key = entry.Key?.ToString();
                if (string.IsNullOrEmpty(key)) continue;

                var value = currentResources[key]?.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    value = TranslateResource(key, langCode);
                }

                result[key] = value;
            }

            return result.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public void SetResourceStore()
        {
            _Table = new Hashtable();
            var lstDataResource = new List<ResourceModel>();

            using (DbContext dbContext = new DbContext())
            {
                var languageDefault = GetDefaultLanguage();
                if (!string.IsNullOrEmpty(languageDefault))
                {
                    var sql = $@"
                        SELECT 
                            language.LanguageCode   AS {nameof(ResourceModel.LanguageCode)},
                            resource.ResourceKey    AS {nameof(ResourceModel.Name)},
                            resource.ResourceValue  AS {nameof(ResourceModel.Value)}
                        FROM sy_languages language
                        LEFT JOIN sy_resources resource ON language.LanguageCode = resource.LanguageCode
                        ORDER BY resource.ResourceKey";

                    lstDataResource = dbContext.QueryAsync<ResourceModel>(sql, new { defaultLang = languageDefault }).GetAwaiter().GetResult().ToList();
                }
            }

            if (lstDataResource.Any())
            {
                var grpCountry = lstDataResource.GroupBy(x => x.LanguageCode).ToList();

                foreach (var country in grpCountry)
                {
                    Hashtable resourceData = new Hashtable();
                    foreach (var resource in country.ToList())
                    {
                        if (!string.IsNullOrEmpty(resource.Name) && !resourceData.ContainsKey(resource.Name))
                        {
                            resourceData.Add(resource.Name, resource.Value);
                        }
                    }

                    _Table.Add(country.Key, resourceData);
                }
            }
        }

        public static string GetDefaultLanguage()
        {
            using (DbContext dbContext = new DbContext())
            {
                return dbContext.QueryFirstOrDefaultAsync<string>("SELECT LanguageCode FROM sy_languages WHERE IsDefault = 1").GetAwaiter().GetResult() ?? string.Empty;
            }
        }

        private string TranslateResource(string resourceName, string langShortName)
        {
            string translatedData = string.Empty;
            var defaultLang = GetDefaultLanguage();
            
            if (!string.IsNullOrEmpty(defaultLang) && _Table != null && _Table.ContainsKey(defaultLang))
            {
                string defaultData = GetString(resourceName, new CultureInfo(defaultLang));
                translatedData = TranslateHelper.TranslateTextAsync(defaultData, langShortName).Result;
                
                if (!string.IsNullOrEmpty(translatedData))
                {
                    using (DbContext dbContext = new DbContext())
                    {
                        string checkSql = "SELECT COUNT(1) FROM sy_resources WHERE LanguageCode = @LanguageCode AND ResourceKey = @ResourceKey";
                        var exists = dbContext.ExecuteScalarAsync<int>(checkSql, new { LanguageCode = langShortName, ResourceKey = resourceName }).GetAwaiter().GetResult();

                        if (exists == 0)
                        {
                            string insertSql = @"
                                INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                                VALUES (@ResourceKey, @ResourceValue, @SourceLanguage, @LanguageCode, @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt)";

                            var now = DateTimeHelper.Now;
                            dbContext.ExecuteAsync(insertSql, new
                            {
                                ResourceKey = resourceName,
                                ResourceValue = translatedData,
                                SourceLanguage = defaultLang,
                                LanguageCode = langShortName,
                                CreatedBy = ApplicationConstants.Defaults.SystemUser,
                                CreatedAt = now,
                                UpdatedBy = ApplicationConstants.Defaults.SystemUser,
                                UpdatedAt = now
                            }).GetAwaiter().GetResult();
                        }
                    }
                }
            }
            return translatedData;
        }
    }
}
