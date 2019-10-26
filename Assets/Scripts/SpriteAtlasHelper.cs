using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace SmoothieOperator
{

    public class SpriteAtlasHelper : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private SpriteAtlas _spriteAtlas;
#pragma warning restore CS0649

        private SpriteRenderer _spriteRenderer;

        private Image _image;

        private void Awake()
        {

            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            _image = gameObject.GetComponent<Image>();

        }

        private void OnEnable()
        {

            if (_spriteRenderer && _spriteRenderer.sprite)
            {

                SetSpriteRenderer(_spriteRenderer.sprite);

            }

            if (_image && _image.sprite)
            {

                SetImage(_image.sprite);

            }

        }

        public void SetSpriteRenderer(Sprite sprite)
        {

            SetSpriteRenderer(sprite.name);

        }

        public void SetSpriteRenderer(string spriteName)
        {

            if (!_spriteRenderer || !_spriteAtlas)
            {
                return;
            }

            var sprite = _spriteAtlas.GetSprite(spriteName);

            if (sprite)
            {

                _spriteRenderer.sprite = sprite;

            }

        }

        public void SetImage(Sprite sprite)
        {

            SetImage(sprite.name);

        }

        public void SetImage(string spriteName)
        {

            if (!_image || !_spriteAtlas)
            {
                return;
            }

            var sprite = _spriteAtlas.GetSprite(spriteName);

            if (sprite)
            {

                _image.sprite = sprite;

            }

        }

    }

    public static class SpriteCustomExtensions
    {

        public static string GetNiceName(this Sprite sprite)
        {

            return sprite.name.Replace("(Clone)", "");

        }

    }

}
