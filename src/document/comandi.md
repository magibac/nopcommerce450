cartella

cd C:\laragon\www\nopCommerce_4.50.0_Source\src\Presentation\Nop.Web

dotnet run --project Nop.Web.csproj

https://localhost:5001/

cd C:\laragon\www\nopCommerce_4.50.0_Source\src\Presentation\Nop.Web

| Operazione                                 | Comando                                                                                                             |
| ------------------------------------------ | ------------------------------------------------------------------------------------------------------------------- |
| Ripristinare pacchetti NuGet               | `dotnet restore`                                                                                                    |
| Compilare la soluzione                     | `dotnet build`                                                                                                      |
| Compilare una solution specifica           | `dotnet build NopCommerce.sln`                                                                                      |
| Pulire file compilati                      | `dotnet clean`                                                                                                      |
| Ricompilare completamente                  | `dotnet clean && dotnet build`                                                                                      |
| Avviare NopCommerce                        | `dotnet run --project src/Presentation/Nop.Web/Nop.Web.csproj`                                                      |
| Avvio con hot reload                       | `dotnet watch run --project src/Presentation/Nop.Web/Nop.Web.csproj`                                                |
| Pubblicare in Release                      | `dotnet publish -c Release`                                                                                         |
| Pubblicare solo Nop.Web                    | `dotnet publish src/Presentation/Nop.Web/Nop.Web.csproj -c Release`                                                 |
| Verificare SDK installati                  | `dotnet --list-sdks`                                                                                                |
| Verificare versione .NET                   | `dotnet --version`                                                                                                  |
| Eliminare manualmente bin/obj (PowerShell) | `Get-ChildItem -Path . -Include bin,obj -Recurse \| Remove-Item -Recurse -Force`                                    |
| Workflow completo consigliato              | `dotnet clean` → `dotnet restore` → `dotnet build` → `dotnet run --project Nop.Web.csproj` |
