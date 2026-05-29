# Struttura finale plugin

Nop.Plugin.Product.TechnicalSheet/
│
├── Components/
│   └── ProductTechnicalSheetAdminViewComponent.cs
│
├── Controllers/
│   └── ProductTechnicalSheetController.cs
│
├── Domain/
│   └── ProductTechnicalSheet.cs
│
├── Infrastructure/
│   ├── NopStartup.cs
│   ├── PermissionProvider.cs
│   └── TechnicalSheetDefaults.cs
│
├── Models/
│   └── ProductTechnicalSheetModel.cs
│
├── Services/
│   ├── IProductTechnicalSheetService.cs
│   └── ProductTechnicalSheetService.cs
│
├── Views/
│   ├── _ViewImports.cshtml
│   ├── CreateOrEdit.cshtml
│   └── Shared/
│       └── Components/
│           └── ProductTechnicalSheetAdmin/
│               └── Default.cshtml
│
├── TechnicalSheetPlugin.cs
├── plugin.json
└── Nop.Plugin.Product.TechnicalSheet.csproj

---

* `Components/ProductTechnicalSheetAdminViewComponent.cs`

  * Responsabilità:

    * Renderizza il blocco “Technical Sheets” nella pagina Product Edit
    * Recupera le schede tecniche del prodotto corrente
    * Verifica i permessi ACL (non attivo — controllo commentato)
  * Dipendenze:

    * `IProductTechnicalSheetService`
    * `IPermissionService` (iniettato, non utilizzato)
    * `TechnicalSheetDefaults`
    * `AdminWidgetZones.ProductDetailsBlock`
    * `Views/Shared/Components/ProductTechnicalSheetAdmin/Default.cshtml`

---

* `Controllers/ProductTechnicalSheetController.cs`

  * Responsabilità:

    * Gestione CRUD schede tecniche
    * Gestione validazione
    * Logging operazioni CRUD
    * Toast notifications
    * Redirect verso Product Edit
  * Dipendenze:

    * `IProductTechnicalSheetService`
    * `ILocalizationService`
    * `INotificationService`
    * `ILogger`
    * `ProductTechnicalSheetModel`
    * `CreateOrEdit.cshtml`

---

* `Domain/ProductTechnicalSheet.cs`

  * Responsabilità:

    * Entity database scheda tecnica
  * Dipendenze:

    * `BaseEntity`
    * Repository nopCommerce

---

* `Infrastructure/NopStartup.cs`

  * Responsabilità:

    * Registrazione Dependency Injection
    * Registrazione services/plugin infrastructure
  * Dipendenze:

    * `IProductTechnicalSheetService`
    * `ProductTechnicalSheetService`
    * `PermissionProvider`

---

* `Infrastructure/PermissionProvider.cs`

  * Responsabilità:

    * Definizione permission custom
    * Registrazione ACL plugin (non attiva — controlli commentati)
  * Dipendenze:

    * `IPermissionProvider`
    * Security infrastructure nopCommerce

---

* `Infrastructure/TechnicalSheetDefaults.cs`

  * Responsabilità:

    * Centralizzazione costanti plugin
  * Dipendenze:

    * Nessuna runtime dependency

---

* `Models/ProductTechnicalSheetModel.cs`

  * Responsabilità:

    * ViewModel UI
    * Validation form
    * Model binding Razor
  * Dipendenze:

    * DataAnnotations
    * Razor form binding

---

* `Services/IProductTechnicalSheetService.cs`

  * Responsabilità:

    * Contratto service layer
  * Dipendenze:

    * `ProductTechnicalSheet`

---

* `Services/ProductTechnicalSheetService.cs`

  * Responsabilità:

    * Business logic
    * CRUD repository
    * Query database
  * Dipendenze:

    * `IRepository<ProductTechnicalSheet>`

---

* `Views/_ViewImports.cshtml`

  * Responsabilità:

    * Import Razor condivisi
    * Registrazione TagHelpers
  * Dipendenze:

    * `Nop.Web.Framework`
    * Razor engine

---

* `Views/CreateOrEdit.cshtml`

  * Responsabilità:

    * Form creazione/modifica scheda tecnica
    * Validation UI
    * Layout admin nopCommerce
  * Dipendenze:

    * `ProductTechnicalSheetModel`
    * TagHelpers nopCommerce
    * `ProductTechnicalSheetController`

---

* `Views/Shared/Components/ProductTechnicalSheetAdmin/Default.cshtml`

  * Responsabilità:

    * UI principale del tab/widget Technical Sheets
    * Lista schede tecniche
    * Pulsanti Add/Edit/Delete
    * AJAX delete
  * Dipendenze:

    * `ProductTechnicalSheetAdminViewComponent`
    * `ProductTechnicalSheetController`
    * `IList<ProductTechnicalSheet>`

---

* `TechnicalSheetPlugin.cs`

  * Responsabilità:

    * Entry point plugin
    * Registrazione widget
    * Install/Uninstall
    * Registrazione localizzazioni
  * Dipendenze:

    * `BasePlugin`
    * `IWidgetPlugin`
    * `ILocalizationService`
    * `TechnicalSheetDefaults`

---

* `plugin.json`

  * Responsabilità:

    * Metadata plugin nopCommerce
  * Dipendenze:

    * Plugin loader nopCommerce

---

* `Nop.Plugin.Product.TechnicalSheet.csproj`

  * Responsabilità:

    * Configurazione build plugin
    * Output plugin
    * Copia Views e asset
  * Dipendenze:

    * .NET SDK
    * `Nop.Web.Framework`
    * MSBuild nopCommerce
