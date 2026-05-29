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
        public float CastIntervalSec = 3f;

        [Header("Movement")]
        public float Speed = 12f;

        public float MaxLifetimeSec = 5f;

        [Header("Collision")]
        public float СollisionRadius = 1.0f;

        [Header("Explosion")]
        public float ExplosionRadius = 3f;

        public int Damage = 25;

        [Header("BurnDuration")]
        public float DurationSec = 5f;

        [Header("BurnTick")]
        public float TickRateSec = 1f;

        [Header("BurnDamage")]
        public int DamagePerTick = 5;
    }
}