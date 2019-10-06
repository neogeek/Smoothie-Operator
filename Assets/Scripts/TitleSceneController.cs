using UnityEngine;
using UnityEngine.SceneManagement;

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
