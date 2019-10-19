using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    public class OrderManager : MonoBehaviour
    {

        private readonly WaitForSeconds _delayBetweenSpawningNewCustomers = new WaitForSeconds(1);

#pragma warning disable CS0649
        [SerializeField]
        private Transform[] _customerTransforms;

        [SerializeField]
        private GameObject[] _customerPrefabs;
#pragma warning restore CS0649

        private readonly Dictionary<Transform, CustomerController> _customers =
            new Dictionary<Transform, CustomerController>();

        private IEnumerator Start()
        {

            while (true)
            {

                yield return _delayBetweenSpawningNewCustomers;

                if (_customers.Count < _customerTransforms.Length)
                {

                    SpawnNewCustomer();

                }

            }

        }

        private void SpawnNewCustomer()
        {

            var spawnTransform = _customerTransforms.First(t => !_customers.ContainsKey(t));

            if (!spawnTransform)
            {
                return;
            }

            var spawnedCustomer = Instantiate(
                _customerPrefabs[Random.Range(0, _customerPrefabs.Length)],
                spawnTransform.position,
                Quaternion.identity,
                gameObject.transform);

            var customerController = spawnedCustomer.GetComponent<CustomerController>();

            _customers.Add(spawnTransform, customerController);

        }

        public Sprite FruitNeededByCustomerNotOnAvailable(HashSet<GameObject> availableFruits)
        {

            var availableFruitSprites = availableFruits
                .Select(f => f.GetComponent<SpriteRenderer>().sprite);

            var orderFruitSprites = _customers.SelectMany(c => c.Value.fruits);

            if (!orderFruitSprites.Any())
            {

                return null;

            }

            var fruitSprite = orderFruitSprites.Except(availableFruitSprites).ToArray().First();

            return fruitSprite;

        }

    }

}
