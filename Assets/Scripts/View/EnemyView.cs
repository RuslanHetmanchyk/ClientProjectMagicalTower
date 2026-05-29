using Tools.Pool;
using UnityEngine;

namespace View
{
    public class EnemyView : PoolableObject
    {
        [SerializeField] private EnemyInfoUI enemyInfoUI;
    
        private float moveSpeed;
        private float attackDamage;
        private float attackDistance;
        private float attackCooldown;
        private float maxHealth;

        private float nextAttackTime;
        private float currentHealth;

        public TowerView Target { get; set; }

        void Update()
        {
            if (Target == null)
            {
                return;
            }

            var targetPosition = new Vector3(Target.transform.position.x, 0.0f, Target.transform.position.z);
            var distance = Vector3.Distance(transform.position, targetPosition);

            if (distance > attackDistance)
            {
                MoveToTarget(targetPosition);
            }
            else
            {
                TryAttack();
            }
        }

        public void Init(float maxHealth, float moveSpeed, float attackDamage, float attackDistance, float attackCooldown)
        {
            this.maxHealth = maxHealth;
            this.moveSpeed = moveSpeed;
            this.attackDamage = attackDamage;
            this.attackDistance = attackDistance;
            this.attackCooldown = attackCooldown;
        
            currentHealth = maxHealth;
        }

        private void MoveToTarget(Vector3 targetPos)
        {
            var direction = (targetPos - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        private void TryAttack()
        {
            if (Time.time >= nextAttackTime && Target != null)
            {
                Target.TakeDamage(attackDamage);
                nextAttackTime = Time.time + attackCooldown; 
            }
        }
    
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            
            enemyInfoUI.SetHealth(currentHealth, maxHealth);
            enemyInfoUI.ShowDamage((int)damage, transform.position);
        }
    
        public override void OnSpawn()
        {
        }

        public override void OnDespawn()
        {
        }
    }
}