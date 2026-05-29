using System;
using UnityEngine;

namespace GameplaySevices
{
    public class GameStateService : MonoBehaviour
    {
        public static GameStateService Instance { get; private set; }

        public GameState CurrentState { get; private set; }

        public event Action<GameState> OnGameStateChanged;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            StartGameplay();
        }

        public void StartGameplay()
        {
            SetState(GameState.Gameplay);
            Time.timeScale = 1f;
        }

        public void WinGame()
        {
            SetState(GameState.Win);
        }

        public void LoseGame()
        {
            SetState(GameState.Lose);
            Time.timeScale = 0f;
        }

        public void PauseGame()
        {
            SetState(GameState.Pause);

            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;

            SetState(GameState.Gameplay);
        }

        private void SetState(GameState newState)
        {
            if (CurrentState == newState)
            {
                return;
            }

            CurrentState = newState;

            OnGameStateChanged?.Invoke(newState);
        }
    }

    public enum GameState
    {
        Gameplay,
        Pause,
        Lose,
        Win
    }
}