using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmoothieOperator
{

    public class TitleSceneController : MonoBehaviour
    {

        private void OnStart()
        {

            SceneManager.LoadScene("Controls");

        }

    }

}
