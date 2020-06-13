using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothieOperator
{

    public class GameController : MonoBehaviour
    {

        public enum STATE
        {

            PLAYING,

            PAUSED,

            GAME_OVER

        }

#pragma warning disable CS0649
        [SerializeField]
        private Canvas _pausedCanvas;

        [SerializeField]
        private Canvas _gameOverCanvas;

        [SerializeField]
        private PlayerReference _playerReference;

        [SerializeField]
        private TimerReference _timerReference;

        [SerializeField]
        private HighScoreReference _highScoreReference;
#pragma warning restore CS0649

        private STATE _currentState = STATE.PLAYING;

        private void Start()
        {

            _playerReference.Reset();
            _timerReference.Reset();

            _highScoreReference.Load();

        }

        public void Pause()
        {

            if (!_currentState.Equals(STATE.PLAYING))
            {
                return;
            }

            _currentState = STATE.PAUSED;

            Time.timeScale = 0;

            foreach (var pausable in FindObjectsOfType<MonoBehaviour>().OfType<IPausable>())
            {

                pausable.Pause();

            }

            _pausedCanvas.enabled = true;

        }

        public void Resume()
        {

            if (_currentState.Equals(STATE.PLAYING))
            {
                return;
            }

            _currentState = STATE.PLAYING;

            Time.timeScale = 1;

            foreach (var pausable in FindObjectsOfType<MonoBehaviour>().OfType<IPausable>())
            {

                pausable.Resume();

            }

            _pausedCanvas.enabled = false;

        }

        private void OnApplicationFocus(bool hasFocus)
        {

            if (!hasFocus)
            {

                Pause();

            }

        }

        public void GameOver(int score)
        {

            _currentState = STATE.GAME_OVER;

            Time.timeScale = 0;

            _gameOverCanvas.enabled = true;

            _highScoreReference.AddScore("AAA", score);
            _highScoreReference.Save();

        }

        public void ReloadLevel()
        {

            SceneManager.LoadScene("Game");

        }

        public void OnPause()
        {

            Pause();

        }

        public void OnStart()
        {

            if (_currentState.Equals(STATE.GAME_OVER))
            {

                ReloadLevel();

            }
            else
            {

                Resume();

            }

        }

    }

}
