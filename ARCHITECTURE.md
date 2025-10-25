# TimerTracker

## Architektura & rozhodnut�
Repozit�� je uspo��d�n jako .NET solution (TimeTracker.sln) s v�ce projekty pro backend, frontend a desktop:

- TimeTracker.BE.Web.BusinessLogic � backendov� obchodn� logika
- TimeTracker.BE.DB � data access / DB projekt
- TimeTracker.BE.Web.Shared � sd�len� typy/kontrakty pro web
- TimeTracker.Basic.Enums � spole�n� enumy
- TimeTracker.Basic.Interfaces � spole�n� rozhran�
- TimeTracker.FE.Web.Components � frontend komponenty
- TimeTracker.Web.Blazor.Server � web (Blazor Server)
- TimeTracker.PC � desktop (PC verze)
- TimeTracker.Tests.* � jednotkov�/integracn� testy (v repozit��i jsou adres��e pro testy)

Stru�n� rozhodnut�:
- Odd�len� BE / FE / Shared / DB usnad�uje v�voj a testov�n� jednotliv�ch vrstev.
- Doporu�en� postupy pro tento typ �e�en�: Dependency Injection, DTO/Mapping mezi vrstvami, Repository, centralizovan� error handling middleware pro API.
