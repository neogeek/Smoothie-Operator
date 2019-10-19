using UnityEngine;

namespace SmoothieOperator
{

    [SelectionBaseAttribute]
    public class FruitController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private LayerMask _truckLayerMask;

        [SerializeField]
        private Transform _shadowTransform;
#pragma warning restore CS0649

        private void Update()
        {

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, _truckLayerMask);

            if (hit)
            {

                _shadowTransform.position = hit.point;

                _shadowTransform.localScale =
                    Vector3.Lerp(Vector3.one, Vector3.zero, Mathf.InverseLerp(0, 5, hit.distance));

            }

        }

    }

}
