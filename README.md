# Ürituste korraldamine

Programmeerimine II aineprojekt ürituste, registreerimiste, arvete ja maksete haldamiseks.

## Projektid

- `KooliProjekt.Application` – andmemudel, Entity Framework Core, CQRS handlerid, valideerimine ja repositoryd.
- `KooliProjekt.WebAPI` – REST API ja Swagger.
- `KooliProjekt.Application.UnitTests` – application-kihi ühiktestid.
- `KooliProjekt.IntegrationTests` – kõikide API kontrollerite List/Get/Save/Delete testid.
- `KooliProjekt.WindowsForms` – MVP mustriga töölauarakendus.
- `KooliProjekt.WindowsForms.UnitTests` – presenteri ühiktestid.
- `KooliProjekt.WpfApplication` – MVVM, binding, `INotifyPropertyChanged` ja commandid.
- `KooliProjekt.WpfApplication.UnitTests` – view modeli ühiktestid.
- `KooliProjekt.BlazorWASM` – sündmuste loend ning lisamise ja muutmise vorm serveripoolsete valideerimisvigadega.

## Käivitamine

1. Ava `KooliProjekt.sln` Visual Studios.
2. Käivita `KooliProjekt.WebAPI` (`http://localhost:5086`). Esimesel käivitamisel rakendatakse migratsioonid ja luuakse testandmed.
3. Käivita soovitud klient: Windows Forms, WPF või Blazor WASM (`http://localhost:5173`).

Andmebaas kasutab SQL Server LocalDB-d. Ühendusstring asub failis `KooliProjekt.WebAPI/appsettings.json`.

## Testid

Kõik testid:

```powershell
dotnet test KooliProjekt.sln
```

Application-kihi coverage raport:

```powershell
.\run-tests.ps1
```

Raport luuakse kausta `TestResults`.
