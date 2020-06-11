using UnityEngine;
using UnityEngine.InputSystem;
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

        private bool ControllerConnected => Gamepad.current != null && Gamepad.current.name.Contains("Gamepad");

        private void Start()
        {

            _controllerControlsImage.enabled = ControllerConnected;
            _keyboardControlsImage.enabled = !ControllerConnected;

        }

        private void OnStart()
        {

            SceneManager.LoadScene("Game");

        }

    }

}
