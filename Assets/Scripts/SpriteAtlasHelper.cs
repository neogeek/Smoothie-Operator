using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

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

            _spriteRenderer.sprite = _spriteAtlas.GetSprite(_spriteRenderer.sprite.name) ?? _spriteRenderer.sprite;

        }

        if (_image && _image.sprite)
        {

            _image.sprite = _spriteAtlas.GetSprite(_image.sprite.name) ?? _image.sprite;

        }

    }

}
