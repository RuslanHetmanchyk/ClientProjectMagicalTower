using TMPro;
using Tools.Pool;
using UI;
using UnityEngine;

public class EnemyInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private ComponentPool damageTextPool;
    [SerializeField] private Vector3 offset = new (0f, 2.5f, 0f);

    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    public void ShowDamage(int damage, Vector3 worldPosition)
    {
        var damageText = damageTextPool.Get<DamageText>();
        damageText.Show(damage, worldPosition + offset);
    }
}
