using System;
using System.IO;

namespace UniManage.Tools.CodeGen
{
    public class CodeGenerator
    {
        private readonly string _backendPath;
        private readonly string _connectionString;
        private readonly string _moduleName;
        private readonly string _functionGroup;
        private readonly string _entityName;
        private readonly string _tableName;
        private readonly string _routeName;
        private readonly string _screenCode;
        private readonly List<ColumnInfo> _dbColumns;
        private readonly List<string> _gridCols;
        private readonly List<string> _searchCols;
        private readonly List<string> _editCols;

        private string ModuleAppPath => Path.Combine(_backendPath, "src", "02.Modules", _moduleName, $"UniManage.Modules.{_moduleName}.Application");
        private string ControllersPath => Path.Combine(_backendPath, "src", "03.Apps", "UniManage.WebApi", "Controllers", _moduleName);

        public CodeGenerator(
            string backendPath, 
            string connectionString,
            string moduleName, 
            string functionGroup,
            string entityName, 
            string tableName, 
            string routeName,
            string screenCode,
            List<ColumnInfo> dbColumns,
            List<string> gridCols,
            List<string> searchCols,
            List<string> editCols)
        {
            _backendPath = backendPath;
            _connectionString = connectionString;
            _moduleName = moduleName;
            _functionGroup = functionGroup;
            _entityName = entityName;
            _tableName = tableName;
            _routeName = routeName;
            _screenCode = screenCode;
            _dbColumns = dbColumns;
            _gridCols = gridCols;
            _searchCols = searchCols;
            _editCols = editCols;

            string appPath = ModuleAppPath;
            string commandsDir = Path.Combine(appPath, "Commands", _entityName);
            string queriesDir = Path.Combine(appPath, "Queries", _entityName);
            string modelsDir = Path.Combine(appPath, "Models", _entityName);
            string validatorsDir = Path.Combine(appPath, "Validators", _entityName);

            // Ensure directories exist
            Directory.CreateDirectory(commandsDir);
            Directory.CreateDirectory(queriesDir);
            Directory.CreateDirectory(modelsDir);
            Directory.CreateDirectory(validatorsDir);
            Directory.CreateDirectory(ControllersPath);
        }

        private string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.ToUpper() == str) return str.ToLowerInvariant();
            if (str.Length == 1) return str.ToLowerInvariant();
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        private string ToSnakeCase(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLowerInvariant();
        }

