using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    public class GameController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private Canvas _pausedCanvas;

        [SerializeField]
        private PlayerReference _playerReference;
#pragma warning restore CS0649

        private void Start()
        {

            _playerReference.Reset();

        }

        public void Pause()
        {

            Time.timeScale = 0;

            foreach (var pausable in FindObjectsOfType<MonoBehaviour>().OfType<IPausable>())
            {

                pausable.Pause();

            }

            _pausedCanvas.enabled = true;

        }

        public void Resume()
        {

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

    }

}
