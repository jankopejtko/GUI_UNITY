# Osnova lekce: Unity Základy & UI Spawning

Tento dokument slouží jako průvodce pro výuku základů Unity.

---

## 🏃‍➡️ LEVEL 1: Player Setup & Movement
**Cíl:** Vytvoření postavy, nastavení fyziky a základní pohyb.

### 1. Vytvoření hráče (Player)
* **Objekt:** `Hierarchy` -> `Create` -> `Capsule`
    * **Název:** "Player"
    * **Tag:** "Player"
* **Komponenty:**
    * Přidat **Character Controller**.
    * **Odstranit** původní `Capsule Collider` (kolizi řeší Controller sám).
* **Vizuál:** Přiřadit materiál ze složky `StarterPack/materials`.

### 2. Prostředí (Environment)
* **Země:** `Create` -> `3D Object` -> `Plane` (Scale: 100, 1, 100)
    * **Název:** "ground"
    * **Tag:** "ground"
* **Platformy:** Přidat 3x `Cube` (Scale: `5, 0.2, 5`).
    * Aplikovat materiály `floor` a `platform`.

### 3. Skriptování a Kamera
* **Organizace:** Přetáhnout hráče do složky `Prefabs/Level1` (vytvoření prefabu).
* **Kamera:** Přetáhnout `Main Camera` v hierarchii pod objekt **Player** (vytvoření Child objektu).
* **Skript:** Přidat komponentu `player_movement`.
    * **Fix pro Input:** Pokud nefunguje klávesnice, jdi do: 
      `Edit -> Project Settings -> Player -> Other Settings -> Active Input Handling -> Both`.

> **⏳ Úkol pro studenty (20 min):**
> * Implementuj **Sprint** (zvýšení rychlosti při držení `KeyCode.LeftShift`).
> * Implementuj **Double Jump** (využij proměnnou `jumpCount` a podmínku `< 2`).

---

## 🏭 LEVEL 2: UI & Prefab Spawning
**Cíl:** Práce s UI tlačítky a dynamické vytváření objektů ve scéně.

### 1. Příprava Scény
* Duplikovat scénu `level1` (CTRL + D) -> Přejmenovat na `level2`.
* **Úprava kódu:** Ve skriptu `player_movement` zakomentuj zamykání kurzoru:
  `// Cursor.lockState = CursorLockMode.Locked;`
  *(Potřebujeme myš pro klikání na UI tlačítka).*

### 2. UI Systém (Canvas & Button)
* **Struktura:** `Create Empty` (Název: "UI") -> pod něj vložit `UI -> Canvas`.
* **Tlačítko:** `Canvas -> UI -> Legacy -> Button`.
    * **Název:** "spawn button"
    * **Text:** "Spawn Object"
* **Skript:** Na tlačítko přidat komponentu `spawnPrefab`.
* **Event:** V inspektoru `Button -> On Click ()` -> Přidat slot (+), přetáhnout tam samotné tlačítko a vybrat funkci: `spawnPrefab.SpawnObject`.

### 3. Spawning Logika
* **Prefab pro spawn:** Vytvořit novou `Cube` s komponentou `Rigidbody`, přetáhnout do složky `Prefabs` a originál ze scény smazat.
* **Spawn Point:** Na hráči vytvořit prázdný objekt `Create Empty` (Název: "spawnObjectTransform").
* **Reference:** Ve skriptu na tlačítku propojit prefab kostky a transformaci spawn pointu.

> **⏳ Úkol pro studenty (20 min):**
> * **Timer:** Objekt se po spawnu automaticky smaže po 15 sekundách (`Destroy`).
> * **Nová koule:** Přidat druhé tlačítko pro spawn kuličky (Sphere).
> * **Čistič:** Tlačítko pro smazání všech objektů s tagem **"spawnedPrefab"**.
> * *Důležité:* Nezapomeňte vytvořit Tag **"spawnedPrefab"** v Tag Manageru!


# 🪙 LEVEL 3: Collectibles & Scoring System
**Cíl:** Práce s Triggery (neviditelnými snímači), sbírání předmětů a aktualizace UI skóre.

---

## 1. Příprava Coin Prefabu (Mince)
* **Objekt:** `Create` -> `3D Object` -> `Cylinder`.
* **Transformace:**
    * **Scale:** `0.5, 0.05, 0.5`.
    * **Rotation:** `-90, 0, 0` (aby mince stála vertikálně).
* **Vizuál:** Vytvořit žlutý materiál (např. "coin").
* **Fyzika (Klíčové):** * V komponentě **Mesh Collider** (nebo Capsule Collider) zaškrtni políčko **Is Trigger**.
    * *Teorie:* Triggerem lze projít, nevyvolá fyzický náraz, ale spustí událost v kódu.
