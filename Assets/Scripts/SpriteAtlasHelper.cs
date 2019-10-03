using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAtlasHelper : MonoBehaviour
{

#pragma warning disable CS0649
    [SerializeField]
    private SpriteAtlas _spriteAtlas;
#pragma warning restore CS0649

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (_spriteRenderer.sprite)
        {

            _spriteRenderer.sprite = _spriteAtlas.GetSprite(_spriteRenderer.sprite.name);

        }

    }

}
