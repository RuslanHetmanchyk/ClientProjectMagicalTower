using System;
using Configs;
using UnityEngine;

public class TowerService : MonoBehaviour
{
    [SerializeField] private TowerConfig config;
    [SerializeField] private TowerView towerView;
    
    public event Action<float, float> OnHealthChanged;

    public TowerView TowerView => towerView;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Start()
    {
        GameStateService.Instance.OnGameStateChanged += HandleGameStateChanged;
        towerView.OnTakeDamage += TakeDamage;
        
        InitHealth();
    }

    private void OnDestroy()
    {
        GameStateService.Instance.OnGameStateChanged -= HandleGameStateChanged;
        towerView.OnTakeDamage -= TakeDamage;
    }

    private void HandleGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Gameplay:
                InitHealth();
                break;
        }
    }

    private void InitHealth()
    {
        MaxHealth = config.MaxHealth;
        CurrentHealth = MaxHealth;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

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
            GameStateService.Instance.LoseGame();
        }
    }
}