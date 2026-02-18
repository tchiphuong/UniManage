using Dapper;
using System.Collections;
using System.Globalization;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Utilities;

namespace UniManage.Resource
{
    public class ResourceManager
    {
        /// <summary>
        /// CoreResource store
        /// </summary>
        private Hashtable _Table;

        /// <summary>
        /// Get resource by name
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string GetString(string resourceName, CultureInfo culture = null)
        {
            if (_Table == null || _Table.Count == 0)
            {
                SetResourceStore();
            }

            var langCode = (culture ?? Thread.CurrentThread.CurrentUICulture).Name;

            if (!_Table.ContainsKey(langCode)) return string.Empty;

            var tbResource = _Table[langCode] as Hashtable;
            if (tbResource == null) return string.Empty;

            var langData = tbResource[resourceName]?.ToString();

            if (!string.IsNullOrEmpty(langData))
            {
                return langData;
            }

            // Nếu không có trong DB thì fallback qua dịch
            langData = TranslateResource(resourceName, langCode);

            return langData ?? string.Empty;
        }

        /// <summary>
        /// Get all resources
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public Dictionary<string, string?> GetResources(CultureInfo culture = null)
        {
            if (_Table == null || _Table.Count == 0)
            {
                SetResourceStore();
            }

            var langCode = (culture ?? Thread.CurrentThread.CurrentUICulture).Name;

            if (!_Table.ContainsKey(langCode)) return new Dictionary<string, string?>();

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

            result = result.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            return result;
        }

        /// <summary>
        /// Set/re-set resource from database to store
        /// </summary>
        public void SetResourceStore()
        {
            _Table = new Hashtable();

            var lstDataResource = new List<ResourceModel>();

            using (DbContext dbContext = new DbContext())
            {
                var languageDefault = GetDefaultLanguage();
                if (!string.IsNullOrEmpty(languageDefault))
                {
                    // 2. Lấy toàn bộ resource
                    var sql = $@"
                                SELECT 
                                    language.LanguageCode   AS {nameof(ResourceModel.LanguageCode)},
                                    resource.ResourceKey    AS {nameof(ResourceModel.Name)},
                                    resource.ResourceValue  AS {nameof(ResourceModel.Value)}
                                FROM sy_languages language
                                LEFT JOIN sy_resources resource ON language.LanguageCode = resource.LanguageCode
                                ORDER BY resource.ResourceKey
                            ";

                    lstDataResource = dbContext.QueryAsync<ResourceModel>(sql, new { defaultLang = languageDefault }).GetAwaiter().GetResult().ToList();
                }
            }

            if (lstDataResource.Any())
            {
                var grpCountry = lstDataResource.GroupBy(x => x.LanguageCode).AsList();

                foreach (var country in grpCountry)
                {
                    Hashtable resourceData = new Hashtable();
                    foreach (var resource in country.ToList())
                    {
                        if (!string.IsNullOrEmpty(resource.Name))
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
            // Nếu không tìm thấy trong ngôn ngữ hiện tại, thử tìm trong ngôn ngữ mặc định
            var defaultLang = GetDefaultLanguage();
            if (!string.IsNullOrEmpty(defaultLang) && _Table.ContainsKey(defaultLang))
            {
                string defaultData = GetString(resourceName, new CultureInfo(defaultLang));

                translatedData = TranslateHelper.TranslateTextAsync(defaultData, langShortName).Result;
                if (!string.IsNullOrEmpty(translatedData))
                {
                    using (DbContext dbContext = new DbContext())
                    {
                        // Kiểm tra xem resource đã tồn tại chưa
                        string checkSql = @"
                                    SELECT COUNT(1) 
                                    FROM sy_resources 
                                    WHERE LanguageCode = @LanguageCode AND ResourceKey = @ResourceKey";

                        var exists = dbContext.ExecuteScalarAsync<int>(checkSql, new
                        {
                            LanguageCode = langShortName,
                            ResourceKey = resourceName
                        }).GetAwaiter().GetResult();

                        if (exists == 0)
                        {
                            string insertSql = @" INSERT INTO sy_resources (
                                                            Uuid,
                                                            ResourceKey,
                                                            ResourceValue,
                                                            SourceLanguage,
                                                            LanguageCode,
                                                            CreatedBy,
                                                            CreatedAt,
                                                            UpdatedBy,
                                                            UpdatedAt
                                                        )
                                                        VALUES (
                                                            @Uuid,
                                                            @ResourceKey,
                                                            @ResourceValue,
                                                            @SourceLanguage,
                                                            @LanguageCode,
                                                            @CreatedBy,
                                                            @CreatedAt,
                                                            @UpdatedBy,
                                                            @UpdatedAt
                                                        )";

                            var now = DateTime.Now;

                            dbContext.ExecuteAsync(insertSql, new
                            {
                                Uuid = Guid.NewGuid(),
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

        sealed class ResourceModel
        {
            public string LanguageCode { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}