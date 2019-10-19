using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothieOperator
{

    public class ControlsSceneController : MonoBehaviour
    {

        private void Update()
        {

            if (Input.anyKeyDown)
            {

                SceneManager.LoadScene("Game");

            }

        }

    }

}
