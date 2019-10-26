using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace SmoothieOperator
{

    [SelectionBaseAttribute]
    public class CustomerController : MonoBehaviour
    {

        private readonly WaitForSecondsRealtime _delayTimer = new WaitForSecondsRealtime(1);

        private const int SECONDS_REMAINING_BEFORE_SHOWING_TIMER = 30;

        public IEnumerable<Sprite> fruits => _order.fruits.Length > 0 ? Array.AsReadOnly(_order.fruits) : null;

#pragma warning disable CS0649
        [SerializeField]
        private Order _order;

        [SerializeField]
        private Sprite[] _fruitSprites;

        [SerializeField]
        private SpriteRenderer[] _fruitSpriteRenderers;

        [SerializeField]
        private Text _timerTextComp;
#pragma warning restore CS0649

        private int _timer = 60;

        private void Awake()
        {

            _order = new Order
            {
                fruits = new[]
                {
                    _fruitSprites[Random.Range(0, _fruitSprites.Length)],
                    _fruitSprites[Random.Range(0, _fruitSprites.Length)],
                    _fruitSprites[Random.Range(0, _fruitSprites.Length)]
                }
            };

        }

        private IEnumerator Start()
        {

            _timerTextComp.enabled = false;

            for (var i = 0; i < _fruitSpriteRenderers.Length; i += 1)
            {

                _fruitSpriteRenderers[i].sprite = _order.fruits[i];

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

        public bool CanFruitsFulfillOrder(IEnumerable<Sprite> fruitsInBlender)
        {

            return _order.fruits.Except(fruitsInBlender).ToArray().Length.Equals(0);

        }

    }

}
