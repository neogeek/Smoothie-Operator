using System.Collections.Generic;
using UnityEngine;

namespace SmoothieOperator
{

    [SelectionBase]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {

        private const float HORIZONTAL_SPEED = 5;

        private const float JUMP_FORCE = 5;

        private const float GROUND_TEST_DISTANCE = 0.1f;

#pragma warning disable CS0649
        [SerializeField]
        private BoxCollider2D _boxCollider2D;

        [SerializeField]
        private SpriteAtlasHelper[] _blendedSpriteAtlasHelpers;

        [SerializeField]
        private GameObject _splatPrefab;

        [SerializeField]
        private Transform _truckTransform;

        [SerializeField]
        private SpriteAtlasHelper _blenderSpriteAtlasHelper;

        [SerializeField]
        private Sprite _spriteBlenderDefault;

        [SerializeField]
        private Sprite _spriteBlenderHighlight;

        [SerializeField]
        private AudioSource[] _blendSoundAudioSources;

        [SerializeField]
        private AudioSource[] _flushSoundAudioSources;
#pragma warning restore CS0649

        private Rigidbody2D _rigidbody2D;

        private FruitSpawner _fruitSpawner;

        private bool isGrounded;

        private float _horizontalMovement;

        private RaycastHit2D _collectibleFruit;

        private readonly List<Fruit> _collectedFruits = new List<Fruit>();

        private void Awake()
        {

            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            _fruitSpawner = FindObjectOfType<FruitSpawner>();

            if (_fruitSpawner == null)
            {

                Debug.LogError("Fruit Spawner not found!");

            }

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

            _collectibleFruit = Physics2D.BoxCast(gameObject.transform.position, _boxCollider2D.bounds.size, 0,
                Vector2.zero,
                0, FruitController.layerMask);

            if (_collectibleFruit && _collectedFruits.Count < _blendedSpriteAtlasHelpers.Length)
            {

                _blenderSpriteAtlasHelper.SetSpriteRenderer(_spriteBlenderHighlight);

            }
            else
            {

                _blenderSpriteAtlasHelper.SetSpriteRenderer(_spriteBlenderDefault);

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

            if (!_collectibleFruit)
            {

                return;

            }

            var fruit = _collectibleFruit.collider.gameObject.GetComponent<FruitController>().fruit;

            _collectedFruits.Add(fruit);

            _blendedSpriteAtlasHelpers[_collectedFruits.Count - 1].SetSpriteRenderer(fruit.blended);

            _fruitSpawner.DestroyFruit(_collectibleFruit.collider.gameObject);

            _blendSoundAudioSources[Random.Range(0, _blendSoundAudioSources.Length - 1)].Play();

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

            _flushSoundAudioSources[Random.Range(0, _flushSoundAudioSources.Length - 1)].Play();

        }

    }

}
