using UnityEngine;

namespace TowerSpells.Fireball
{
    public class BurnEffect : MonoBehaviour
    {
        private Enemy enemy;

        private float duration;
        private float tickRate;

        private float damage;

        private float durationTimer;
        private float tickTimer;

        public void Init(
            Enemy target,
            float burnDuration,
            float burnTickInterval,
            float burnTickDamage)
        {
            enemy = target;

            duration = burnDuration;
            tickRate = burnTickInterval;
            damage = burnTickDamage;

            durationTimer = 0f;
            tickTimer = 0f;
        }

        private void Update()
        {
            if (enemy == null)
            {
                Destroy(this);
                return;
            }

            durationTimer += Time.deltaTime;
            tickTimer += Time.deltaTime;

            if (tickTimer >= tickRate)
            {
                tickTimer = 0f;

                enemy.TakeDamage(damage);
            }

            if (durationTimer >= duration)
            {
                Destroy(this);
            }
        }
    }
}