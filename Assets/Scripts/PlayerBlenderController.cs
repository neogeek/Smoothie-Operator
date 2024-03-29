using System.Collections.Generic;
using System.Linq;
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
#pragma warning restore CS0649

        private AudioSource[] _sfxAudioSources;

        private FruitSpawner _fruitSpawner;

        private OrderManager _orderManager;

        private bool isGrounded;

        private float _horizontalMovement;

        private RaycastHit2D[] _collectibleFruits;

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

            _sfxAudioSources = GetComponentsInChildren<AudioSource>();

        }

        private void OnBlend()
        {

            Blend();
            CheckOrder();

        }

        private void OnFlush()
        {

            Flush();

        }

        private void Update()
        {

            _collectibleFruits = Physics2D.BoxCastAll(gameObject.transform.position, _boxCollider2D.bounds.size, 0,
                Vector2.zero,
                0, FruitController.layerMask);

            if (_collectibleFruits.Length > 0 && _collectedFruits.Count < _blendedSpriteAtlasHelpers.Length)
            {

                _blenderSpriteAtlasHelper.SetSpriteRenderer(_spriteBlenderHighlight);

            }
            else
            {

                _blenderSpriteAtlasHelper.SetSpriteRenderer(_spriteBlenderDefault);

            }

        }

        private void Blend()
        {

            if (_collectibleFruits.Length == 0 || _collectedFruits.Count.Equals(_blendedSpriteAtlasHelpers.Length))
            {
                return;
            }

            var collectibleFruits = _collectibleFruits.Where(c => _orderManager.IsFruitPartOfAnOrder(_collectedFruits,
                c.collider.gameObject.GetComponent<FruitController>().fruit));

            var collectibleFruit =
                collectibleFruits.Any() ? collectibleFruits.First() : _collectibleFruits.First();

            var fruit = collectibleFruit.collider.gameObject.GetComponent<FruitController>().fruit;

            _collectedFruits.Add(fruit);

            _blendedSpriteAtlasHelpers[_collectedFruits.Count - 1].SetSpriteRenderer(fruit.blended);

            _fruitSpawner.DestroyFruit(collectibleFruit.collider.gameObject);

            PlaySFX("blender_blend");

        }

        private void CheckOrder()
        {

            if (_orderManager.CanFruitsFulfillAnOrder(_collectedFruits.ToArray()))
            {

                Clear();

            }

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

            Clear();

            PlaySFX("blender_flush");

        }

        private void Clear()
        {

            foreach (var blendedSpriteAtlasHelper in _blendedSpriteAtlasHelpers)
            {

                blendedSpriteAtlasHelper.ClearSpriteRenderer();

            }

            _collectedFruits.Clear();

        }

        private void PlaySFX(string filter)
        {

            var sources = _sfxAudioSources.Where(source => source.clip.name.StartsWith(filter)).ToList();

            sources[Random.Range(0, sources.Count - 1)].Play();

        }

    }

}
