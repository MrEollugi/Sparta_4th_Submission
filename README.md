Last Updated: 2024-05-23
# Submission Prototype

A Unity prototype featuring player movement, item interaction, and modular gameplay systems — built as part of a multi-feature gameplay demo.

---

## Features

### Core Mechanics
- **Player Movement & Jumping** using Rigidbody physics  
  - **Double Jump** with grounded check and limited jump count  
  - **Directional Jump Boost** when jumping opposite to current movement  
  - **Increased gravity during falling** for tighter control
- **Third-Person / First-Person Camera Toggle** (Tab)
- **Interactable Objects** using Raycast (1st-person only)

### Inventory System
- 2-slot item inventory
- R: Use item  
- Q: Drop item  
- T: Switch selected slot
- Inventory UI highlights selection and displays item icons

### Item Effect System
- Fully modular effect system using **ScriptableObject**
- ItemData can hold **multiple effects**
- Current implemented effects:
  - Heal (restores HP)
  - Speed Boost (temporarily increases move speed)

### UI System
- Dynamic **Health Bar**
- Minimal **Inventory UI**
- **Inspect UI** shows object info on gaze (first-person mode)
- **Pause Menu** with resume, restart, and quit

### Environment Interactives
- **Jump Pads** that launch the player upward on contact
- **Moving Platforms** that travel between fixed points
  - Supports looping and ping-pong modes
  - Player automatically moves along with the platform via velocity syncing

### Lighting
- **Scene lighting initialized at runtime** via LightSettingInitializer
  - Ambient lighting controlled through `RenderSettings`
  - Directional light intensity, color, and shadows configured automatically

### System For Test
- H: Reduce HP (10)
- J: Restore HP (10)

---

## Tech Stack

- Unity 2022.x
- Unity Input System (new)
- ScriptableObject architecture
- Modular, extensible C# component system

---

## ✅ Completed Tasks

- [x] Double jump system
- [x] Modular item effects
- [x] Inventory and UI system
- [x] Moving platform system
- [x] Pause Menu with resume/restart/quit
- [x] LightSettingInitializer for ambient and directional lighting

## 🔜 Upcoming Tasks

- [ ] Save/Load system
- [ ] Seamless scene restart without flicker or state desync
- [ ] ESC key toggle pause menu from anywhere
- [ ] Fix restart bug causing camera/player desync or jitter

## 🐛 Known Issues

- Restarting the game via SceneManager.LoadScene sometimes causes a brief desync between camera and player.
- Fixed platforms may occasionally stutter when combined with manual Rigidbody control.

---

## Author

Built with ❤️ by *[ME]*  
For personal learning and gameplay systems prototyping.


