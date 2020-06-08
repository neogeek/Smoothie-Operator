using System.Collections;
using UnityEngine;

namespace SmoothieOperator
{

    public class TimerController : MonoBehaviour, IPausable
    {

        private readonly WaitForSecondsRealtime _delayTimer = new WaitForSecondsRealtime(1);

#pragma warning disable CS0649
        [SerializeField]
        private TimerReference _timerReference;
#pragma warning restore CS0649

        private Coroutine _timerCoroutine;

        private void Start()
        {

            Resume();

        }

        private IEnumerator Tick()
        {

            while (true)
            {

                yield return _delayTimer;

                _timerReference.timer += 1;

            }

        }

        public void Resume()
        {

            _timerCoroutine = StartCoroutine(Tick());

        }

        public void Pause()
        {

            if (_timerCoroutine == null)
            {
                return;
            }

            StopCoroutine(_timerCoroutine);

            _timerCoroutine = null;

        }

    }

}
