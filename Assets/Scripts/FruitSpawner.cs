using System.Collections;
using UnityEngine;

namespace SmoothieOperator
{

    public class FruitSpawner : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private Transform[] _spawnTransforms;

        [SerializeField]
        private GameObject[] _fruitPrefabs;
#pragma warning restore CS0649

        private GameObject[] _spawnedFruits;

        private IEnumerator Start()
        {

            while (true)
            {

                var spawnTransform = _spawnTransforms[Random.Range(0, _spawnTransforms.Length)];

                var spawnedFruit = Instantiate(_fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)],
                    spawnTransform.position,
                    Quaternion.identity);

                var rigidbody2D = spawnedFruit.GetComponent<Rigidbody2D>();

                rigidbody2D.velocity = spawnTransform.right;

                yield return new WaitForSeconds(Random.Range(0.5f, 1));

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

    }

}
