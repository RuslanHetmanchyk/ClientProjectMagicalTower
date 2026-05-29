using TowerSpells.Fireball;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    // Ссылка на чистый сервис, которую потом заберет UI
    [SerializeField] private TowerService towerService;
    [SerializeField] private BarrageSpell barrageSpell;
    [SerializeField] private FireballSpell fireballSpell;

    void Start()
    {
        // Подписываемся на смерть башни, чтобы остановить игру
        towerService.OnGameOver += HandleGameOver;
    }

    // Этот метод по-прежнему вызывают враги
    public void TakeDamage(float damage)
    {
        towerService.TakeDamage(damage);
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0f; // Останавливаем игру
    }

    void OnDestroy()
    {
        towerService.OnGameOver -= HandleGameOver;
    }
    
    //--------
    [ContextMenu("CastBarrageSpell")]
    private void CastBarrageSpell()
    {
        Debug.Log("TEST CastBarrageSpell");
        barrageSpell.Cast();
    }
}