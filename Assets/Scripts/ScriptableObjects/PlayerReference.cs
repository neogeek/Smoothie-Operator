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

    }

}
