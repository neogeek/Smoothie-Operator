using UnityEngine;
using UnityEngine.UI;

namespace SmoothieOperator
{

    [RequireComponent(typeof(Text))]
    public class PointsDisplay : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private PlayerReference _playerReference;
#pragma warning restore CS0649

        private Text _textComp;

        private void Awake()
        {

            _textComp = gameObject.GetComponent<Text>();

        }

        private void Update()
        {

            _textComp.text = $"{_playerReference.score:N0} Points";

        }

    }

}
