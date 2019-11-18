using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SmoothieOperator
{

    [SelectionBaseAttribute]
    public class CustomerController : MonoBehaviour
    {

        private const int SECOND_BEFORE_CUSTOMER_LEAVES = 60;

        private readonly WaitForSecondsRealtime _delayTimer = new WaitForSecondsRealtime(1);

        private readonly WaitForSecondsRealtime _delayExitAnimation = new WaitForSecondsRealtime(2);

        public Order order;

        public delegate void CustomerEventHandler(CustomerController customerController);

        public CustomerEventHandler OrderCanceledEvent;

        public CustomerEventHandler OrderFulFilledEvent;

#pragma warning disable CS0649
        [SerializeField]
        private SpriteRenderer[] _fruitSpriteRenderers;

        [SerializeField]
        private Text _timerTextComp;
#pragma warning restore CS0649

        private int _timer = SECOND_BEFORE_CUSTOMER_LEAVES;

        private IEnumerator Start()
        {

            for (var i = 0; i < _fruitSpriteRenderers.Length; i += 1)
            {

                _fruitSpriteRenderers[i].sprite = order.fruits[i].fruit;

            }

            _timerTextComp.enabled = false;

            while (_timer > 0)
            {

                yield return _delayTimer;

                _timer -= 1;

                if (_timer <= SECOND_BEFORE_CUSTOMER_LEAVES / 2 && !_timerTextComp.enabled)
                {

                    _timerTextComp.enabled = true;

                }

                _timerTextComp.text = _timer.ToString();

            }

            StartCoroutine(OrderCanceled());

        }

        private IEnumerator OrderCanceled()
        {

            yield return _delayExitAnimation;

            OrderCanceledEvent?.Invoke(this);

            Destroy(gameObject);

        }

        private IEnumerator OrderFulFilled()
        {

            yield return _delayExitAnimation;

            OrderFulFilledEvent?.Invoke(this);

            Destroy(gameObject);

        }

    }

}
