# ICS projekt

## Cíl
Cílem je vytvořit použitelnou a snadno rozšiřitelnou aplikaci, která splňuje požadavky zadání. Aplikace nesmí padat nebo zamrzávat. Pokud uživatel vyplní něco špatně, je upozorněn validační hláškou.

## Data
V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Uživatel
- Jméno
- Příjmení
- Fotografie (postačí url)
- (Aktivity)
- (Projekty)

### Aktivita
- Začátek (datum, čas)
- Konec (datum, čas)
- Typ / tag aktivity (postačí enum, nebo uživatelem definovaná hodnota)
- Popis aktivity
  
### Projekt
- Název
- (Aktivity)
- (Uživatelé)

> () anotují vazby mezi entitami