using Spectre.Console;

namespace UniManage.Tools.CodeGen
{
    /// <summary>
    /// 
    /// </summary>
    static class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("UniManage Gen").Centered().Color(Color.Blue));
            AnsiConsole.MarkupLine("[bold]Code Generator (CRUD + Export/Import) for UniManage[/]");
            AnsiConsole.WriteLine();

            string? backendPath = GetBackendRoot();
            if (string.IsNullOrEmpty(backendPath))
            {
                AnsiConsole.MarkupLine("[red]ERROR: Could not find UniManage backend directory![/]");
                return;
            }

            string connectionString;
            try 
            {
                connectionString = DatabaseHelper.GetConnectionString(backendPath);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]ERROR loading DB Config: {ex.Message}[/]");
                return;
            }

            int step = 1;
            string moduleName = "";
            string functionGroup = "";
            string mode = "";
            string tableName = "";
            List<ColumnInfo> dbColumns = new();
            List<string> colNames = new();
            List<string> gridCols = new();
            List<string> searchCols = new();
            List<string> editCols = new();
            string entityName = "";
            string routeName = "";
            string screenCode = "";
            List<string> options = new();

            while (step <= 8)
            {
                if (step == 1)
                {
                    // 1. Module Name
                    var modulesPath = Path.Combine(backendPath, "src", "02.Modules");
                    var fileModules = Directory.Exists(modulesPath) 
                        ? Directory.GetDirectories(modulesPath).Select(d => Path.GetFileName(d)).ToList() 
                        : new List<string>();
                        
                    var dbModules = DatabaseHelper.GetModules(connectionString);
                    var existingModules = fileModules.Union(dbModules).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                    existingModules.Add("+ Create New Module");

                    var moduleSelection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]Module[/]:")
                            .PageSize(10)
                            .EnableSearch()
                            .AddChoices(existingModules));

                    moduleName = moduleSelection;
                    if (moduleName == "+ Create New Module")
                    {
                        moduleName = AnsiConsole.Ask<string>("Enter [green]New Module[/] name (type '<' to go back):").Trim();
                        if (moduleName == "<") { continue; }
                    }
                    step++;
                }
                else if (step == 2)
                {
                    // 1.5 Function Group Name
                    var existingGroups = DatabaseHelper.GetFunctionGroups(connectionString);
                    existingGroups.Insert(0, "[grey]<-- Back[/]");
                    existingGroups.Add("+ Create New Group");

                    var groupSelection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]Function Group[/]:")
                            .PageSize(10)
                            .EnableSearch()
                            .AddChoices(existingGroups));

                    if (groupSelection == "[grey]<-- Back[/]") { step--; continue; }

                    functionGroup = groupSelection;
                    if (functionGroup == "+ Create New Group")
                    {
                        functionGroup = AnsiConsole.Ask<string>("Enter [green]New Function Group[/] name (type '<' to go back):").Trim();
                        if (functionGroup == "<") { continue; }
                    }
                    step++;
                }
                else if (step == 3)
                {
                    // 2. Choose how to input table
                    var modeChoices = new List<string> { "[grey]<-- Back[/]", "Select from Database list", "Enter table name manually", "Skip table mapping (Empty Entity)" };
                    mode = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Database Table mapping:")
                            .AddChoices(modeChoices));

                    if (mode == "[grey]<-- Back[/]") { step--; continue; }

                    if (mode == "Skip table mapping (Empty Entity)")
                    {
                        tableName = AnsiConsole.Ask<string>("Enter [green]Table Name[/] for SQL (or leave empty, type '<' to go back):", "").Trim();
                        if (tableName == "<") { continue; }
                        dbColumns = new();
                        colNames = new();
                    }
                    else
                    {
                        if (mode == "Enter table name manually")
                        {
                            tableName = AnsiConsole.Ask<string>("Enter [green]Database Table[/] name (type '<' to go back):").Trim();
                            if (tableName == "<") { continue; }
                        }
                        else
                        {
                            List<string> dbTables;
                            try
                            {
                                dbTables = DatabaseHelper.GetTables(connectionString);
                            }
                            catch (Exception ex)
                            {
                                AnsiConsole.MarkupLine($"[red]ERROR connecting to Database: {ex.Message}[/]");
                                return;
                            }

                            dbTables.Insert(0, "[grey]<-- Back[/]");
                            tableName = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("Select the [green]Database Table[/]:")
                                    .PageSize(10)
                                    .EnableSearch()
                                    .AddChoices(dbTables));
                            
                            if (tableName == "[grey]<-- Back[/]") { continue; }
                        }

                        // 4. Load Columns
                        dbColumns = DatabaseHelper.GetColumns(connectionString, tableName);
                        colNames = dbColumns.Where(c => !c.IsPrimaryKey).Select(c => c.Name).ToList();

                        if (colNames.Any())
                        {
                            // 5. Select Grid Columns
                            gridCols = AnsiConsole.Prompt(
                                new MultiSelectionPrompt<string>()
                                    .Title("Select columns for [green]Grid/List[/]:")
                                    .NotRequired()
                                    .PageSize(10)
                                    .AddChoices(colNames));
                            
                            // 6. Select Search Columns
                            searchCols = AnsiConsole.Prompt(
                                new MultiSelectionPrompt<string>()
                                    .Title("Select columns for [green]Search[/]:")
                                    .NotRequired()
                                    .PageSize(10)
                                    .AddChoices(colNames));

                            // 7. Select Edit Columns
                            editCols = AnsiConsole.Prompt(
                                new MultiSelectionPrompt<string>()
                                    .Title("Select columns for [green]Create/Update[/]:")
                                    .NotRequired()
                                    .PageSize(10)
                                    .AddChoices(colNames));
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]No columns found or table does not exist. Skipping column selection.[/]");
                        }
                    }
                    step++;
                }
                else if (step == 4)
                {
                    // Keep the full table name for default suggestions (do not strip prefix)
                    string singularName = tableName;
                    if (tableName.EndsWith("ies"))
                    {
                        singularName = tableName.Substring(0, tableName.Length - 3) + "y";
                    }
                    else if (tableName.EndsWith("sses"))
                    {
                        singularName = tableName.Substring(0, tableName.Length - 2);
                    }
                    else if (tableName.EndsWith("s") && !tableName.EndsWith("ss"))
                    {
                        singularName = tableName.Substring(0, tableName.Length - 1);
                    }
                    
                    string modulePrefix = "";
                    if (!string.IsNullOrEmpty(moduleName))
                    {
                        if (moduleName.Equals("MasterData", StringComparison.OrdinalIgnoreCase)) modulePrefix = "Ms";
                        else if (moduleName.Equals("System", StringComparison.OrdinalIgnoreCase)) modulePrefix = "Sy";
                        else modulePrefix = moduleName.Substring(0, Math.Min(2, moduleName.Length));
                        
                        modulePrefix = char.ToUpper(modulePrefix[0]) + (modulePrefix.Length > 1 ? char.ToLower(modulePrefix[1]).ToString() : "");
                    }

                    string defaultEntity = string.IsNullOrEmpty(singularName) ? $"{modulePrefix}NewEntity" : string.Join("", singularName.Split('_').Select(w => w.Length > 0 ? char.ToUpper(w[0]) + w.Substring(1) : ""));
                    if (!string.IsNullOrEmpty(entityName) && entityName != "<") defaultEntity = entityName;

                    // 8. Entity Name
                    var input = AnsiConsole.Ask<string>($"Enter [green]Entity Name[/] (type '<' to go back):", defaultEntity).Trim();
                    if (input == "<") { step--; continue; }
                    entityName = input;
                    step++;
                }
                else if (step == 5)
                {
                    string kebabEntity = string.Concat(entityName.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString())).ToLowerInvariant();
                    
                    string pluralRoute = kebabEntity;
                    if (kebabEntity.EndsWith("y") && kebabEntity.Length > 1 && !"aeiou".Contains(kebabEntity[kebabEntity.Length - 2]))
                    {
                        pluralRoute = kebabEntity.Substring(0, kebabEntity.Length - 1) + "ies";
                    }
                    else if (kebabEntity.EndsWith("s") || kebabEntity.EndsWith("sh") || kebabEntity.EndsWith("ch") || kebabEntity.EndsWith("x") || kebabEntity.EndsWith("z"))
                    {
                        pluralRoute = kebabEntity + "es";
                    }
                    else
                    {
                        pluralRoute = kebabEntity + "s";
                    }

                    // Convert the first hyphen to a slash for module prefixes (e.g. ms-companies -> ms/companies)
                    int firstHyphen = pluralRoute.IndexOf('-');
                    if (firstHyphen > 0)
                    {
                        pluralRoute = pluralRoute.Substring(0, firstHyphen) + "/" + pluralRoute.Substring(firstHyphen + 1);
                    }

                    string defaultRoute = string.IsNullOrEmpty(tableName) ? pluralRoute : tableName.Replace("_", "/").ToLower();
                    if (!string.IsNullOrEmpty(routeName) && routeName != "<") defaultRoute = routeName;
                    
                    // 9. Enter Route Name
                    var input = AnsiConsole.Ask<string>($"Enter [green]API Route Name[/] (type '<' to go back):", defaultRoute).Trim();
                    if (input == "<") { step--; continue; }
                    routeName = input;
                    step++;
                }
                else if (step == 6)
                {
                    string defaultScreenCode = entityName;
                    if (!string.IsNullOrEmpty(screenCode) && screenCode != "<") defaultScreenCode = screenCode;

                    // 10. Enter Screen Code
                    var input = AnsiConsole.Ask<string>($"Enter [green]Screen Code[/] for Resources (type '<' to go back):", defaultScreenCode).Trim();
                    if (input == "<") { step--; continue; }
                    screenCode = input;
                    step++;
                }
                else if (step == 7)
                {
                    // 11. Options
                    var opts = new List<string> {
                        "[grey]<-- Back[/]",
                        "CRUD (Create, Update, Delete, GetList, GetById)",
                        "Export Excel",
                        "Import Excel"
                    };

                    var selectedOpts = AnsiConsole.Prompt(
                        new MultiSelectionPrompt<string>()
                            .Title("Select features to generate:")
                            .NotRequired() 
                            .PageSize(10)
                            .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to accept)[/]")
                            .AddChoices(opts)
                            .Select("CRUD (Create, Update, Delete, GetList, GetById)")
                    );

                    if (selectedOpts.Contains("[grey]<-- Back[/]")) { step--; continue; }

                    options = selectedOpts;
                    step++;
                }
                else if (step == 8)
                {
                    bool generateCrud = options.Contains("CRUD (Create, Update, Delete, GetList, GetById)");
                    bool generateExport = options.Contains("Export Excel");
                    bool generateImport = options.Contains("Import Excel");

                    AnsiConsole.MarkupLine($"\nPreparing to generate code for [blue]{moduleName}.{entityName}[/]...");

                    try
                    {
                        var generator = new CodeGenerator(backendPath, connectionString, moduleName, functionGroup, entityName, tableName, routeName, screenCode, dbColumns, gridCols, searchCols, editCols);
                        
                        if (generateCrud)
                        {
                            generator.GenerateCrud();
                            AnsiConsole.MarkupLine("[green]CRUD classes generated successfully![/]");
                        }

                        if (generateExport)
                        {
                            generator.GenerateExport();
                            AnsiConsole.MarkupLine("[green]Export feature generated successfully![/]");
                        }

                        if (generateImport)
                        {
                            generator.GenerateImport();
                            AnsiConsole.MarkupLine("[green]Import feature generated successfully![/]");
                        }

                        AnsiConsole.MarkupLine($"[blue]Seed resources to sy_resources table using screen code '{screenCode}' successfully![/]");
                        AnsiConsole.MarkupLine("\n[bold green]Done![/] Please check the generated files and register DI if necessary.");
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.WriteException(ex);
                    }

                    break; // Exit the state machine
                }
            }
        }

        static string? GetBackendRoot()
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (dir != null)
            {
                if (dir.Name.Equals("backend", StringComparison.OrdinalIgnoreCase) && dir.GetDirectories("src").Length > 0)
                {
                    return dir.FullName;
                }
                dir = dir.Parent;
            }
            return null;
        }
    }
}
