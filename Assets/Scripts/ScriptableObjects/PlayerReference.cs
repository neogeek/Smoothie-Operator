using System;
using UnityEngine;

namespace SmoothieOperator
{

    [CreateAssetMenu(fileName = "PlayerReference", menuName = "PlayerReference")]
    public class PlayerReference : ScriptableObject
    {

        public const int STARTING_LIVES = 3;

        [NonSerialized]
        [RangeAttribute(0, STARTING_LIVES)]
        public int lives = STARTING_LIVES;

        [NonSerialized]
        public int score;

        public void LifeLost()
        {

            if (lives >= 1)
            {

                lives -= 1;

            }

            if (lives == 0)
            {

                FindObjectOfType<GameController>().GameOver(score);

            }

        }

        public void AddPointsToScore(int points)
        {

            score += points;

        }

        public void Reset()
        {

            lives = STARTING_LIVES;

            score = 0;

        }

    }

}
