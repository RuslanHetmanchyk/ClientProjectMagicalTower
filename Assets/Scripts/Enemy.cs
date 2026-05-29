using Pool;
using TMPro;
using UnityEngine;

public class Enemy : PoolableObject
{
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private ComponentPool damageTextPool;
    
    private float moveSpeed = 3f;
    private float attackDamage = 10f;
    private float attackDistance = 2.5f;
    private float attackCooldown = 2f;
    private float maxHealth = 100f;
    
    public TowerView Target { get; set; }

    private float nextAttackTime;
    private float currentHealth;

    void Update()
    {
        if (Target == null) return;

        Vector3 targetPosition = new Vector3(Target.transform.position.x, 0.0f, Target.transform.position.z);
        float distance = Vector3.Distance(transform.position, targetPosition);

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
        // Поворот в сторону башни
        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        // Движение вперед
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private void TryAttack()
    {
        if (Time.time >= nextAttackTime && Target != null)
        {
            // Вызываем урон у View, а View передаст его в Service
            Target.TakeDamage(attackDamage);
            nextAttackTime = Time.time + attackCooldown; 
        }
    }
    
    public void TakeDamage(float damage)
    {
        //if (IsGameOver) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        // Вызываем колбэк, если на него кто-то подписался
        //OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        healthText.text = $"{currentHealth}/{maxHealth}";
        
        ShowDamage((int)damage, transform.position);
    }
    
    [SerializeField] private Vector3 offset =
        new Vector3(0f, 1.5f, 0f);
    public void ShowDamage(int damage, Vector3 worldPosition)
    {
        var damageText = damageTextPool.Get<DamageText>();

        damageText.Show(damage, worldPosition + offset);
    }

    public override void OnSpawn()
    {
    }

    public override void OnDespawn()
    {
    }
}