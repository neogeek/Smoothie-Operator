using System.Collections.Generic;
using UnityEngine;

namespace SmoothieOperator
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {

        private const float HORIZONTAL_SPEED = 5;

        private const float JUMP_FORCE = 5;

        private const float GROUND_TEST_DISTANCE = 0.1f;

#pragma warning disable CS0649
        [SerializeField]
        private FruitSpawner _fruitSpawner;

        [SerializeField]
        private BoxCollider2D _boxCollider2D;

        [SerializeField]
        private SpriteAtlasHelper[] _blendedSpriteAtlasHelpers;

        [SerializeField]
        private GameObject _splatPrefab;

        [SerializeField]
        private Transform _truckTransform;
#pragma warning restore CS0649

        private Rigidbody2D _rigidbody2D;

        private bool isGrounded;

        private float _horizontalMovement;

        private readonly List<Fruit> _collectedFruits = new List<Fruit>();

        private void Awake()
        {

            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        }

        private void Update()
        {

            _horizontalMovement = Input.GetAxis("Horizontal");

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, TruckController.layerMask);

            isGrounded = hit.distance < GROUND_TEST_DISTANCE;

            if (isGrounded && Input.GetButtonUp("Jump"))
            {

                _rigidbody2D.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);

            }

            if (Input.GetButtonDown("Blend"))
            {

                Blend();

            }

            if (Input.GetButtonDown("Flush"))
            {

                Flush();

            }

        }

        private void FixedUpdate()
        {

            _rigidbody2D.velocity = new Vector2(_horizontalMovement * HORIZONTAL_SPEED, _rigidbody2D.velocity.y);

        }

        private void Blend()
        {

            if (_collectedFruits.Count.Equals(_blendedSpriteAtlasHelpers.Length))
            {

                return;

            }

            var hit = Physics2D.BoxCast(gameObject.transform.position, _boxCollider2D.bounds.size, 0, Vector2.zero,
                0, FruitController.layerMask);

            if (!hit)
            {
                return;
            }

            var fruit = hit.collider.gameObject.GetComponent<FruitController>().fruit;

            _collectedFruits.Add(fruit);

            _blendedSpriteAtlasHelpers[_collectedFruits.Count - 1].SetSpriteRenderer(fruit.blended);

            _fruitSpawner.DestroyFruit(hit.collider.gameObject);

        }

        private void Flush()
        {

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, TruckController.layerMask);

            foreach (var fruit in _collectedFruits)
            {

                var splat = Instantiate(_splatPrefab, hit.point, Quaternion.identity,
                    _truckTransform);

                splat.GetComponent<SpriteAtlasHelper>().SetSpriteRenderer(fruit.splat);

            }

            foreach (var blendedSpriteAtlasHelper in _blendedSpriteAtlasHelpers)
            {

                blendedSpriteAtlasHelper.ClearSpriteRenderer();

            }

            _collectedFruits.Clear();

        }

    }

}
