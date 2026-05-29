using Pool;
using UnityEngine;

namespace TowerSpells.Fireball
{
    public class FireballProjectile : PoolableObject
    {
        [Header("Movement")]
        [SerializeField] private float speed = 12f;
        [SerializeField] private float maxLifetime = 5f;

        [Header("Collision")]
        [SerializeField] private float collisionRadius = 0.3f;
        [SerializeField] private LayerMask collisionMask;

        [Header("Explosion")]
        [SerializeField] private float explosionRadius = 4f;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private int damage = 25;

        [Header("Burn")]
        [SerializeField] private float burnDuration = 4f;
        [SerializeField] private float burnTickRate = 1f;
        [SerializeField] private int burnDamage = 5;

        private readonly Collider[] overlapResults =
            new Collider[32];

        private Vector3 direction;

        private float timer;

        public void Launch(
            Vector3 startPosition,
            Vector3 moveDirection)
        {
            transform.position = startPosition;

            direction = moveDirection.normalized;

            timer = 0f;

            gameObject.SetActive(true);
        }

        private void Update()
        {
            float moveDistance =
                speed * Time.deltaTime;

            Vector3 currentPosition =
                transform.position;

            bool hit = Physics.SphereCast(
                currentPosition,
                collisionRadius,
                direction,
                out RaycastHit hitInfo,
                moveDistance,
                collisionMask);

            if (hit)
            {
                transform.position = hitInfo.point;

                Explode();

                return;
            }

            transform.position +=
                direction * moveDistance;

            transform.forward = direction;

            timer += Time.deltaTime;

            if (timer >= maxLifetime)
            {
                ReturnToPool();
            }
        }

        private void Explode()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                explosionRadius,
                overlapResults,
                enemyMask);

            for (int i = 0; i < hitCount; i++)
            {
                Collider hit =
                    overlapResults[i];

                if (!hit.TryGetComponent(out Enemy enemy))
                    continue;

                enemy.TakeDamage(damage);

                ApplyBurn(enemy);
            }

            ReturnToPool();
        }

        private void ApplyBurn(Enemy enemy)
        {
            BurnEffect burn =
                enemy.GetComponent<BurnEffect>();

            if (burn == null)
            {
                burn =
                    enemy.gameObject
                        .AddComponent<BurnEffect>();
            }

            burn.Init(
                enemy,
                burnDuration,
                burnTickRate,
                burnDamage);
        }

        public override void OnSpawn()
        {
        }

        public override void OnDespawn()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(
                transform.position,
                explosionRadius);
        }
    }
}