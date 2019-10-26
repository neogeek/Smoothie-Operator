using System.Collections;
using UnityEngine;

namespace SmoothieOperator
{

    public class TruckController : MonoBehaviour
    {

        private const float BOUNCE_FORCE = 5;

        private static LayerMask _fruitLayerMask => LayerMask.GetMask("Fruit");

#pragma warning disable CS0649
        [SerializeField]
        private BoxCollider2D _floorCollider;

        [SerializeField]
        private AnimationCurve _bounceAnimation;
#pragma warning restore CS0649

        private Vector3 _bounceBoxCastPosition;

        private Vector3 _bounceBoxCastSize;

        private readonly RaycastHit2D[] groundedFruits = new RaycastHit2D[1000];

        private void Awake()
        {

            var colliderBounds = _floorCollider.bounds;

            _bounceBoxCastPosition = colliderBounds.center + Vector3.up * colliderBounds.size.y;

            _bounceBoxCastSize = colliderBounds.size;

        }

        private IEnumerator Start()
        {

            while (true)
            {

                var hits = Physics2D.BoxCastNonAlloc(_bounceBoxCastPosition, _bounceBoxCastSize, 0, Vector2.zero,
                    groundedFruits, 0, _fruitLayerMask);

                for (var i = 0; i < hits; i += 1)
                {

                    groundedFruits[i].collider.gameObject.GetComponent<Rigidbody2D>()
                        .AddForce(Vector2.up * BOUNCE_FORCE, ForceMode2D.Impulse);

                }

                yield return StartCoroutine(AnimateTruck());

                yield return new WaitForSeconds(Random.Range(1, 5));

            }

        }

        private IEnumerator AnimateTruck()
        {

            var timer = 0f;

            while (timer < _bounceAnimation.keys[_bounceAnimation.keys.Length - 1].time)
            {

                timer += Time.deltaTime;

                gameObject.transform.position = new Vector3(0, _bounceAnimation.Evaluate(timer), 0);

                yield return null;

            }

        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_bounceBoxCastPosition, _bounceBoxCastSize);

        }

    }

}
