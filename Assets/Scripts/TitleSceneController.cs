using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothieOperator
{

    public class TitleSceneController : MonoBehaviour
    {

        private void Update()
        {

            if (Input.anyKeyDown)
            {

                SceneManager.LoadScene("Controls");

            }

        }

    }

}
