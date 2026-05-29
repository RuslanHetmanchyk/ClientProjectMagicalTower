using System;
using Configs;
using UnityEngine;

public class TowerService : MonoBehaviour
{
    [SerializeField] private TowerConfig config;
    [SerializeField] private TowerView towerView;
    
    public event Action<float, float> OnHealthChanged;
    public event Action OnGameOver;

    public TowerView TowerView => towerView;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Start()
    {
        Init();
        
        towerView.OnTakeDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        towerView.OnTakeDamage -= TakeDamage;
    }

    private void Init()
    {
        MaxHealth = config.MaxHealth;
        CurrentHealth = MaxHealth;
        IsGameOver = false;
    }

    public void TakeDamage(float damage)
    {
        if (IsGameOver)
        {
            return;
        }

        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        
        OnGameOver?.Invoke();
    }
}