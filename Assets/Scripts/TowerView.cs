using System;
using TowerSpells.Fireball;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    [SerializeField] private TowerService towerService;
    [SerializeField] private BarrageSpell barrageSpell;
    [SerializeField] private FireballSpell fireballSpell;
    
    public event Action<float> OnTakeDamage;

    public void TakeDamage(float damage)
    {
        OnTakeDamage?.Invoke(damage);
    }
}