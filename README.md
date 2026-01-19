# Hyper-Casual Core Loop Prototype

---

## 1. Unity Version
**Unity 6.0.0f1 (6000.0.30f1)**

---

## 2. Platform
**Android (Portrait)**

---

## 3. Core Gameplay Loop
The game is a hyper-casual endless runner built around a **tap & hold to move / release to stop** core mechanic.  
The player holds the screen to move forward and releases to stop, timing jumps to avoid obstacles.

Difficulty ramps up naturally as distance increases by:
- Increasing player speed
- Reducing obstacle spacing

This creates a simple but skill-based core loop focused on timing and decision-making.

---

## 4. Controls
- **Hold Touch / Mouse** – Move forward  
- **Release Touch / Mouse** – Stop  
- **Jump Button** – Jump to avoid obstacles  

---

## 5. Game Flow
1. Main Menu  
2. Pre-run Booster Offer (shown only if player has enough gems)  
3. Gameplay  
4. Game Over  
5. Revive (Premium) or Restart (Scene Reload)  

---

## 6. Architecture Overview
The project follows a modular and responsibility-driven architecture:

- **GameManager**  
  Handles game states, canvas switching, revive flow, booster offers, and scene reload for clean restarts.

- **PlayerController**  
  Manages movement, jump logic, difficulty scaling, booster effects, and collision handling.

- **ObstacleSpawner**  
  Spawns obstacles and coins ahead of the player, manages lane-based obstacle variety, and safely despawns obstacles using pooling.

- **PoolManager**  
  Handles safe obstacle pooling by tracking active and available objects, preventing reuse of active obstacles.

- **CoinPool**  
  Manages coin pooling to avoid Instantiate/Destroy calls during gameplay, ensuring low GC pressure on mobile.

- **EconomyManager**  
  Manages soft currency (coins) and premium currency (gems), including mock purchase logic.

- **ReviveManager**  
  Controls revive logic with increasing gem costs per run.

- **AudioManager**  
  Handles looping background music and one-shot sound effects.

---

## 7. Performance Strategy (Mobile-Focused)
- **Object Pooling**
  - Obstacles and coins are pooled.
  - Active objects are never reused until returned to the pool.

- **GC Avoidance**
  - No Instantiate or Destroy calls during gameplay.
  - Coins and obstacles are recycled safely.

- **Update Usage**
  - Only essential gameplay logic runs per frame.
  - No unnecessary allocations inside Update.

These optimizations ensure smooth performance on low-end Android devices.

---

## 8. IAP Economy Design (Mock Implementation)

### 8.1 Soft Currency
- **Coins**
  - Earned through gameplay and coin pickups.
  - Used for score progression feedback.

### 8.2 Premium Currency
- **Gems**
  - Mock premium currency (no real payment SDK).
  - Used for revives and boosters.

### 8.3 Purchasable Items
1. **Revive**
   - Allows the player to continue from the same point after collision.
   - Cost increases per run (2, 4, 6, … gems).
   - Unlimited revives per run until gems are depleted.

2. **Power Booster**
   - Offered before the run starts if the player has enough gems.
   - Temporarily provides:
     - Increased movement speed
     - Score ×2
     - Coins ×2
     - Obstacle collision immunity
   - Booster status is clearly shown via UI text.

### 8.4 Balance Reasoning
The economy introduces a meaningful tradeoff between:
- Spending gems on **revives** to extend a run
- Spending gems on **boosters** to maximize score and rewards

This demonstrates monetization-aware design without interrupting gameplay.

---

## 9. Audio
- Looping background music
- One-shot sound effects:
  - Coin pickup
  - Jump (plays once per jump)
  - Failure / collision
- AudioManager persists across scene reloads

---

## 10. Known Limitations & Future Improvements
With more time, the following improvements could be explored:
- Additional booster types (shield, slow motion)
- More advanced difficulty scaling
- Visual feedback such as camera shake or effects
- Analytics hooks for retention and monetization testing

---

## 11. Summary
This prototype demonstrates a complete hyper-casual core loop with mobile-focused performance optimization, mock monetization systems, and scalable architecture.  
The project emphasizes simplicity, responsiveness, and production-ready practices suitable for real-world hyper-casual development.

---