        public void GenerateCrud()
        {
            string moduleCamel = ToCamelCase(_moduleName);
            string funcGroupCamel = _functionGroup.ToLowerInvariant();
            string screenCamel = ToCamelCase(_screenCode);

            string groupCodeUpper = _functionGroup.ToUpperInvariant();
            
            // Legacy Function Code: MS_UNIT (from MsUnit)
            string functionCode = ToSnakeCase(_screenCode).ToUpperInvariant();

            // 1. Resource Seeding for Labels (Chuẩn: screenCode.lbl.fieldName -> camelCase)
            foreach (var col in _dbColumns)
            {
                string labelKey = $"{screenCamel}.lbl.{ToCamelCase(col.Name)}";
                DatabaseHelper.InsertResourceIfMissing(_connectionString, labelKey, labelKey);
            }

            // 2. Module, Function Group & Function Seeding
            string moduleCodeUpper = _moduleName.ToUpperInvariant();
            string moduleResKey = $"module.{moduleCamel}";
            
            string groupResKey = $"func.group.{funcGroupCamel}";
            string titleResKey = $"func.{functionCode.Replace("_", ".").ToLowerInvariant()}";
            
            DatabaseHelper.InsertResourceIfMissing(_connectionString, moduleResKey, moduleResKey);
            DatabaseHelper.InsertResourceIfMissing(_connectionString, groupResKey, groupResKey);
            DatabaseHelper.InsertResourceIfMissing(_connectionString, titleResKey, titleResKey);

            // Module record in sy_modules
            DatabaseHelper.InsertModuleIfMissing(_connectionString, moduleCodeUpper, moduleResKey);

            // Function Group record in sy_function_groups
            DatabaseHelper.InsertFunctionGroupIfMissing(_connectionString, groupCodeUpper, moduleCodeUpper, groupResKey);

            // Function record (Code = functionCode)
            DatabaseHelper.InsertFunctionIfMissing(_connectionString, functionCode, titleResKey, groupCodeUpper);

            // 3. Menu Seeding (Legacy: GRP_XXX cho Group, MNU_XXX cho Screen)
            string menuL2Code = $"GRP_{groupCodeUpper}";
            DatabaseHelper.InsertMenuIfMissing(_connectionString, menuL2Code, DBNull.Value.ToString(), DBNull.Value.ToString(), groupResKey, ""); // Root group menu

            string menuL3Code = $"MNU_{functionCode}";
            DatabaseHelper.InsertMenuIfMissing(_connectionString, menuL3Code, functionCode, menuL2Code, titleResKey, $"/{_routeName}");

            // 4. Action Seeding (Global Action Codes: CREATE, UPDATE, DELETE, VIEW)
            var baseActions = new Dictionary<string, string> 
            { 
                { "CREATE", "action.create" },
                { "UPDATE", "action.update" },
                { "DELETE", "action.delete" },
                { "VIEW", "action.view" }
            };
            
            foreach (var action in baseActions)
            {
                string actionCode = action.Key;
                string resourceKey = action.Value;
                
                DatabaseHelper.InsertResourceIfMissing(_connectionString, resourceKey, resourceKey);
                DatabaseHelper.InsertActionIfMissing(_connectionString, actionCode, resourceKey);
                DatabaseHelper.InsertFunctionMappingIfMissing(_connectionString, actionCode, functionCode);
                DatabaseHelper.InsertRolePermissionIfMissing(_connectionString, "admin", functionCode, actionCode);
            }

            // 1. Models
            WriteFile(Path.Combine(ModuleAppPath, "Models", _entityName, $"{_entityName}Models.cs"), Templates.GetModelsTemplate(_moduleName, _entityName, _dbColumns, _gridCols));

            // 2. Commands
            WriteFile(Path.Combine(ModuleAppPath, "Commands", _entityName, $"Create{_entityName}Command.cs"), Templates.GetCreateCommandTemplate(_moduleName, _entityName, _tableName, _screenCode, _dbColumns, _editCols));
            WriteFile(Path.Combine(ModuleAppPath, "Commands", _entityName, $"Update{_entityName}Command.cs"), Templates.GetUpdateCommandTemplate(_moduleName, _entityName, _tableName, _screenCode, _dbColumns, _editCols));
            WriteFile(Path.Combine(ModuleAppPath, "Commands", _entityName, $"Delete{_entityName}Command.cs"), Templates.GetDeleteCommandTemplate(_moduleName, _entityName, _tableName, _screenCode));

            // Generate Validators (All in one file)
            WriteFile(Path.Combine(ModuleAppPath, "Validators", _entityName, $"{_entityName}Validators.cs"), Templates.GetValidatorsTemplate(_moduleName, _entityName, _screenCode, _dbColumns, _editCols));

            // 3. Queries
            WriteFile(Path.Combine(ModuleAppPath, "Queries", _entityName, $"Get{_entityName}ListQuery.cs"), Templates.GetListQueryTemplate(_moduleName, _entityName, _tableName, _dbColumns, _gridCols, _searchCols));
            WriteFile(Path.Combine(ModuleAppPath, "Queries", _entityName, $"Get{_entityName}ByIdQuery.cs"), Templates.GetByIdQueryTemplate(_moduleName, _entityName, _tableName, _screenCode, _dbColumns));

            // 4. Controller
            WriteFile(Path.Combine(ControllersPath, $"{_entityName}sController.cs"), Templates.GetControllerTemplate(_moduleName, _entityName, _routeName, functionCode));
        }

        public void GenerateExport()
        {
            string functionCode = ToSnakeCase(_screenCode).ToUpperInvariant();

            string actionCode = "EXPORT";
            string resourceKey = $"action.export";
            DatabaseHelper.InsertResourceIfMissing(_connectionString, resourceKey, resourceKey);
            DatabaseHelper.InsertActionIfMissing(_connectionString, actionCode, resourceKey);
            DatabaseHelper.InsertFunctionMappingIfMissing(_connectionString, actionCode, functionCode);
            DatabaseHelper.InsertRolePermissionIfMissing(_connectionString, "admin", functionCode, actionCode);
            
            WriteFile(Path.Combine(ModuleAppPath, "Queries", _entityName, $"Export{_entityName}Query.cs"), Templates.GetExportQueryTemplate(_moduleName, _entityName, _tableName, _dbColumns, _gridCols, _searchCols));
        }

        public void GenerateImport()
        {
            string functionCode = ToSnakeCase(_screenCode).ToUpperInvariant();

            string actionCode = "IMPORT";
            string resourceKey = $"action.import";
            DatabaseHelper.InsertResourceIfMissing(_connectionString, resourceKey, resourceKey);
            DatabaseHelper.InsertActionIfMissing(_connectionString, actionCode, resourceKey);
            DatabaseHelper.InsertFunctionMappingIfMissing(_connectionString, actionCode, functionCode);
            DatabaseHelper.InsertRolePermissionIfMissing(_connectionString, "admin", functionCode, actionCode);
            
            WriteFile(Path.Combine(ModuleAppPath, "Commands", _entityName, $"Import{_entityName}Command.cs"), Templates.GetImportCommandTemplate(_moduleName, _entityName, _tableName, _screenCode, _dbColumns, _editCols));
        }

        private void WriteFile(string path, string content)
        {
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            bool exists = File.Exists(path);
            File.WriteAllText(path, content);
            
            if (exists)
            {
                Console.WriteLine($"  Overwritten: {Path.GetFileName(path)}");
            }
            else
            {
                Console.WriteLine($"  Created: {Path.GetFileName(path)}");
            }
        }
    }
}
