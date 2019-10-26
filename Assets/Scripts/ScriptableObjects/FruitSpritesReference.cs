using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    [CreateAssetMenu(fileName = "FruitSpritesReference", menuName = "FruitSpritesReference")]
    public class FruitSpritesReference : ScriptableObject
    {

        public Fruit[] _fruits;

        public Fruit GetFruitSpriteFromFruitName(string fruitName)
        {

            return _fruits.First(f => f.fruit.name.Equals(fruitName));

        }

    }

}
