using Tools.Pool;
using UnityEngine;
using View;

namespace TowerSpells.Fireball
{
    public class FireballProjectile : PoolableObject
    {
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private LayerMask enemyMask;

        public float MoveSpeed { get; set; } = 12f;
        public float MaxLifetimeSec { get; set; } = 5.0f;
        public float CollisionRadius { get; set; } = 0.3f;
        public float ExplosionRadius { get; set; } = 4.0f;
        public float Damage { get; set; } = 25.0f;
        public float BurnDurationSec { get; set; } = 4.0f;
        public float BurnTickRate { get; set; } = 1.0f;
        public float DamagePerTick { get; set; } = 5.0f;

        private readonly Collider[] overlapResults = new Collider[32];

        private Vector3 direction;

        private float timer;

        public void Launch(Vector3 startPosition, Vector3 moveDirection)
        {
            transform.position = startPosition;
            direction = moveDirection.normalized;

            timer = 0f;

            gameObject.SetActive(true);
        }

        private void Update()
        {
            var moveDistance = MoveSpeed * Time.deltaTime;
            var currentPosition = transform.position;
            var hit = Physics.SphereCast(
                currentPosition,
                CollisionRadius,
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

            transform.position += direction * moveDistance;
            transform.forward = direction;

            timer += Time.deltaTime;
            if (timer >= MaxLifetimeSec)
            {
                ReturnToPool();
            }
        }

        private void Explode()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                ExplosionRadius,
                overlapResults,
                enemyMask);

            for (var i = 0; i < hitCount; i++)
            {
                var hit = overlapResults[i];
                if (!hit.TryGetComponent(out EnemyView enemy))
                {
                    continue;
                }

                enemy.TakeDamage(Damage);

                ApplyBurn(enemy);
            }

            ReturnToPool();
        }

        private void ApplyBurn(EnemyView enemyView)
        {
            var burnEffect = enemyView.GetComponent<BurnEffect>();
            if (burnEffect == null)
            {
                burnEffect = enemyView.gameObject.AddComponent<BurnEffect>();
            }

            burnEffect.Init(enemyView, BurnDurationSec, BurnTickRate, DamagePerTick);
        }

        public override void OnSpawn()
        {
        }

        public override void OnDespawn()
        {
        }
    }
}