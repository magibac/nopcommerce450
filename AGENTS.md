# nopCommerce 4.50.0 — AGENTS.md

## Solution structure

```
src/
├── Libraries/   Nop.Core, Nop.Data, Nop.Services    (class libraries)
├── Presentation/Nop.Web, Nop.Web.Framework           (ASP.NET Core app + framework)
├── Plugins/     23 plugins                            (built individually)
└── Tests/       Nop.Tests                             (single test project)
```

Entrypoint: `src/Presentation/Nop.Web/Program.cs`

## Build & test

```powershell
# Build everything
dotnet build src/NopCommerce.sln

# Build a single plugin (build output goes to Presentation/Nop.Web/Plugins/{SystemName}/)
dotnet build src/Plugins/Nop.Plugin.Payments.CheckMoneyOrder/Nop.Plugin.Payments.CheckMoneyOrder.csproj

# Run all tests (uses SQLite in-memory, no DB setup needed)
dotnet test src/Tests/Nop.Tests/Nop.Tests.csproj

# Single test class
dotnet test src/Tests/Nop.Tests/Nop.Tests.csproj --filter FullyQualifiedName~OrderTotalCalculationServiceTests
```

CI (`.travis.yml`): `dotnet restore` → `dotnet build` → `dotnet test` on the solution.

## Plugin development

### Required files
- `plugin.json` — `SystemName` determines output folder name
- `Nop.Plugin.{Type}.{Name}.csproj` — must reference `Nop.Web.Framework.csproj`
- Main class extending `BasePlugin` + implementing the relevant interface (`IPaymentMethod`, `IWidgetPlugin`, etc.)
- `Views/` with `Layout = "_ConfigurePlugin"` for admin pages

### Install/Uninstall locale resources
Override `InstallAsync()` / `UninstallAsync()` in the plugin class:

```csharp
public override async Task InstallAsync()
{
    await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
    {
        ["Plugins.MyPlugin.MyField"] = "Display text",
    });
    await base.InstallAsync();
}

public override async Task UninstallAsync()
{
    await _localizationService.DeleteLocaleResourcesAsync("Plugins.MyPlugin");
    await base.UninstallAsync();
}
```

Keys use `Plugins.{SystemName}.{Field}` convention. `base.InstallAsync()` handles the schema migration via `[NopMigration]` attributes — do NOT create tables manually.

### DI registration
No `NopStartup.cs` needed for most plugins — services are auto-discovered via assembly scanning (`INopStartup`). Settings (`ISettings`) and event consumers (`IConsumer<>`) are auto-registered. Only register custom services manually via `INopStartup`.

### Admin controller attributes (REQUIRED)
```csharp
[AuthorizeAdmin]
[Area(AreaNames.Admin)]
[AutoValidateAntiforgeryToken]
public class MyController : BasePluginController
```

Always check permissions in every action:
```csharp
if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePaymentMethods))
    return AccessDeniedView();
```

### Build output
Every plugin `.csproj` sets:
```xml
<OutputPath>..\..\Presentation\Nop.Web\Plugins\{SystemName}\</OutputPath>
```
Views are served from `~/Plugins/{SystemName}/Views/...` (not from plugin source path).

### Model conventions
- Inherit `BaseNopModel` or `BaseNopEntityModel`
- Use `record` keyword
- Decorate with `[NopResourceDisplayName("Plugins...")]`

## Code style (enforced by `.editorconfig`)
- Allman braces (`csharp_new_line_before_open_brace = all`)
- Private fields: `_camelCase` prefix
- Interfaces: `I` prefix, PascalCase
- Async methods: `Async` suffix
- Constants: `ALL_UPPER_WITH_UNDERSCORES`
- `var` preferred when type is apparent
- No `this.` qualification
- No expression-bodied methods/constructors (only properties/indexers)
- UTF-8 BOM for `.cs` files
- 4-space indent for C#, 2-space for XML/JSON

## Testing
- **Framework:** NUnit 3 + FluentAssertions 6 + Moq 4
- **Database:** In-memory SQLite (no external DB needed)
- **Base class:** `BaseNopTest` — bootstraps full DI, installs sample data, runs migrations
- **Helper:** `TestCrud<TEntity>()` for standard CRUD entity tests
- Tests are in `src/Tests/Nop.Tests/` categorized by area (`Nop.Core.Tests/`, `Nop.Services.Tests/`, `Nop.Web.Tests/`)

## Docker
Three compose files at repo root:
- `docker-compose.yml` — SQL Server 2019
- `postgresql-docker-compose.yml` — PostgreSQL
- `mysql-docker-compose.yml` — MySQL

All use SA password `nopCommerce_db_password`.

## Plugin: Nop.Plugin.Product.TechnicalSheet (CURRENT SESSION)

This is a **WIP** plugin adding technical sheets to products. Current state:
- **Domain/Model/Service/Migration** — fully implemented
- **Controller** — updated for Razor views + AJAX delete (`Json(new { success = true/false })`)
- **Views** — `List.cshtml` + `CreateOrEdit.cshtml` implemented with AJAX delete
- **Missing:** `InstallAsync()`/`UninstallAsync()` override in `TechnicalSheetPlugin.cs` (no locale resources installed), localization keys `Admin.ProductTechnicalSheet.Fields.*` referenced but not in DB, `List.cshtml` uses hardcoded English strings instead of `@T()`
- **Dead code:** `Infrastructure/Cache/MyCacheDefaults.cs` unused
- **Comments:** `ProductTechnicalSheetService.cs` contains educational comment table (lines 8-13) that should be removed for production


### Session Initialization

After `/new`, the agent must:

1. Derive the session path deterministically from the current system date:

```txt
.agent/sessions/YYYY/Mese/YYYY-MM-DD.md
```

2. If the file does not exist:

   * Create it
   * Initialize with:

```md
## Session started
```

3. If the file already exists:

   * Append:

```md
## Session resumed
```

4. Confirm creation or update before proceeding with any task.