using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    [SelectionBaseAttribute]
    public class CustomerController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private Order _order;

        [SerializeField]
        private GameObject _armsWait;

        [SerializeField]
        private GameObject _armsVictory;

        [SerializeField]
        private GameObject _orderBubble;

        [SerializeField]
        private Sprite[] _fruitSprites;

        [SerializeField]
        private SpriteRenderer[] _fruitSpriteRenderers;
#pragma warning restore CS0649

        private void Awake()
        {

            _armsVictory.SetActive(false);

            _order = new Order
            {
                fruits = new[]
                {
                    _fruitSprites[Random.Range(0, _fruitSprites.Length)],
                    _fruitSprites[Random.Range(0, _fruitSprites.Length)],
                    _fruitSprites[Random.Range(0, _fruitSprites.Length)]
                }
            };

            for (var i = 0; i < _fruitSpriteRenderers.Length; i += 1)
            {

                _fruitSpriteRenderers[i].sprite = _order.fruits[i];

            }

        }

        public bool CanFruitsFulfillOrder(IEnumerable<Sprite> fruits)
        {

            return _order.fruits.Except(fruits).ToArray().Length.Equals(0);

        }

    }

}
