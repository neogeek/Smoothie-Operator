using System.Collections;
using UnityEngine;

namespace SmoothieOperator
{

    public class TimerController : MonoBehaviour
    {

        private readonly WaitForSecondsRealtime _delayTimer = new WaitForSecondsRealtime(1);

#pragma warning disable CS0649
        [SerializeField]
        private TimerReference _timerReference;
#pragma warning restore CS0649

        private Coroutine _timerCoroutine;

        private void Awake()
        {

            _timerReference.Reset();

            StartTimer();

        }

        public void StartTimer()
        {

            _timerCoroutine = StartCoroutine(Tick());

        }

        private IEnumerator Tick()
        {

            while (true)
            {

                yield return _delayTimer;

                _timerReference.timer += 1;

            }

        }

        public void StopTimer()
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