* **Tag:** Vytvořit nový Tag **"Coin"** a přiřadit ho minci.
* **Organizace:** Přetáhnout do složky `Prefabs`.



---

## 2. Uživatelské rozhraní (UI Score)
* **Vytvoření:** V existujícím `Canvasu` vytvořit `UI -> Legacy -> Text`.
    * **Název:** "ScoreText"
    * **Text:** "Coins: 0"
* **Pozicování:** * Pomocí **Anchor Presets** (čtvereček v Rect Transform) + klávesa `Alt` ukotvi text do levého nebo pravého horního rohu.
* **Vizuál:** Zvětšit font a změnit barvu na bílou/žlutou pro lepší čitelnost.

---

## 3. Logika sbírání (Programování)
Studenti do svých skriptů implementují následující logiku:

### A) Rotace mince (`ItemRotation`)
* Skript se vloží přímo na prefab mince.
* **Funkce:** Neustálá rotace kolem osy (použijte `transform.Rotate`).

### B) Inventář hráče (`PlayerInventory`)
* Skript se vloží na objekt **Player**.
* **Proměnné:** Celé číslo (`int`) pro počet mincí a odkaz na `UI Text`.
* **Detekce:** Použijte metodu `OnTriggerEnter`. 
    * *Logika:* Pokud objekt, do kterého jsme narazili, má Tag "Coin":
        1. Přičti +1 ke skóre.
        2. Aktualizuj text v UI.
        3. Znič objekt mince (`Destroy`).

---

## 4. Level Design
* Rozmístit po mapě alespoň 10-30 mincí.
* Využít duplikování (`CTRL + D`) pro rychlou tvorbu cestiček z mincí.

---

> **⏳ Úkol pro studenty (20 min):**
>1.  **Vítězná zpráva:** Pokud počet mincí ve scéně dosáhne 0, změňte barvu ScoreTextu na zelenou a vypište do konzole "Victory!".
>2.  **Audio Efekt:** Přidejte komponentu `AudioSource` (vypněte "play on awake") na hráče a při každém sebrání mince přehrajte krátké "pípnutí".
>3.  **Power-up:** Vytvořte speciální "červenou minci". Po jejím sebrání se přidá 5 mincí a přehraje se jiná melodie.

---

### Teoretické minimum pro lekci:
* **Trigger:** Detekuje průchod, ale nebrání pohybu (využití: sbírání věcí, checkpointy, zóny pro spawnování nepřátel).
* **Collision:** Pevná překážka (využití: zdi, podlaha, bedny).
* **using UnityEngine.UI:** Bez tohoto řádku nahoře v kódu nelze ovládat UI prvky na obrazovce.

---

# 🔥 LEVEL 4: Traps, Lava and Respawn
**Cíl:** Vytvořit nebezpečné zóny a naučit se restartovat hru pomocí SceneManageru.

---

## 1. Příprava "Lávy" (Hazard Zone)
* **Objekt:** `Hierarchy` -> `Create` -> `3D Object` -> `Cube` (nebo `Plane`).
* **Nastavení:**
    * **Název:** "Lava"
    * **Měřítko (Scale):** Roztáhněte kostku tak, aby pokryla celou plochu pod herními plošinkami, kam může hráč spadnout.
* **Vizuál:** Vytvořte nový materiál (např. "mat_lava") s červenou nebo oranžovou barvou. NEBO použijte shader z StarterPack/Shaders/Lava
* **Fyzika (Klíčové):** V komponentě **Box Collider** zaškrtněte políčko **Is Trigger**.
    * *Vysvětlení:* Chceme, aby hráč do lávy propadl, ne aby po ní chodil jako po podlaze.
* **Tag:** Vytvořte nový Tag **"Lava"** a přiřaďte ho této ploše.

---

## 2. Restartování scény (Programování)
Abychom mohli hru restartovat, musíme do skriptu přidat knihovnu pro správu scén.

### Úprava skriptu (`PlayerInventory` a `Player_movement`)
* **Knihovna:** Na úplný začátek skriptu (nahoře k ostatním using) přidejte `using UnityEngine.SceneManagement;`.
* **Logika v OnTriggerEnter:** * *Podmínka:* Pokud narazíme do objektu, který má Tag **"Lava"**:
        1. Zjistěte název aktuální scény pomocí `SceneManager.GetActiveScene().name`.
        2. Znovu tuto scénu načtěte pomocí `SceneManager.LoadScene`.

---

