using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SmoothieOperator
{

    [SelectionBaseAttribute]
    public class CustomerController : MonoBehaviour
    {

        private const int SECONDS_REMAINING_BEFORE_SHOWING_TIMER = 30;

        private const int SECOND_BEFORE_CUSTOMER_LEAVES = 60;

        private readonly WaitForSecondsRealtime _delayTimer = new WaitForSecondsRealtime(1);

        public Order order;

#pragma warning disable CS0649
        [SerializeField]
        private SpriteRenderer[] _fruitSpriteRenderers;

        [SerializeField]
        private Text _timerTextComp;
#pragma warning restore CS0649

        private int _timer = SECOND_BEFORE_CUSTOMER_LEAVES;

        private IEnumerator Start()
        {

            _timerTextComp.enabled = false;

            for (var i = 0; i < _fruitSpriteRenderers.Length; i += 1)
            {

                _fruitSpriteRenderers[i].sprite = order.fruits[i].fruit;

            }

            while (true)
            {

                yield return _delayTimer;

                _timer -= 1;

                if (_timer <= SECONDS_REMAINING_BEFORE_SHOWING_TIMER && !_timerTextComp.enabled)
                {

                    _timerTextComp.enabled = true;

                }

                _timerTextComp.text = _timer.ToString();

            }

        }

    }

}
