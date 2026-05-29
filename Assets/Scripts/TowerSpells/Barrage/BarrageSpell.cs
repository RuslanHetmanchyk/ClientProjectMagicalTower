using Pool;
using TowerSpells.Barrage;
using UnityEngine;

public class BarrageSpell : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] private float radius = 6f;
    [SerializeField] private LayerMask enemyMask;

    [Header("References")]
    [SerializeField] private ComponentPool projectilePool;
    [SerializeField] private Transform shootPoint;

    [Header("Damage")]
    [SerializeField] private int damage = 10;
    
    [Header("Cast")]
    [SerializeField] private float castInterval = 2f;

    private float castTimer;
    
    private readonly Collider[] overlapResults = new Collider[64];

    private void Update()
    {
        castTimer += Time.deltaTime;

        if (castTimer >= castInterval)
        {
            castTimer = 0f;

            Cast();
        }
    }

    public void Cast()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            shootPoint.position,
            radius,
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
                damage,
                shootPoint.position);
        }
    }
}