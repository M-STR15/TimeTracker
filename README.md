
# TimerTracker (ve vývoji)

Aplikace vznikla za účelem evidování aktivit při vývoji SWs. Následné zobrazení výsledků aktivit za jednotlivé dny. 

Tento program byl vytvořen za účelem vlastní potřeby.Ale může ho kdokoliv užívat zdarma.


## Authors

- [@M-STR](https://github.com/M-STR15)


## License

[MIT](https://choosealicense.com/licenses/mit/)


## Features

- Zápis aktivit na PC
- Zobrazení reportu s výsledky aktivit


## Installation

### PC verze:

- aplikace sama po spuštění vytvoří potřebné databázové tabulky na uložišti:
 
   - "C:\Users\<user>\AppData\Local\TimeTracker.db"
   - "C:\Users\<user>\AppData\Local\TimeTracker.db-wal"
   - "C:\Users\<user>\AppData\Local\TimeTracker.db-shm"

### Web verze:

- aplikace sama po spuštění vytvoří potřebné databázové tabulky v MS SQL databázi, jenom je potřeba změnit připojovací řetězec TimeTracker.BE.DB.DataAccess.MsSqlDbContext->OnConfiguring()


## Installation for develop

- Aby u generovnání informace zobrazil vygenerovat název větve, je potřeba mít nainstalovaný https://gitforwindows.org/

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
- ***

***

#### 0.0.3   (2025-10-25)
Web verze:
 - 🛠  úprava zobrazování verze aplikace v informačním panelu
#### 0.0.2   (2025-10-11)

PC verze:
- 🐞 vytvořen nový migrační balíček a oživení připojování k DB 
- 🐞 při přepínání projektů, aby 

***

#### 0.0.1   (2025-10-10)

-uvolněna první betaverze - u webové aplikace je ještě potřeba doladit některé funkce (informační panel po změně  aktivity)

PC verze:

-využívá MS SQL

WEB verze:

-využívá SQLite


## 🔗 Links
[![portfolio](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/M-STR15/TimeTracker)

