using Tools.Pool;
using UnityEngine;
using View;

namespace TowerSpells.Barrage
{
    public class BarrageProjectile : PoolableObject
    {
        [SerializeField] private float flightTime = 0.5f;
        [SerializeField] private float arcHeight = 4.0f;

        private EnemyView target;
        private int damage;

        private Vector3 startPos;
        private float timer;

        public void Init(EnemyView enemyView, int damageValue, Vector3 spawnPosition)
        {
            target = enemyView;
            damage = damageValue;
            startPos = spawnPosition;

            transform.position = spawnPosition;

            timer = 0f;
        }

        private void Update()
        {
            if (target == null)
            {
                ReturnToPool();
                return;
            }

            timer += Time.deltaTime;

            var progress = timer / flightTime;

            if (progress >= 1f)
            {
                HitTarget();
                return;
            }

            var targetPos = target.transform.position;
            var position = Vector3.Lerp(startPos, targetPos, progress);
            position.y += Mathf.Sin(progress * Mathf.PI) * arcHeight;

            transform.position = position;
        }

        private void HitTarget()
        {
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            ReturnToPool();
        }

        public override void OnSpawn()
        {
        }

        public override void OnDespawn()
        {
            target = null;
        }
    }
}