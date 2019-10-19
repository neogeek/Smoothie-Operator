using UnityEngine;

namespace SmoothieOperator
{

    [CreateAssetMenu(fileName = "PlayerReference", menuName = "PlayerReference")]
    public class PlayerReference : ScriptableObject
    {

        public const int STARTING_LIVES = 3;

        public int lives = STARTING_LIVES;

        public int score;

        public void Reset()
        {

            lives = STARTING_LIVES;

            score = 0;

        }

    }

}
