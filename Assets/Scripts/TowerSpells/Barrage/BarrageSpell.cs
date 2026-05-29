using Configs;
using Pool;
using TowerSpells.Barrage;
using UnityEngine;

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

    public void Cast()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            shootPoint.position,
            config.Radius,
            overlapResults,
            enemyMask);

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = overlapResults[i];

            Enemy enemy = hit.GetComponent<Enemy>();

            if (enemy == null)
                continue;

            BarrageProjectile projectile =
                projectilePool.Get<BarrageProjectile>();

            projectile.Init(
                enemy,
                config.Damage,
                shootPoint.position);
        }
    }
}