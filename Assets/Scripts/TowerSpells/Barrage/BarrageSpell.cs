using Configs;
using Tools.Pool;
using UnityEngine;
using View;

namespace TowerSpells.Barrage
{
    public class BarrageSpell : MonoBehaviour
    {
        [SerializeField] private BarrageSpellConfig config;
    
        [Header("Search")]
        [SerializeField] private LayerMask enemyMask;

        [Header("References")]
        [SerializeField] private ComponentPool projectilePool;
        [SerializeField] private Transform shootPoint;

        private float castTimer;
    
        private readonly Collider[] overlapResults = new Collider[64];

        private void Update()
        {
            castTimer += Time.deltaTime;

            if (castTimer >= config.CastIntervalSec)
            {
                castTimer = 0f;

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

            for (var i = 0; i < hitCount; i++)
            {
                var hit = overlapResults[i];
                var enemy = hit.GetComponent<EnemyView>();
                if (enemy == null)
                {
                    continue;
                }

                var projectile = projectilePool.Get<BarrageProjectile>();
                projectile.Init(enemy, config.Damage, shootPoint.position);
            }
        }
    }
}