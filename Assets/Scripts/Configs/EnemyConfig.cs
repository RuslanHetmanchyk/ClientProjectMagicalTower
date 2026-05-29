using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Game/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public EnemyType Type;

        [Header("Stats")]
        public float MaxHealth = 100;
        public float MoveSpeed = 3f;
        public float AttackDamage = 10;
        public float AttackDistance = 10;
        public float AttackCooldown = 10;
    }

    public enum EnemyType
    {
        Normal,
        Fast,
        Big
    }
}