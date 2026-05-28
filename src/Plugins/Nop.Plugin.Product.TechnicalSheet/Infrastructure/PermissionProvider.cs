using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Services.Security;

namespace Nop.Plugin.Product.TechnicalSheet.Infrastructure;

/// <summary>
/// Provider dei permessi personalizzati per il plugin Schede Tecniche.
/// Definisce il permesso "ManageTechnicalSheets" associato al ruolo Amministratori.
/// </summary>
public class PermissionProvider : IPermissionProvider
{
    /// <summary>
    /// Permesso per la gestione delle schede tecniche nell'area admin.
    /// SystemName utilizzato per la persistenza nel database e per le verifiche ACL.
    /// </summary>
    public static readonly PermissionRecord ManageTechnicalSheets = new()
    {
        Name = "Admin area. Manage Product Technical Sheets",
        SystemName = "ManageTechnicalSheets",
        Category = "Catalog"
    };

    /// <summary>
    /// Restituisce l'elenco dei permessi registrati dal plugin
    /// </summary>
    /// <returns>Enumerable di PermissionRecord</returns>
    public virtual IEnumerable<PermissionRecord> GetPermissions()
    {
        return new[]
        {
            ManageTechnicalSheets
        };
    }

    /// <summary>
    /// Restituisce l'assegnazione predefinita dei permessi ai ruoli utente.
    /// Il permesso ManageTechnicalSheets viene assegnato automaticamente al ruolo Administrators.
    /// </summary>
    /// <returns>HashSet di tuple (nome ruolo, permessi)</returns>
    public virtual HashSet<(string systemRoleName, PermissionRecord[] permissions)> GetDefaultPermissions()
    {
        return new HashSet<(string, PermissionRecord[])>
        {
            (
                NopCustomerDefaults.AdministratorsRoleName,
                new[]
                {
                    ManageTechnicalSheets
                }
            )
        };
    }
}
