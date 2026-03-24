# Lesson Plan: Unity Basics & UI Spawning

This document serves as a guide for teaching Unity basics.

---

## 🏃‍➡️ LEVEL 1: Player Setup & Movement
**Goal:** Character creation, physics setup, and basic movement.

### 1. Player Creation
* **Object:** `Hierarchy` -> `Create` -> `Capsule`
    * **Name:** "Player"
    * **Tag:** "Player"
* **Components:**
    * Add **Character Controller**.
    * **Remove** the original `Capsule Collider` (collisions are handled by the Controller itself).
* **Visuals:** Assign a material from the `StarterPack/materials` folder.

### 2. Environment
* **Ground:** `Create` -> `3D Object` -> `Plane` (Scale: 100, 1, 100)
    * **Name:** "ground"
    * **Tag:** "ground"
* **Platforms:** Add 3x `Cube` (Scale: `5, 0.2, 5`).
    * Apply the `floor` and `platform` materials.

### 3. Scripting and Camera
* **Organization:** Drag the player into the `Prefabs/Level1` folder (creating a prefab).
* **Camera:** Drag the `Main Camera` in the hierarchy onto the **Player** object (making it a Child object).
* **Script:** Add the `player_movement` component.
    * **Input Fix:** If the keyboard isn't working, go to: 
      `Edit -> Project Settings -> Player -> Other Settings -> Active Input Handling -> Both`.

> **⏳ Student Task (20 min):**
> * Implement **Sprint** (increase speed while holding `KeyCode.LeftShift`).
> * Implement **Double Jump** (use a `jumpCount` variable and a `< 2` condition).

---

## 🏭 LEVEL 2: UI & Prefab Spawning
**Goal:** Working with UI buttons and dynamically spawning objects in the scene.

### 1. Scene Preparation
* Duplicate the `level1` scene (CTRL + D) -> Rename to `level2`.
* **Code tweak:** In the `player_movement` script, comment out the cursor locking:
  `// Cursor.lockState = CursorLockMode.Locked;`
  *(We need the mouse to click on UI buttons).*

### 2. UI System (Canvas & Button)
* **Structure:** `Create Empty` (Name: "UI") -> place a `UI -> Canvas` inside it.
* **Button:** `Canvas -> UI -> Legacy -> Button`.
    * **Name:** "spawn button"
    * **Text:** "Spawn Object"
* **Script:** Add the `spawnPrefab` component to the button.
* **Event:** In the inspector `Button -> On Click ()` -> Add a slot (+), drag the button itself into it, and select the function: `spawnPrefab.SpawnObject`.

### 3. Spawning Logic
* **Spawn Prefab:** Create a new `Cube` with a `Rigidbody` component, drag it to the `Prefabs` folder, and delete the original from the scene.
* **Spawn Point:** Create an empty object on the player `Create Empty` (Name: "spawnObjectTransform").
* **References:** In the script on the button, link the cube prefab and the spawn point transform.

> **⏳ Student Task (20 min):**
> * **Timer:** The spawned object automatically deletes itself after 15 seconds (`Destroy`).
> * **New Sphere:** Add a second button to spawn a sphere.
> * **Cleaner:** A button to delete all objects with the tag **"spawnedPrefab"**.
> * *Important:* Don't forget to create the **"spawnedPrefab"** Tag in the Tag Manager!

---

# 🪙 LEVEL 3: Collectibles & Scoring System
**Goal:** Working with Triggers (invisible sensors), collecting items, and updating the UI score.

---

## 1. Coin Prefab Preparation
* **Object:** `Create` -> `3D Object` -> `Cylinder`.
* **Transform:**
    * **Scale:** `0.5, 0.05, 0.5`.
    * **Rotation:** `-90, 0, 0` (so the coin stands vertically).
* **Visuals:** Create a yellow material (e.g., "coin").
* **Physics (Key Step):** * In the **Mesh Collider** (or Capsule Collider) component, check the **Is Trigger** box.
    * *Theory:* A trigger can be passed through; it doesn't cause a physical collision, but it triggers an event in the code.
* **Tag:** Create a new Tag **"Coin"** and assign it to the coin.
* **Organization:** Drag it into the `Prefabs` folder.

---

## 2. User Interface (UI Score)
* **Creation:** In the existing `Canvas`, create `UI -> Legacy -> Text`.
    * **Name:** "ScoreText"
    * **Text:** "Coins: 0"
* **Positioning:** * Using **Anchor Presets** (the square in Rect Transform) + the `Alt` key, anchor the text to the top left or top right corner.
* **Visuals:** Increase the font size and change the color to white/yellow for better readability.

---

## 3. Collection Logic (Programming)
Students will implement the following logic into their scripts:

### A) Coin Rotation (`ItemRotation`)
* This script is placed directly on the coin prefab.
* **Function:** Constant rotation around its axis (use `transform.Rotate`).

### B) Player Inventory (`PlayerInventory`)
* This script is placed on the **Player** object.
* **Variables:** An integer (`int`) for the coin count and a reference to the `UI Text`.
* **Detection:** Use the `OnTriggerEnter` method. 
    * *Logic:* If the object we collided with has the Tag "Coin":
        1. Add +1 to the score.
        2. Update the text in the UI.
        3. Destroy the coin object (`Destroy`).

---

## 4. Level Design
* Scatter at least 10-30 coins around the map.
* Use duplication (`CTRL + D`) to quickly create trails of coins.

---

