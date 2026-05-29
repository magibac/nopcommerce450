Quando modifichi un plugin in nopCommerce, l'ordine è:

1. Modifica il codice sorgente — nei file del plugin sotto src/Plugins/Nop.Plugin.{Tipo}.{Nome}/

2. Ricostruisci il plugin — dotnet build src/Plugins/Nop.Plugin.Product.TechnicalSheet/Nop.Plugin.Product.TechnicalSheet.csproj (produce output in Presentation/Nop.Web/Plugins/{SystemName}/)

3. Riavvia l'app — arresta e riavvia il sito (es. Ctrl+C → dotnet run in Presentation/Nop.Web/)

4. Se hai modificato InstallAsync() / UninstallAsync() (locale resources, migrazioni DB) → vai in Admin → Configuration → Plugins → trova il plugin → Disinstalla → Installa di nuovo.

5. Se hai modificato solo logica C# / view (nessuna risorsa locale nuova) → basta rebuild + restart, nessuna reinstallazione.

Nel tuo caso hai solo commentato controlli di permesso (logica C#) — basta rebuild + restart. Non serve disinstallare/reinstallare.


Nota

1. dotnet build — compilazione incrementale (solo ciò che è cambiato). Usalo sempre per sviluppo normale.

2. dotnet rebuild — clean + build (ricompila tutto da zero). Usalo solo se sospetti cache sporca o comportamenti anomali.

Nel 99% dei casi basta dotnet build + riavvio del sito.

Quando usare Clean

dotnet clean rimuove tutti gli output di build precedenti (bin/obj). Usalo:

- Quando cambi framework target
- Quando modifichi il .csproj (es. nuovi package, output path)
- Come alternativa a rebuild: se build dà errori strani, fai clean → build

Per modifiche di routine non serve.