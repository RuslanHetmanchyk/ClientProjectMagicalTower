using System;
using UnityEngine;

public class TowerService : MonoBehaviour
{
    [SerializeField] private TowerView towerView;
    
    public event Action<float, float> OnHealthChanged;
    public event Action OnGameOver;

    public TowerView TowerView => towerView;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Start()
    {
        Init(1000f);
    }

    private void Init(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        IsGameOver = false;
    }

    public void TakeDamage(float damage)
    {
        if (IsGameOver) return;

        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;

        // Вызываем колбэк, если на него кто-то подписался
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }
}