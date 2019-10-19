using System.Collections;
using UnityEngine;

namespace SmoothieOperator
{

    public class FruitSpawner : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private GameObject[] _fruitPrefabs;

        [SerializeField]
        private Vector3 _spawnVelocity;
#pragma warning restore CS0649

        private void Start()
        {

            StartCoroutine(SpawnFruits());

        }

        private IEnumerator SpawnFruits()
        {

            while (true)
            {

                var spawnedFruit = Instantiate(_fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)],
                    gameObject.transform.position,
                    Quaternion.identity);

                var rigidbody2D = spawnedFruit.GetComponent<Rigidbody2D>();

                rigidbody2D.velocity = _spawnVelocity;

                yield return new WaitForSeconds(Random.Range(0.5f, 1));

            }

        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.green;

            Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + _spawnVelocity);

        }

    }

}
