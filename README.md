
# TimerTracker (ve vývoji)

💡 *Aplikace pro evidenci a sledování aktivit při vývoji softwaru. Umožňuje zaznamenávat činnosti na PC a zobrazovat reporty aktivit za jednotlivé dny. Projekt vznikl pro vlastní potřebu, ale je zdarma k užívání.*

---

## Features

- Zápis aktivit na PC
- Zobrazení reportu s výsledky aktivit
- Podpora PC i Web verze


## Installation

### PC verze:

Po prvním spuštění aplikace automaticky vytvoří potřebné databázové soubory:

```
C:\Users\<user>\AppData\Local\TimeTracker.db
C:\Users\<user>\AppData\Local\TimeTracker.db-wal
C:\Users\<user>\AppData\Local\TimeTracker.db-shm
```

### Web verze:

Po prvním spuštění aplikace se automaticky vytvoří databázové tabulky v MS SQL.
Stačí upravit připojovací řetězec ve třídě:
```
TimeTracker.BE.DB.DataAccess.MsSqlDbContext -> OnConfiguring()
```

## Installation for develop

- Aby se při generování informací zobrazil název aktuální větve, je potřeba mít nainstalovaný [Git for Windows](https://gitforwindows.org/)

- Někdy se nespustí kompilace hned na poprvé, je to z důvodu, že aplikace má nastavený powershell script, který generuje BuildInfo.cs.
 Někdy se stane, že se to nepovede na poprvé, je potřeba spustit znovu kompilaci.


## Release

### Changes
Legend: 

Hlavní kategorie 

- 🚀 - Nová funkce
- 🐞 - Oprava chyby
- 📝 - Dokumentace
- 🛠 - Úprava kódu
- 🚨 - Bezpečnostní aktualizace
- ❌ - Odstranění funkce
- 🛢 - Databázové úpravy

Dodatečné info
- 🔒 - Nezveřejňovat informaci zákazníkovi
- 🔥 – Kritická
- ⚠ – Důležitá -> ovlivňující mnoho uživatelů
- 🛑 – Zásadní
- 🚨 – Bezpečnostní

***

#### 0.0.3 (2025-10-25) - Web verze:
 - 🛠  úprava zobrazování verze aplikace v informačním panelu
 
#### 0.0.2 (2025-10-11) - PC verze:
- 🐞 vytvořen nový migrační balíček a oživení připojování k DB 
- 🐞 při přepínání projektů, aby docházelo k aktualizaci tabulky (v případě neexistujících záznamů)

***

#### 0.0.1 (2025-10-10) – První betaverze

PC verze: využívá MS SQL

Web verze: využívá SQLite

U webové aplikace je potřeba doladit informační panel po změně aktivity

---

## Authors

[@M-STR](https://github.com/M-STR15)


## License

[MIT](https://choosealicense.com/licenses/mit/)

## 🔗 Links
[![portfolio](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/M-STR15/TimeTracker)