> **⏳ Student Task (20 min):**
> 1. **Victory Message:** If the number of coins in the scene reaches 0, change the ScoreText color to green and print "Victory!" to the console.
> 2. **Audio Effect:** Add an `AudioSource` component (turn off "play on awake") to the player and play a short "beep" every time a coin is collected.
> 3. **Power-up:** Create a special "red coin". Collecting it adds 5 coins and plays a different tune.

---

### Theoretical Minimum for the Lesson:
* **Trigger:** Detects passage but doesn't block movement (use cases: collecting items, checkpoints, enemy spawn zones).
* **Collision:** Solid obstacle (use cases: walls, floors, crates).
* **using UnityEngine.UI:** Without this line at the top of the code, you cannot control UI elements on the screen.

---

# 🔥 LEVEL 4: Traps, Lava and Respawn
**Goal:** Create hazard zones and learn how to restart the game using the SceneManager.

---

## 1. "Lava" Preparation (Hazard Zone)
* **Object:** `Hierarchy` -> `Create` -> `3D Object` -> `Cube` (or `Plane`).
* **Settings:**
    * **Name:** "Lava"
    * **Scale:** Stretch the cube to cover the entire area below the game platforms where the player can fall.
* **Visuals:** Create a new material (e.g., "mat_lava") with a red or orange color. OR use the shader from `StarterPack/Shaders/Lava`.
* **Physics (Key Step):** In the **Box Collider** component, check the **Is Trigger** box.
    * *Explanation:* We want the player to fall through the lava, not walk on it like a floor.
* **Tag:** Create a new Tag **"Lava"** and assign it to this area.

---

## 2. Restarting the Scene (Programming)
To restart the game, we need to add a scene management library to our script.

### Script Modification (`PlayerInventory` and `Player_movement`)
* **Library:** At the very top of the script (with the other `using` directives), add `using UnityEngine.SceneManagement;`.
* **Logic in OnTriggerEnter:** * *Condition:* If we collide with an object that has the Tag **"Lava"**:
        1. Get the current scene name using `SceneManager.GetActiveScene().name`.
        2. Reload this scene using `SceneManager.LoadScene`.

---

## 3. Death Zone (Safety Net)
Sometimes the player doesn't fall directly onto the visible lava, but glitches off the map into the void.
* **Task:** Modify the appropriate script to detect if the player has fallen below the map, e.g., `player position y < -10`.
* This ensures the game restarts even if the player flies off the map.

---

>## ⏳ Student Tasks (20 minutes)
>1. **Disappearing Platform:** Create a new platform with a script that destroys it 2 seconds after the player touches it.
>2. **Moving Trap:** Create a cube with the "Lava" Tag that constantly moves from side to side.

---

### ⚠️ Important for Build
For `SceneManager` to restart a scene, the scene must be saved and added to the export list: 
`File` -> `Build Settings` -> `Add Open Scenes`.

---

# 🕹️ LEVEL 5: Main Menu and Game export (Build)
**Goal:** Create a game start menu and export the project as a standalone executable application (.exe).

---

## 1. Main Menu Scene Preparation
* **Menu Template:** In the `StarterPack/Scenes` folder, find and duplicate the scene as **"MainMenu"**.
* **UI Modification:** In the `Hierarchy` window, expand the `Canvas` object. You will see pre-prepared elements there:
    * `Title` (Game Name)
    * `Game info` (Subtitle or instructions)
    * `Author` (Your name)
    * `Button start` and `Button exit`
* **Text Changes:** Click through the individual text elements and rewrite the texts in the Inspector window to match your game.

---

## 2. Bringing the Menu to Life (Programming)
For the buttons to work, we need to add a simple script to our Canvas.
* **Adding the script:** Create a new C# script named `MainMenuManager` and **drag it directly onto the `Canvas` object** in the Hierarchy.
* **Script Logic:**
    * At the very top (with the other `using` directives), add: `using UnityEngine.SceneManagement;`
    * Create a public method to start the game:
      ```csharp
      public void PlayGame() {
          SceneManager.LoadScene(""); // Change the name according to your scene
      }
      ```
    * Create a public method to quit the game:
      ```csharp
      public void QuitGame() {
          Debug.Log("Quitting game...");
          Application.Quit(); // Nothing happens in the Editor, it only works after export (build)
      }
      ```
* **Connecting the Buttons:** * Click on `Button start`. In the Inspector, scroll down to **On Click ()**.
    * Click the **+**, drag the **Canvas** object into the empty slot (because our script is on it).
    * From the `No Function` dropdown menu, select `MainMenuManager` -> `PlayGame()`.
    * Repeat the same process for `Button exit`, just select the `QuitGame()` function.

---

## 3. Build Settings (Preparation for Export)
For switching from the Menu to the game to work, Unity needs to know which scenes belong in the final game.
* Open `File` -> `Build Settings`.
* Open the Scenes folder at the bottom and **drag the scenes into the top "Scenes In Build" window**.
* **IMPORTANT - Order (Index):** The `MainMenu` scene must be at the very top of the list (Index 0). Then follows `level1` (Index 1), etc.
* **Export (Build):** Click the **Build** button, create a new empty folder on your disk (e.g., "MyGame_Build"), and confirm. Unity will create the `.exe` file of your game!

---

>## ⏳ Student Tasks
>1. **Return to Menu after Victory:** Open your coin collection script (`PlayerInventory`). Find the victory condition (when the number of coins is <= 0) and add the command to load the menu scene:
   `SceneManager.LoadScene("MainMenu");`
>2. **In-Game Button:** Add a small UI button to the corner of the screen in your game level with the text "Menu". Create a simple script for it that returns the player to the start screen when clicked.
>3. **Level adjustments/expansions according to preference:** Add more platforms/coins/obstacles.
