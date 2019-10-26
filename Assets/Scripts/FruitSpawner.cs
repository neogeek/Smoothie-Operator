using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmoothieOperator
{

    public class FruitSpawner : MonoBehaviour
    {

        private const int MAX_FRUITS_AVAILABLE_AT_A_TIME = 10;

#pragma warning disable CS0649
        [SerializeField]
        private Transform[] _spawnTransforms;

        [SerializeField]
        private GameObject[] _fruitPrefabs;
#pragma warning restore CS0649

        private OrderManager _orderManager;

        private readonly HashSet<GameObject> _spawnedFruits = new HashSet<GameObject>();

        private void Awake()
        {

            _orderManager = FindObjectOfType<OrderManager>();

            if (_orderManager == null)
            {

                Debug.LogError("Order Manager not found!");

            }

        }

        private IEnumerator Start()
        {

            while (true)
            {

                if (_spawnedFruits.Count < MAX_FRUITS_AVAILABLE_AT_A_TIME)
                {

                    var spawnTransform = _spawnTransforms[Random.Range(0, _spawnTransforms.Length)];

                    var fruitToSpawn = _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)];

                    var fruitSpriteNeeded = _orderManager.FruitNeededByCustomerNotCurrentlyAvailable(_spawnedFruits);

                    if (fruitSpriteNeeded != null)
                    {

                        fruitToSpawn = fruitSpriteNeeded;

                    }

                    var spawnedFruit = Instantiate(fruitToSpawn,
                        spawnTransform.position,
                        Quaternion.identity);

                    spawnedFruit.GetComponent<Rigidbody2D>().velocity = spawnTransform.right;

                    _spawnedFruits.Add(spawnedFruit);

                    yield return new WaitForSeconds(Random.Range(0.5f, 1));

                }
                else
                {

                    yield return null;

                }

            }

        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.green;

            for (var i = 0; i < _spawnTransforms.Length; i += 1)
            {

                Gizmos.DrawLine(_spawnTransforms[i].position,
                    _spawnTransforms[i].position + _spawnTransforms[i].right);

            }

        }

        public void DestroyFruit(GameObject fruit)
        {

            _spawnedFruits.Remove(fruit);

            Destroy(fruit);

        }

    }

}
