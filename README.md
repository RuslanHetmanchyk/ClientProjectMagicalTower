# Magical Tower Defense (Unity 6)

A small prototype of a 3D auto-battler / survivors-like tower defense game made with Unity 6.

The project focuses on:

* scalable gameplay architecture
* reusable pooling system
* data-driven configs via ScriptableObjects
* modular spell system
* clean separation between gameplay logic and game data

---

# Features

## Enemies

* Multiple enemy types:

  * Normal
  * Fast
  * Big
* Random enemy spawning
* Configurable enemy stats
* Object pooling for all enemies

## Combat System

* Automatic spell casting
* Projectile-based combat
* Area damage
* Damage over time effects (Burn)
* Floating damage text

## Spells

### Barrage

Creates projectiles for every enemy in range.

### Fireball

Launches a projectile toward a random enemy direction.
The projectile explodes on impact and applies burn damage over time.

## UI / Feedback

* Floating world-space damage numbers
* Billboard text facing the camera

---

# Architecture

The project follows a modular and data-driven architecture.

## Core Principles

### ScriptableObject Configs

Gameplay values are stored in ScriptableObjects instead of hardcoded inside MonoBehaviours.

This allows:

* easier balancing
* reusable gameplay logic
* quick iteration without code changes
* scalable upgrade systems

Example configs:

* EnemyConfig
* FireballSpellConfig
* BarrageSpellConfig
* BurnEffectConfig
* TowerConfig

---

### Reusable Generic Object Pool

A reusable pool system is used for:

* enemies
* projectiles
* floating damage texts
* future VFX

The pool is implemented as a reusable MonoBehaviour component configurable directly from the inspector.

Benefits:

* reduced allocations
* lower GC spikes
* better runtime performance
* scalable combat systems

---

### Separation of Responsibilities

Systems are intentionally separated:

| System           | Responsibility                        |
| ---------------- | ------------------------------------- |
| Enemy            | Enemy gameplay logic                  |
| EnemyConfig      | Enemy data                            |
| EnemySpawner     | Enemy creation                        |
| Spell            | Spell casting logic                   |
| Projectile       | Projectile movement and hit detection |
| BurnEffect       | Damage over time logic                |
| GameStateService | Global game state                     |
| ComponentPool    | Object pooling                        |

This keeps the project maintainable as it grows.

---

# Configs & Balancing

All gameplay configs are located in:

Assets/Configs/

Main configs:

* EnemyConfig
* FireballSpellConfig
* BarrageSpellConfig
* BurnEffectConfig
* TowerConfig

## Example Balancing Workflow

To balance enemy HP:

1. Open EnemyConfig asset
2. Change:

   * Max HP
   * Move Speed
   * Damage

To balance spells:

1. Open FireballSpellConfig or BarrageSpellConfig
2. Adjust:

   * Damage
   * Radius
   * Cast interval
   * Projectile speed
   * Explosion radius

To balance burn damage:

1. Open BurnEffectConfig
2. Adjust:

   * Duration
   * Tick rate
   * Damage per tick

No code changes are required for balancing.

---

# Technical Notes

## Physics

Projectile movement is implemented manually without Rigidbody for:

* deterministic movement
* lower physics overhead
* better scalability for large projectile counts

Collision detection uses:

* SphereCast
* OverlapSphereNonAlloc

to reduce allocations and improve performance.

---

## Damage System

All damageable entities implement a shared damage interface pattern.

This allows:

* enemies
* towers
* future destructible objects

to work with the same combat systems.

---

# Unity Version

Unity 6.3+


---

# Author

Ruslan Hetmanchyk

GitHub:
https://github.com/RuslanHetmanchyk
