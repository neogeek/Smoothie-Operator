using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    public class OrderManager : MonoBehaviour, IPausable
    {

        private const float MAX_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS = 10;

        private const float MIN_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS = 1;

        private const float DELTA_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS = 0.25f;

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

        private Coroutine _spawnCustomersCoroutine;

        private float _delayBetweenSpawningNewCustomers = MAX_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS;

        private void Start()
        {

            _spawnCustomersCoroutine = StartCoroutine(SpawnCustomers());

        }

        private IEnumerator SpawnCustomers()
        {

            while (true)
            {

                yield return new WaitForSeconds(_delayBetweenSpawningNewCustomers);

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

            customerController.order = new Order
            {
                fruits = new[]
                {
                    _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)].GetComponent<FruitController>().fruit,
                    _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)].GetComponent<FruitController>().fruit,
                    _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)].GetComponent<FruitController>().fruit
                }
            };

            customerController.OrderCanceledEvent += HandleOrderCanceled;

            customerController.OrderFulFilledEvent += HandleOrderFulFilled;

            _customers.Add(spawnTransform, customerController);

            _delayBetweenSpawningNewCustomers = Mathf.Clamp(
                _delayBetweenSpawningNewCustomers - DELTA_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS,
                MIN_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS,
                MAX_DELAY_BETWEEN_SPAWNING_NEW_CUSTOMERS);

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

        private void HandleOrderFulFilled(CustomerController customerController)
        {

            _customers.Remove(_customers.First(customer => customer.Value.Equals(customerController)).Key);

        }

        public bool CanFruitsFulfillAnOrder(Fruit[] fruits)
        {

            foreach (var customer in _customers)
            {

                if (customer.Value.order.fruits.Length != fruits.Length ||
                    customer.Value.order.fruits.Except(fruits).Any())
                {
                    continue;
                }

                StartCoroutine(customer.Value.OrderFulFilled());

                return true;

            }

            return false;

        }

        public void Pause()
        {

            if (_spawnCustomersCoroutine == null)
            {
                return;
            }

            StopCoroutine(_spawnCustomersCoroutine);

            _spawnCustomersCoroutine = null;

        }

        public void Resume()
        {

            _spawnCustomersCoroutine = StartCoroutine(SpawnCustomers());

        }

    }

}
