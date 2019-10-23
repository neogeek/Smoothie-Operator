using System;
using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    [CreateAssetMenu(fileName = "FruitSpritesReference", menuName = "FruitSpritesReference")]
    public class FruitSpritesReference : ScriptableObject
    {

        [Serializable]
        public struct FruitSprite
        {

            public Sprite fruit;

            public Sprite blended;

            public Sprite splat;

        }

        public FruitSprite[] _fruitSprites;

        public FruitSprite GetFruitSpriteFromFruitName(string fruitName)
        {

            return _fruitSprites.First(f => f.fruit.name.Equals(fruitName));

        }

    }

}
