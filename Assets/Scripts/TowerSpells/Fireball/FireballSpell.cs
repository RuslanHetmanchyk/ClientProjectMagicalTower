using Configs;
using Pool;
using UnityEngine;

namespace TowerSpells.Fireball
{
    public class FireballSpell : MonoBehaviour
    {
        [SerializeField] private FireballSpellConfig config;    
        
        [Header("Search")]
        [SerializeField] private LayerMask enemyMask;

        [Header("References")]
        [SerializeField] private ComponentPool projectilePool;
        [SerializeField] private Transform shootPoint;

        private readonly Collider[] overlapResults = new Collider[64];

        private float timer;

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= config.CastIntervalSec)
            {
                timer = 0f;

                Cast();
            }
        }

        private void Cast()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(
                shootPoint.position,
                config.Radius,
                overlapResults,
                enemyMask);

            if (hitCount == 0)
            {
                return;
            }

            var randomIndex = Random.Range(0, hitCount);
            var targetCollider = overlapResults[randomIndex];
            if (!targetCollider.TryGetComponent(out Enemy enemy))
            {
                return;
            }

            var direction = (enemy.transform.position - shootPoint.position).normalized;
            direction.Normalize();

            var projectile = projectilePool.Get<FireballProjectile>();
            projectile.MoveSpeed = config.Speed;
            projectile.MaxLifetimeSec = config.MaxLifetimeSec;
            projectile.CollisionRadius = config.СollisionRadius;
            projectile.ExplosionRadius = config.ExplosionRadius;
            projectile.Damage = config.Damage;
            projectile.BurnDurationSec = config.DurationSec;
            projectile.BurnTickRate = config.TickRateSec;
            projectile.DamagePerTick = config.DamagePerTick;
            
            projectile.Launch(shootPoint.position, direction);
        }
    }
}