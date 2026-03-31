
# TimerTracker (ve vývoji)

💡 *Aplikace pro evidenci a sledování aktivit při vývoji softwaru. Umožňuje zaznamenávat činnosti na PC a zobrazovat reporty aktivit za jednotlivé dny. Projekt vznikl pro vlastní potřebu, ale je zdarma k užívání.*

Aplikace vznikla z důvodu, prezentace stylu psaní kódu, architektury a dalších aspektů vývoje softwaru. Projekt je stále ve vývoji, ale již nyní nabízí základní funkce pro evidenci a sledování aktivit.
Program není zamýšlen jako finální produkt, ale spíše jako ukázka a nástroj pro vlastní potřebu. Projekt je zdarma k užívání, ale není určen pro komerční využití.

---

## Features

- Zápis aktivit na PC
- Zobrazení reportu s výsledky aktivit
- Podpora PC i Web verze

## Technologie

### PC verze:
- Backend: C# (.NET9.0-windows)
- Architektura: „WBA“ (Windows-Based Application)
    - tato architektura byla zvolena z důvodu, že MVVM architektura již je ukázana v jiném projektu
- Databáze: SQLite
- Licence: MIT

### Web verze:
- Backend: C# (.NET 9)
- Frontend: Blazor(HTML, SCSS, JavaScript)
- Architektura: MVC (Model-View-Controller)
- Databáze: MS SQL
- Licence: MIT

## Rychlé spuštění (pro developery)

### PC verze:
1. Clone repo
   git clone https://github.com/M-STR15/TimeTracker.git
2. Zvolit projekt TimeTracker.PC a spustit aplikaci (F5 nebo Ctrl+F5)

Poznámka:

Po prvním spuštění aplikace automaticky vytvoří potřebné databázové soubory:

```
C:\Users\<user>\AppData\Local\TimeTracker.db
C:\Users\<user>\AppData\Local\TimeTracker.db-wal
C:\Users\<user>\AppData\Local\TimeTracker.db-shm
```
### Web verze:
1. Clone repo
   git clone https://github.com/M-STR15/TimeTracker.git
2. Nainstalovat lokální databázi MS SQL. Uživatel musí mít admin práva do DB, aby byla vygenerována DB.
3. Zvolit projekt TimeTracker.Web a spustit aplikaci (F5 nebo Ctrl+F5) 

Poznámka:

Po prvním spuštění aplikace se automaticky vytvoří databázové tabulky v MS SQL.
Stačí upravit připojovací řetězec ve třídě:
```
TimeTracker.BE.DB.DataAccess.MsSqlDbContext -> OnConfiguring()
```
## Architektura & rozhodnutí

Odkaz na [ARCHITECTURE.md](ARCHITECTURE.md)

## Changelog

Odkaz na [CHANGELOG.md](CHANGELOG.md)

## Development note

Odkaz na [DEVELOPMENT_NOTE.md](DEVELOPMENT_NOTE.md)

---

## Authors

[@M-STR](https://github.com/M-STR15)


## License

[MIT](https://choosealicense.com/licenses/mit/)

## 🔗 Links

[![portfolio](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/M-STR15/TimeTracker)