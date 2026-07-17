using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UniManage.Tools.CodeGen
{
    public class ColumnInfo
    {
        public string Name { get; set; } = string.Empty;
        public string SqlType { get; set; } = string.Empty;
        public int MaxLength { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }

        public string CSharpType
        {
            get
            {
                string type = SqlType.ToLower() switch
                {
                    "bigint" => "long",
                    "int" => "int",
                    "smallint" => "short",
                    "tinyint" => "byte",
                    "bit" => "bool",
                    "decimal" or "numeric" or "money" or "smallmoney" => "decimal",
                    "float" => "double",
                    "real" => "float",
                    "date" or "datetime" or "datetime2" or "smalldatetime" => "DateTime",
                    "uniqueidentifier" => "Guid",
                    "time" => "TimeSpan",
                    "timestamp" or "rowversion" => "byte[]",
                    _ => "string"
                };

                // Add nullable modifier for value types if not a string
                if (IsNullable && type != "string" && type != "byte[]")
                {
                    type += "?";
                }

                // Reference types like string and byte[] are nullable in C# 8+ if enabled
                if (IsNullable && (type == "string" || type == "byte[]"))
                {
                    type += "?";
                }

                return type;
            }
        }
    }

    public static class DatabaseHelper
    {
        public static string GetConnectionString(string backendRoot)
        {
            string appSettingsPath = Path.Combine(backendRoot, "src", "03.Apps", "UniManage.WebApi", "appsettings.Development.json");
            
            if (!File.Exists(appSettingsPath))
            {
                throw new FileNotFoundException($"Cannot find {appSettingsPath}. Ensure you have appsettings.Development.json in the WebApi project.");
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath);
            
            var config = builder.Build();
            var dbConfig = config.GetSection("Database");

            string server = dbConfig["Server"] ?? "localhost";
            string database = dbConfig["DefaultDatabase"] ?? "UniManage";
            string userId = dbConfig["UserId"] ?? "sa";
            string password = dbConfig["Password"] ?? "";
            bool trustCert = bool.Parse(dbConfig["TrustServerCertificate"] ?? "true");

            return $"Server={server};Database={database};User Id={userId};Password={password};TrustServerCertificate={trustCert};";
        }

        public static List<string> GetTables(string connectionString)
        {
            var tables = new List<string>();
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand(@"
                SELECT TABLE_NAME 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_TYPE = 'BASE TABLE' 
                AND TABLE_NAME NOT LIKE 'sy_%' 
                ORDER BY TABLE_NAME", conn);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
            return tables;
        }

        public static List<string> GetModules(string connectionString)
        {
            var modules = new List<string>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var cmd = new SqlCommand("SELECT Code FROM sy_modules WHERE IsActive = 1 ORDER BY Code", conn);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // To Title Case since the DB stores it as UPPERCASE usually
                    string code = reader.GetString(0);
                    if (code.Length > 0)
                    {
                        code = char.ToUpper(code[0]) + code.Substring(1).ToLower();
                    }
                    modules.Add(code);
                }
            }
            catch
            {
                // Bỏ qua lỗi nếu bảng chưa có
            }
            return modules;
        }

        public static List<string> GetFunctionGroups(string connectionString)
        {
            var groups = new List<string>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                using var cmd = new SqlCommand("SELECT Code FROM sy_function_groups WHERE IsActive = 1 ORDER BY Code", conn);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Convert to PascalCase/TitleCase
                    string code = reader.GetString(0);
                    if (code.Length > 0)
                    {
                        code = char.ToUpper(code[0]) + code.Substring(1).ToLower();
                    }
                    groups.Add(code);
                }
            }
            catch
            {
                // Fallback if sy_function_groups is not found, try from sy_functions
                try 
                {
                    using var conn = new SqlConnection(connectionString);
                    conn.Open();
                    using var cmd = new SqlCommand("SELECT DISTINCT GroupCode FROM sy_functions WHERE GroupCode IS NOT NULL AND GroupCode <> '' ORDER BY GroupCode", conn);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);
                        if (code.Length > 0)
                        {
                            code = char.ToUpper(code[0]) + code.Substring(1).ToLower();
                        }
                        groups.Add(code);
                    }
                }
                catch { }
            }
            return groups;
        }

        public static List<ColumnInfo> GetColumns(string connectionString, string tableName)
        {
            var columns = new List<ColumnInfo>();
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            // Lấy cột và kiểm tra cột nào là Primary Key
            using var cmd = new SqlCommand(@"
                SELECT 
                    c.COLUMN_NAME, 
                    c.DATA_TYPE, 
                    c.CHARACTER_MAXIMUM_LENGTH, 
                    c.IS_NULLABLE,
                    CASE WHEN kcu.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IS_PRIMARY_KEY
                FROM INFORMATION_SCHEMA.COLUMNS c
                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
                    ON c.TABLE_NAME = kcu.TABLE_NAME 
                    AND c.COLUMN_NAME = kcu.COLUMN_NAME 
                    AND OBJECTPROPERTY(OBJECT_ID(kcu.CONSTRAINT_SCHEMA + '.' + QUOTENAME(kcu.CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
                WHERE c.TABLE_NAME = @TableName
                ORDER BY c.ORDINAL_POSITION", conn);
            
            cmd.Parameters.AddWithValue("@TableName", tableName);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var col = new ColumnInfo
                {
                    Name = reader.GetString(0),
                    SqlType = reader.GetString(1),
                    MaxLength = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                    IsNullable = reader.GetString(3) == "YES",
                    IsPrimaryKey = reader.GetInt32(4) == 1
                };
                
                // Bỏ qua các cột audit/system chung
                if (col.Name == "CreatedBy" || col.Name == "CreatedAt" || 
                    col.Name == "UpdatedBy" || col.Name == "UpdatedAt" || 
                    col.Name == "DataRowVersion") 
                {
                    continue;
                }

                columns.Add(col);
            }
            return columns;
        }

        public static void InsertResourceIfMissing(string connectionString, string key, string value)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            // Check exists
            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_resources WHERE ResourceKey = @Key AND LanguageCode = 'vi-VN'", conn);
            checkCmd.Parameters.AddWithValue("@Key", key);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_resources]
                        ([ResourceKey]
                        ,[ResourceValue]
                        ,[SourceLanguage]
                        ,[LanguageCode]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@Key
                        ,@Value
                        ,'vi-VN'
                        ,'vi-VN'
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@Key", key);
                insertCmd.Parameters.AddWithValue("@Value", value);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertFunctionMappingIfMissing(string connectionString, string actionCode, string functionCode)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_function_mapping WHERE ActionCode = @ActionCode AND FunctionCode = @FunctionCode", conn);
            checkCmd.Parameters.AddWithValue("@ActionCode", actionCode);
            checkCmd.Parameters.AddWithValue("@FunctionCode", functionCode);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_function_mapping]
                        ([ActionCode]
                        ,[FunctionCode]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@ActionCode
                        ,@FunctionCode
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@ActionCode", actionCode);
                insertCmd.Parameters.AddWithValue("@FunctionCode", functionCode);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertFunctionIfMissing(string connectionString, string code, string resourceKey, string groupCode)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_functions WHERE Code = @Code", conn);
            checkCmd.Parameters.AddWithValue("@Code", code);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_functions]
                        ([Code]
                        ,[ResourceKey]
                        ,[GroupCode]
                        ,[IsActive]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@Code
                        ,@ResourceKey
                        ,@GroupCode
                        ,1
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                insertCmd.Parameters.AddWithValue("@Code", code);
                insertCmd.Parameters.AddWithValue("@ResourceKey", resourceKey);
                insertCmd.Parameters.AddWithValue("@GroupCode", groupCode);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertModuleIfMissing(string connectionString, string code, string resourceKey)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_modules WHERE Code = @Code", conn);
            checkCmd.Parameters.AddWithValue("@Code", code);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_modules]
                        ([Code]
                        ,[ResourceKey]
                        ,[IsActive]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@Code
                        ,@ResourceKey
                        ,1
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@Code", code);
                insertCmd.Parameters.AddWithValue("@ResourceKey", resourceKey);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertFunctionGroupIfMissing(string connectionString, string code, string moduleCode, string resourceKey)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_function_groups WHERE Code = @Code", conn);
            checkCmd.Parameters.AddWithValue("@Code", code);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_function_groups]
                        ([Code]
                        ,[ModuleCode]
                        ,[ResourceKey]
                        ,[IsActive]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@Code
                        ,@ModuleCode
                        ,@ResourceKey
                        ,1
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@Code", code);
                insertCmd.Parameters.AddWithValue("@ModuleCode", moduleCode);
                insertCmd.Parameters.AddWithValue("@ResourceKey", resourceKey);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertActionIfMissing(string connectionString, string code, string resourceKey)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_actions WHERE Code = @Code", conn);
            checkCmd.Parameters.AddWithValue("@Code", code);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_actions]
                        ([Code]
                        ,[ResourceKey]
                        ,[IsActive]
                        ,[Sort]
                        ,[Icon]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@Code
                        ,@ResourceKey
                        ,1
                        ,0
                        ,''
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@Code", code);
                insertCmd.Parameters.AddWithValue("@ResourceKey", resourceKey);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertMenuIfMissing(string connectionString, string code, string functionCode, string parentCode, string resourceKey, string url)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_menus WHERE Code = @Code", conn);
            checkCmd.Parameters.AddWithValue("@Code", code);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_menus]
                        ([Code]
                        ,[FunctionCode]
                        ,[ParentCode]
                        ,[ResourceKey]
                        ,[Url]
                        ,[Icon]
                        ,[CreatedBy]
                        ,[CreatedAt]
                        ,[UpdatedBy]
                        ,[UpdatedAt])
                    VALUES
                        (@Code
                        ,@FunctionCode
                        ,@ParentCode
                        ,@ResourceKey
                        ,@Url
                        ,''
                        ,'system'
                        ,GETDATE()
                        ,'system'
                        ,GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@Code", code);
                insertCmd.Parameters.AddWithValue("@FunctionCode", string.IsNullOrEmpty(functionCode) ? DBNull.Value : (object)functionCode);
                insertCmd.Parameters.AddWithValue("@ParentCode", string.IsNullOrEmpty(parentCode) ? DBNull.Value : (object)parentCode);
                insertCmd.Parameters.AddWithValue("@ResourceKey", resourceKey);
                insertCmd.Parameters.AddWithValue("@Url", url);
                insertCmd.ExecuteNonQuery();
            }
        }

        public static void InsertRolePermissionIfMissing(string connectionString, string roleCode, string functionCode, string actionCode)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var checkCmd = new SqlCommand("SELECT COUNT(1) FROM sy_role_permissions WHERE RoleCode = @RoleCode AND FunctionCode = @FunctionCode AND ActionCode = @ActionCode", conn);
            checkCmd.Parameters.AddWithValue("@RoleCode", roleCode);
            checkCmd.Parameters.AddWithValue("@FunctionCode", functionCode);
            checkCmd.Parameters.AddWithValue("@ActionCode", actionCode);
            int exists = (int)checkCmd.ExecuteScalar();

            if (exists == 0)
            {
                using var insertCmd = new SqlCommand(@"
                    INSERT INTO [dbo].[sy_role_permissions]
                        ([RoleCode], [FunctionCode], [ActionCode], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt])
                    VALUES
                        (@RoleCode, @FunctionCode, @ActionCode, 'system', GETDATE(), 'system', GETDATE())", conn);
                
                insertCmd.Parameters.AddWithValue("@RoleCode", roleCode);
                insertCmd.Parameters.AddWithValue("@FunctionCode", functionCode);
                insertCmd.Parameters.AddWithValue("@ActionCode", actionCode);
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
