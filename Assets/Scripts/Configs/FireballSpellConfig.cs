using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "FireballSpellConfig",
        menuName = "Game/Spells/Fireball Spell Config")]
    public class FireballSpellConfig : ScriptableObject
    {
        [Header("EnemySearch")]
        public float Radius = 10f;

        [Header("Cast")]
        public float CastIntervalSec = 3.0f;

        [Header("Movement")]
        public float Speed = 12.0f;
        public float MaxLifetimeSec = 5.0f;

        [Header("Collision")]
        public float СollisionRadius = 1.0f;

        [Header("Explosion")]
        public float Damage = 25.0f;
        public float ExplosionRadius = 3.0f;

        [Header("Burn")]
        public float DurationSec = 5.0f;
        public float TickRateSec = 1.0f;
        public float DamagePerTick = 5.0f;
    }
}