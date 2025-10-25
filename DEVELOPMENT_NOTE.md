# TimerTracker 

V�vojov� pozn�mky � v�zvy, u�ivatel

## Hlavn� technick� v�zvy (z praxe v tomto repozit��i)
- Koordinace v�ce projekt� v jedn� solution (BE, DB, Shared, FE, PC).
- Data layer: rozhodnut� mezi SQLite a MS SQL pro r�zn� c�lov� verze (lok�ln� PC vs nasazen�).
- Udr�en� sd�len�ch typ�/kontrakt� mezi backendem a frontendem (TimeTracker.BE.Web.Shared).
- Testy: nastaven� jednotkov�ch a integra�n�ch test� pro ov��en� chov�n� API a DB layeru.

## Co se dalo / dalo se nau�it
- ��zen� v�ce projekt� ve solution a odd�len� odpov�dnost�.
- Jak implementovat �istou separaci dom�ny, persistence a prezenta�n� vrstvy.
- Zku�enost s Blazor Server pro web UI a s tvorbou desktop klienta v r�mci stejn�ho �e�en�.

## Kontext u�ivatele a �e�en� probl�m
- C�lov� u�ivatel: jednotlivci nebo mal� t�my, kte�� cht�j� evidovat �as str�ven� na v�vojov�ch aktivit�ch a generovat reporty za dny/projekty.
- Hlavn� p��nos: snadn� zapisov�n� aktivit, p�ehledn� reporty, mo�nost exportu dat (viz Features v README).

```