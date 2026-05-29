using System;
using GameplaySevices;
using TowerSpells.Barrage;
using TowerSpells.Fireball;
using UnityEngine;

namespace View
{
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
}