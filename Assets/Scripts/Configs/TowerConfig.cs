using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "TowerConfig",
        menuName = "Game/Tower Config")]
    public class TowerConfig : ScriptableObject
    {
        [Header("Stats")]
        public int MaxHealth = 100;
    }
}