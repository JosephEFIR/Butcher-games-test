using System;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Game
{
    public class GameController
    {
        private readonly GameModel _model;
        private readonly FinishSystem _finishSystem;

        public event Action<EGameState> OnStateChanged;

        public GameController(GameModel model, FinishSystem finishSystem)
        {
            _model = model;
            _finishSystem = finishSystem;
        }

        public void StartGame() => ChangeState(EGameState.Gameplay);
        public void Lose() => ChangeState(EGameState.Lose);

        public void OnFinishReached()
        {
            if (_finishSystem.IsWin)
                ChangeState(EGameState.Win);
            else
                ChangeState(EGameState.Lose);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel()
        {
            int next = SceneManager.GetActiveScene().buildIndex + 1;
            if (next < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(next);
            else
                SceneManager.LoadScene(0);
        }

        private void ChangeState(EGameState newState)
        {
            if (_model.CurrentState == newState) return;
            _model.CurrentState = newState;
            OnStateChanged?.Invoke(newState);
        }
    }
}