## 3. Death Zone (Záchranná síť)
Někdy hráč nespadne přímo na viditelnou lávu, ale chybou odletí mimo mapu do prázdna.
* **Úkol:** Upravte příslušný skript tak aby detekoval zda hrač spadl pod mapu napr `player position y < -10`
* To zajistí, že se hra restartuje i v případě, že hráč vyletí z mapy.

---

>## ⏳ Úkoly pro studenty (20 minut)
>1. **Mizející plošinka:** Vytvořte novou plošinku se skriptem, který ji zničí 2 sekundu poté, co se jí hráč dotkne.
>2. **Pohyblivá past:** Vytvořte kvádr s Tagem "Lava", který se neustále pohybuje ze strany na stranu.

---

### ⚠️ Důležité pro Build
Aby mohl `SceneManager` scénu restartovat, musí být scéna uložena a přidána v seznamu pro export: 
`File` -> `Build Settings` -> `Add Open Scenes`.

---

# 🕹️ LEVEL 5: Main Menu and Game export (Build)
**Cíl:** Vytvořit úvodní menu hry a vyexportovat projekt jako samostatně spustitelnou aplikaci (.exe).

---

## 1. Příprava scény Main Menu
* **Šablona menu:** Ve složce `StarterPack/Scenes` najděte a duplikujte scénu jako **"MainMenu"**.
* **Úprava UI:** V okně `Hierarchy` rozbalte objekt `Canvas`. Uvidíte tam již připravené prvky:
    * `Title` (Název hry)
    * `Game info` (Podtitul nebo instrukce)
    * `Author` (Vaše jméno)
    * `Button start` a `Button exit`
* **Změna textů:** Proklikejte si jednotlivé textové prvky a v okně Inspector přepište texty tak, aby odpovídaly vaší hře.

---

## 2. Oživení Menu (Programování)
Aby tlačítka fungovala, potřebujeme na náš Canvas přidat jednoduchý skript.
* **Přidání skriptu:** Vytvořte nový C# skript s názvem `MainMenuManager` a **přetáhněte ho přímo na objekt `Canvas`** v Hierarchy.
* **Logika ve skriptu:**
    * Úplně nahoru (k ostatním `using`) přidejte: `using UnityEngine.SceneManagement;`
    * Vytvořte veřejnou metodu pro spuštění hry:
      ```csharp
      public void PlayGame() {
          SceneManager.LoadScene(""); // Změňte název podle vaší scény
      }
      ```
    * Vytvořte veřejnou metodu pro vypnutí hry:
      ```csharp
      public void QuitGame() {
          Debug.Log("Hra se vypíná...");
          Application.Quit(); // V Editoru se nic nestane, funguje až po exportu (buildu)
      }
      ```
* **Propojení tlačítek:** * Klikněte na `Button start`. V Inspectoru sjeďte dolů na **On Click ()**.
    * Klikněte na **+**, přetáhněte do volného políčka objekt **Canvas** (protože na něm je náš skript).
    * Z rozbalovacího menu `No Function` vyberte `MainMenuManager` -> `PlayGame()`.
    * Stejný postup zopakujte pro `Button exit`, jen vyberte funkci `QuitGame()`.



---

---

## 3. Build Settings (Příprava na export)
Aby fungovalo přepínání z Menu do hry, Unity musí vědět, jaké scény do finální hry patří.
* Otevřete `File` -> `Build Settings`.
* Otevřete si dole složku Scenes a **přetáhněte scény do horního okna "Scenes In Build"**.
* **DŮLEŽITÉ - Pořadí (Index):** Scéna `MainMenu` musí být v seznamu úplně nahoře (Index 0). Poté následuje `level1` (Index 1) atd.
* **Export (Build):** Klikněte na tlačítko **Build**, vytvořte si na disku novou prázdnou složku (např. "MojeHra_Build") a potvrďte. Unity vytvoří `.exe` soubor vaší hry!

---

>## ⏳ Úkoly pro studenty
>1. **Návrat do Menu po výhře:** Otevřete svůj skript pro sbírání mincí (`PlayerInventory`). Najděte podmínku pro vítězství (když je počet mincí <= 0) a přidejte tam příkaz pro načtení scény s menu:
   `SceneManager.LoadScene("MainMenu");`
>2. **In-Game Tlačítko:** Přidejte do svého herního levelu malé UI tlačítko do rohu obrazovky s textem "Menu". Vytvořte mu jednoduchý skript, který po kliknutí hráče vrátí zpět na úvodní obrazovku.
>3. **Úpravy/rozšíření levelu podle své prefernce:** Přidejte více platforem/mincí/překážek.