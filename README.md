# Hyper-Casual Core Loop Prototype

## Unity Version
Unity 6.0.0f1 (6000.0.30f1)

## Platform
Android (Portrait)

---

## Core Gameplay

This is a hyper-casual endless runner prototype built around a simple and addictive core loop.

- Hold the screen to run forward
- Release to stop and plan
- Tap the **JUMP** button to avoid obstacles
- The goal is to survive as long as possible and achieve a high score

The gameplay encourages players to **stop, think, and time their jumps**, instead of relying purely on fast reflexes.

---

## Core Loop

Run → Avoid Obstacles → Score Increases → Difficulty Ramps → Fail → Restart

This loop is designed for short play sessions and strong replayability.

---

## Controls

- **Hold Screen** → Run forward  
- **Release** → Stop movement  
- **Tap JUMP** → Jump over obstacles  

---

## Difficulty Ramp

Difficulty increases gradually based on distance traveled:

- Player movement speed increases over time
- Obstacle spacing becomes tighter as distance increases
- Coins spawn less frequently at higher difficulty
- Reduced reaction time increases challenge without adding complex controls

This ensures the game remains accessible while becoming progressively challenging.

---

## Architecture Overview

The project follows clear responsibility separation:

- **GameManager**  
  Handles menu state, gameplay start, and game over flow

- **PlayerController**  
  Manages player movement, jumping, and difficulty-based speed scaling

- **ObstacleSpawner**  
  Distance-based obstacle spawning with controlled spacing and difficulty scaling

- **GroundLooper**  
  Infinite ground recycling system

- **ScoreManager**  
  Distance-based score tracking

- **EconomyManager**  
  Soft currency (coins) management

- **Coin**  
  Coin pickup logic

All systems are modular and easy to extend.

---

## Performance Strategy

- Object pooling for obstacles
- Distance-based spawning instead of time-based spawning
- Minimal physics usage
- No per-frame instantiation of obstacles
- Lightweight `Update()` logic

These choices ensure smooth performance on mobile devices.

---

## Economy Design

### Soft Currency
- Coins earned during gameplay
- Rewards players for distance and survival
- Stored locally using `PlayerPrefs`

### Usage
- Designed to support progression and replay motivation
- Can be extended for revives, boosters, or cosmetic unlocks

No pay-to-win mechanics are implemented.

---

## Known Limitations & Future Improvements

With additional time, the following could be added:

- Fake IAP flow (revive, score boosters)
- Coin object pooling
- Additional obstacle types
- Visual and audio feedback
- Basic analytics hooks

---

## Notes

- Visual polish was intentionally kept minimal
- Focus was placed on core gameplay loop, performance, and clean architecture
- Project is structured to be production-ready and scalable
