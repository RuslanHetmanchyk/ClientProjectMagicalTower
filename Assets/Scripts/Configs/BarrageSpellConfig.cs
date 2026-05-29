using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "BarrageSpellConfig",
        menuName = "Game/Spells/Barrage Spell Config")]
    public class BarrageSpellConfig : ScriptableObject
    {
        [Header("Search")]
        public float Radius = 12f;

        [Header("Damage")]
        public int Damage = 10;

        [Header("Cast")]
        public float CastIntervalSec = 2f;
    }
}