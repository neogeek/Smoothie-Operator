using UnityEngine;

namespace SmoothieOperator
{

    [SelectionBase]
    public class FruitController : MonoBehaviour
    {

        public static LayerMask layerMask => LayerMask.GetMask("Fruit");

        public Fruit fruit;

    }

}
