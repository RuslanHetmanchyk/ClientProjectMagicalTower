using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayHUD : MonoBehaviour
{
    [Header("UI Элементы")]
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TowerService towerService;

    void Start()
    {
        towerService.OnHealthChanged += UpdateHealthBar;
        towerService.OnGameOver += ShowGameOverScreen;
        
        healthBarSlider.maxValue = towerService.MaxHealth;
        
        UpdateHealthBar(towerService.CurrentHealth, towerService.MaxHealth);

        gameOverPanel.SetActive(false);
    }

    // Тот самый колбэк, который вызывается из сервиса
    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarSlider.value = currentHealth;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, healthBarFill.fillAmount);
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        towerService.OnHealthChanged -= UpdateHealthBar;
        towerService.OnGameOver -= ShowGameOverScreen;
    }
}