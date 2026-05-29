using UnityEngine;
using UnityEngine.UI;

public class GameplayHUD : MonoBehaviour
{
    [Header("UI Элементы")]
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TowerService towerService;

    void Start()
    {
        GameStateService.Instance.OnGameStateChanged += HandleGameStateChanged;
        towerService.OnHealthChanged += UpdateHealthBar;

        gameOverPanel.SetActive(false);
    }

    void OnDestroy()
    {
        GameStateService.Instance.OnGameStateChanged -= HandleGameStateChanged;
        towerService.OnHealthChanged -= UpdateHealthBar;
    }

    private void HandleGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Lose:
                ShowGameOverScreen();
                break;
        }
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarSlider.maxValue = towerService.MaxHealth;
        healthBarSlider.value = currentHealth;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, healthBarFill.fillAmount);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }
}