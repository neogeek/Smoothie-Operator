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

        [SerializeField]
        private GameObject[] _fruitPrefabs;
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

            customerController.order = new Order
            {
                fruits = new[]
                {
                    _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)].GetComponent<FruitController>().fruit,
                    _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)].GetComponent<FruitController>().fruit,
                    _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)].GetComponent<FruitController>().fruit
                }
            };

            customerController.OrderCanceled += HandleOrderCanceled;

            _customers.Add(spawnTransform, customerController);

        }

        public GameObject FruitNeededByCustomerNotCurrentlyAvailable(IEnumerable<GameObject> availableFruitGameObjects)
        {

            var availableFruits = availableFruitGameObjects.Select(f => f.GetComponent<FruitController>().fruit);

            var orderFruits = _customers.SelectMany(c => c.Value.order.fruits).ToArray();

            if (!orderFruits.Any())
            {

                return null;

            }

            var fruitNeededByCustomer = orderFruits.Except(availableFruits).ToArray();

            if (!fruitNeededByCustomer.Any())
            {

                return null;

            }

            return _fruitPrefabs.First(f =>
                f.GetComponent<FruitController>().fruit.Equals(fruitNeededByCustomer.First()));

        }

        private void HandleOrderCanceled(CustomerController customerController)
        {

            _customers.Remove(_customers.First(customer => customer.Value.Equals(customerController)).Key);

        }

    }

}
