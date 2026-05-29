using Pool;
using UnityEngine;

namespace TowerSpells.Fireball
{
    public class FireballSpell : MonoBehaviour
    {
        [Header("Search")]
        [SerializeField] private float radius = 10f;
        [SerializeField] private LayerMask enemyMask;

        [Header("References")]
        [SerializeField] private ComponentPool projectilePool;
        [SerializeField] private Transform shootPoint;

        [Header("Cast")]
        [SerializeField] private float castInterval = 3f;

        private readonly Collider[] overlapResults = new Collider[64];

        private float timer;

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= castInterval)
            {
                timer = 0f;

                Cast();
            }
        }

        private void Cast()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(
                shootPoint.position,
                radius,
                overlapResults,
                enemyMask);

            if (hitCount == 0)
                return;

            int randomIndex =
                Random.Range(0, hitCount);

            Collider targetCollider =
                overlapResults[randomIndex];

            if (!targetCollider.TryGetComponent(
                    out Enemy enemy))
            {
                return;
            }

            Vector3 direction =
                (enemy.transform.position - shootPoint.position)
                .normalized;

            direction.Normalize();

            FireballProjectile projectile =
                projectilePool.Get<FireballProjectile>();

            projectile.Launch(
                shootPoint.position,
                direction);
        }
    }
}