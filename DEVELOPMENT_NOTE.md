# TimerTracker 

Vývojové poznámky — výzvy, uživatel

## Hlavní technické výzvy (z praxe v tomto repozitáøi)
- Koordinace více projektù v jedné solution (BE, DB, Shared, FE, PC).
- Data layer: rozhodnutí mezi SQLite a MS SQL pro rùzné cílové verze (lokální PC vs nasazení).
- Udržení sdílených typù/kontraktù mezi backendem a frontendem (TimeTracker.BE.Web.Shared).
- Testy: nastavení jednotkových a integraèních testù pro ovìøení chování API a DB layeru.

## Co se dalo / dalo se nauèit
- Øízení více projektù ve solution a oddìlení odpovìdností.
- Jak implementovat èistou separaci domény, persistence a prezentaèní vrstvy.
- Zkušenost s Blazor Server pro web UI a s tvorbou desktop klienta v rámci stejného øešení.

## Kontext uživatele a øešený problém
- Cílový uživatel: jednotlivci nebo malé týmy, kteøí chtìjí evidovat èas strávený na vývojových aktivitách a generovat reporty za dny/projekty.
- Hlavní pøínos: snadné zapisování aktivit, pøehledné reporty, možnost exportu dat (viz Features v README).

## Konvence pojmenování
- `aClassName` - abstraktní tøída
- `privateMethodName()` - private metoda
- `_fieldName` - private fields