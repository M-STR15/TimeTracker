# TimerTracker

## Architektura & rozhodnutí
Repozitáø je uspoøádán jako .NET solution (TimeTracker.sln) s více projekty pro backend, frontend a desktop:

- TimeTracker.BE.Web.BusinessLogic — backendová obchodní logika
- TimeTracker.BE.DB — data access / DB projekt
- TimeTracker.BE.Web.Shared — sdílené typy/kontrakty pro web
- TimeTracker.Basic.Enums — spoleèné enumy
- TimeTracker.Basic.Interfaces — spoleèná rozhraní
- TimeTracker.FE.Web.Components — frontend komponenty
- TimeTracker.Web.Blazor.Server — web (Blazor Server)
- TimeTracker.PC — desktop (PC verze)
- TimeTracker.Tests.* — jednotkové/integracní testy (v repozitáøi jsou adresáøe pro testy)

Struèné rozhodnutí:
- Oddìlení BE / FE / Shared / DB usnadòuje vývoj a testování jednotlivých vrstev.
- Doporuèené postupy pro tento typ øešení: Dependency Injection, DTO/Mapping mezi vrstvami, Repository, centralizované error handling middleware pro API.
