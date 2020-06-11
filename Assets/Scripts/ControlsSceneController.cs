using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SmoothieOperator
{

    public class ControlsSceneController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private Image _controllerControlsImage;

        [SerializeField]
        private Image _keyboardControlsImage;
#pragma warning restore CS0649

        private bool ControllerConnected => Input.GetJoystickNames().Length > 0;

        private void Start()
        {

            _controllerControlsImage.enabled = ControllerConnected;
            _keyboardControlsImage.enabled = !ControllerConnected;

        }

        private void Update()
        {

            if (Input.anyKeyDown)
            {

                SceneManager.LoadScene("Game");

            }

        }

    }

}
