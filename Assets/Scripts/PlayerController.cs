using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    private const float HORIZONTAL_SPEED = 5;

    private const float JUMP_FORCE = 5;

    private const float GROUND_TEST_DISTANCE = 0.1f;

#pragma warning disable CS0649
    [SerializeField]
    private LayerMask _truckLayerMask;

    [SerializeField]
    private Transform _shadowTransform;
#pragma warning restore CS0649

    private Rigidbody2D _rigidbody2D;

    private bool isGrounded;

    private float _horizontalMovement;

    private void Awake()
    {

        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        _horizontalMovement = Input.GetAxis("Horizontal");

        var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, _truckLayerMask);

        if (hit)
        {

            _shadowTransform.position = hit.point;

            _shadowTransform.localScale =
                Vector3.Lerp(Vector3.one, Vector3.zero, Mathf.InverseLerp(0, 5, hit.distance));

        }

        isGrounded = hit.distance < GROUND_TEST_DISTANCE;

        if (isGrounded && Input.GetButtonUp("Jump"))
        {

            _rigidbody2D.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);

        }

    }

    private void FixedUpdate()
    {

        _rigidbody2D.velocity = new Vector2(_horizontalMovement * HORIZONTAL_SPEED, _rigidbody2D.velocity.y);

    }

}
