using UnityEngine;
using View;

namespace TowerSpells.Fireball
{
    public class BurnEffect : MonoBehaviour
    {
        private EnemyView enemyView;

        private float duration;
        private float tickRate;

        private float damage;

        private float durationTimer;
        private float tickTimer;

        public void Init(EnemyView target, float burnDuration, float burnTickInterval, float burnTickDamage)
        {
            enemyView = target;

            duration = burnDuration;
            tickRate = burnTickInterval;
            damage = burnTickDamage;

            durationTimer = 0f;
            tickTimer = 0f;
        }

        private void Update()
        {
            if (enemyView == null)
            {
                Destroy(this);
                return;
            }

            durationTimer += Time.deltaTime;
            tickTimer += Time.deltaTime;

            if (tickTimer >= tickRate)
            {
                tickTimer = 0f;

                enemyView.TakeDamage(damage);
            }

            if (durationTimer >= duration)
            {
                Destroy(this);
            }
        }
    }
}