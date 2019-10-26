using UnityEngine;

namespace SmoothieOperator
{

    public class ShadowController : MonoBehaviour
    {

        private const float MAX_RAYCAST_DISTANCE = 10f;

        private const float MAX_SHADOW_SCALE_DISTANCE = 5f;

#pragma warning disable CS0649
        [SerializeField]
        private Transform _shadowTransform;
#pragma warning restore CS0649

        private void Update()
        {

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, MAX_RAYCAST_DISTANCE,
                TruckController.layerMask);

            if (!hit)
            {
                return;
            }

            _shadowTransform.position = hit.point;

            _shadowTransform.localScale =
                Vector3.Lerp(Vector3.one, Vector3.zero, Mathf.InverseLerp(0, MAX_SHADOW_SCALE_DISTANCE, hit.distance));

        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.green;

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, MAX_RAYCAST_DISTANCE,
                TruckController.layerMask);

            Gizmos.DrawLine(gameObject.transform.position, hit.point);

        }

    }

}
