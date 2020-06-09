using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SmoothieOperator
{

    [SelectionBaseAttribute]
    public class CustomerController : MonoBehaviour, IPausable
    {

        private const int SECOND_BEFORE_CUSTOMER_LEAVES = 60;

        private const int MAX_POINTS_FOR_ORDER = 100;

        private static readonly int AnimationVictory = Animator.StringToHash("Victory");

        private readonly WaitForSeconds _delayTimer = new WaitForSeconds(1);

        private readonly WaitForSeconds _delayExitAnimation = new WaitForSeconds(2);

        public Order order;

        public delegate void CustomerEventHandler(CustomerController customerController);

        public CustomerEventHandler OrderCanceledEvent;

        public CustomerEventHandler OrderFulFilledEvent;

#pragma warning disable CS0649
        [SerializeField]
        private PlayerReference _playerReference;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private SpriteRenderer[] _fruitSpriteRenderers;

        [SerializeField]
        private Text _timerTextComp;
#pragma warning restore CS0649

        private Coroutine _timerCoroutine;

        private int _timer = SECOND_BEFORE_CUSTOMER_LEAVES;

        private int _points = MAX_POINTS_FOR_ORDER;

        private void Start()
        {

            for (var i = 0; i < _fruitSpriteRenderers.Length; i += 1)
            {

                _fruitSpriteRenderers[i].sprite = order.fruits[i].fruit;

            }

            _timerCoroutine = StartCoroutine(CustomerTimer());

        }

        private IEnumerator CustomerTimer()
        {

            _timerTextComp.enabled = false;

            while (_timer > 0)
            {

                yield return _delayTimer;

                _timer -= 1;

                _points -= 1;

                if (_timer <= SECOND_BEFORE_CUSTOMER_LEAVES / 2 && !_timerTextComp.enabled)
                {

                    _timerTextComp.enabled = true;

                }

                _timerTextComp.text = _timer.ToString();

            }

            OrderCanceled();

        }

        private void StopTimer()
        {

            if (_timerCoroutine == null)
            {
                return;
            }

            StopCoroutine(_timerCoroutine);

            _timerCoroutine = null;

        }

        private void OrderCanceled()
        {

            StopTimer();

            OrderCanceledEvent?.Invoke(this);

            Destroy(gameObject);

            _playerReference.LifeLost();

        }

        public IEnumerator OrderFulFilled()
        {

            StopTimer();

            _animator.SetTrigger(AnimationVictory);

            yield return _delayExitAnimation;

            OrderFulFilledEvent?.Invoke(this);

            _playerReference.AddPointsToScore(_points);

            Destroy(gameObject);

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

        public void Resume()
        {

            _timerCoroutine = StartCoroutine(CustomerTimer());

        }

    }

}
