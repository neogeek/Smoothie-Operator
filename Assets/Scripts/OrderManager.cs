using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

                if (_customers.Count < _customerTransforms.Length)
                {

                    SpawnNewCustomer();

                }

                yield return _delayBetweenSpawningNewCustomers;

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

        public string FruitNeededByCustomerNotOnAvailable(IEnumerable<GameObject> availableFruits)
        {

            var availableFruitSpriteNames = availableFruits
                .Select(f => f.GetComponent<SpriteRenderer>().sprite.name.Replace("(Clone)", ""));

            var orderFruitSpriteNames = _customers.SelectMany(c => c.Value.fruits).Select(f => f.name).ToArray();

            if (!orderFruitSpriteNames.Any())
            {

                return null;

            }

            var fruitSpriteName = orderFruitSpriteNames.Except(availableFruitSpriteNames).ToArray().FirstOrDefault();

            return fruitSpriteName;

        }

    }

}
