using UnityEngine;

[CreateAssetMenu(fileName = "PlayerReference", menuName = "PlayerReference")]
public class PlayerReference : ScriptableObject
{

    public int lives = 3;

    public int score;

    public void Reset()
    {

        lives = 3;

        score = 0;

    }

}
