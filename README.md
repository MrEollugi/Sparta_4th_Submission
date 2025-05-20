# Submission Prototype

A Unity prototype featuring player movement, item interaction, and modular gameplay systems — built as part of a multi-feature gameplay demo.

---

## Features

### Core Mechanics
- **Player Movement & Jumping** using Rigidbody physics
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
- **Health Bar UI** dynamically updates with HP
- **Inventory UI** with icon and selection indicator
- **UIManager** centralizes all UI components
- **Inspect UI** shows object info when looking at items

### Environment Interactives
- Jump pads that launch the player upward on contact

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

## Upcoming Tasks

- [ ] 2-1: Stamina & status UI
- [ ] 2-8: Interactable UI prompts (e.g. "Press E to open")
- [ ] More item types (e.g. shield, double jump, etc.)
- [ ] Saving/Loading

---

## Author

Built with ❤️ by *[ME]*  
For personal learning and gameplay systems prototyping.


