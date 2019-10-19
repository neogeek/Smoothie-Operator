using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{

#pragma warning disable CS0649
    [SerializeField]
    private PlayerReference _playerReference;

    [SerializeField]
    private Image[] _liveImages;

    [SerializeField]
    private Sprite _spriteLifeAvailable;

    [SerializeField]
    private Sprite _spriteLifeLost;
#pragma warning restore CS0649

    private void Update()
    {

        for (var i = 0; i < PlayerReference.STARTING_LIVES - _playerReference.lives; i += 1)
        {

            if (_liveImages[i].sprite.Equals(_spriteLifeLost))
            {
                continue;
            }

            _liveImages[i].gameObject.SetActive(false);
            _liveImages[i].sprite = _spriteLifeLost;
            _liveImages[i].gameObject.SetActive(true);

        }

        for (var i = PlayerReference.STARTING_LIVES - _playerReference.lives; i < _liveImages.Length; i += 1)
        {

            if (_liveImages[i].sprite.Equals(_spriteLifeAvailable))
            {
                continue;
            }

            _liveImages[i].gameObject.SetActive(false);
            _liveImages[i].sprite = _spriteLifeAvailable;
            _liveImages[i].gameObject.SetActive(true);

        }

    }

}
