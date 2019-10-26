using UnityEngine;

namespace SmoothieOperator
{

    public class ShadowController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private Transform _shadowTransform;
#pragma warning restore CS0649

        private void Update()
        {

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, TruckController.layerMask);

            if (!hit)
            {
                return;
            }

            _shadowTransform.position = hit.point;

            _shadowTransform.localScale =
                Vector3.Lerp(Vector3.one, Vector3.zero, Mathf.InverseLerp(0, 5, hit.distance));

        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.green;

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, TruckController.layerMask);

            Gizmos.DrawLine(gameObject.transform.position, hit.point);

        }

    }

}
