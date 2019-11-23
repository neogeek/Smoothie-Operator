using System.Collections.Generic;
using UnityEngine;

namespace SmoothieOperator
{

    [SelectionBase]
    public class PlayerBlenderController : MonoBehaviour
    {

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

        private FruitSpawner _fruitSpawner;

        private OrderManager _orderManager;

        private bool isGrounded;

        private float _horizontalMovement;

        private RaycastHit2D _collectibleFruit;

        private readonly List<Fruit> _collectedFruits = new List<Fruit>();

        private void Awake()
        {

            _fruitSpawner = FindObjectOfType<FruitSpawner>();

            _orderManager = FindObjectOfType<OrderManager>();

            if (_fruitSpawner == null)
            {

                Debug.LogError("Fruit Spawner not found!");

            }

            if (_orderManager == null)
            {

                Debug.LogError("Order Manager not found!");

            }

        }

        private void Update()
        {

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
