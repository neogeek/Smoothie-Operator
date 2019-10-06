using UnityEngine;
using UnityEngine.SceneManagement;

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
