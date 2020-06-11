using System.Collections;
using System.Linq;
using UnityEngine;

namespace SmoothieOperator
{

    [SelectionBase]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {

        private const float HORIZONTAL_SPEED = 5;

        private const float JUMP_FORCE = 5;

        private const float GROUND_TEST_DISTANCE = 0.1f;

        private Rigidbody2D _rigidbody2D;

        private AudioSource[] _sfxAudioSources;

        private bool isGrounded;

        private float _horizontalMovement;

        private Coroutine _horizontalMovementSFX;

        private void Awake()
        {

            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            _sfxAudioSources = GetComponentsInChildren<AudioSource>();

        }

        private void Update()
        {

            _horizontalMovement = Input.GetAxis("Horizontal");

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, TruckController.layerMask);

            isGrounded = hit.distance < GROUND_TEST_DISTANCE;

            if (Mathf.Abs(_horizontalMovement) > 0.5f && isGrounded)
            {

                if (_horizontalMovementSFX == null)
                {

                    _horizontalMovementSFX = StartCoroutine(PlaySFXLoop("blender_footstep", 0.23f));

                }

            }
            else if (_horizontalMovementSFX != null)
            {

                StopCoroutine(_horizontalMovementSFX);

                _horizontalMovementSFX = null;

            }

            if (isGrounded && _rigidbody2D.velocity.y < -1.5f)
            {

                PlaySFX("blender_land");

            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {

                PlaySFX("blender_jump_prepare");

            }
            else if (isGrounded && Input.GetButtonUp("Jump"))
            {

                _rigidbody2D.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);

                PlaySFX("blender_jump_release");

            }

        }

        private void FixedUpdate()
        {

            _rigidbody2D.velocity = new Vector2(_horizontalMovement * HORIZONTAL_SPEED, _rigidbody2D.velocity.y);

        }

        private void PlaySFX(string filter)
        {

            var sources = _sfxAudioSources.Where(s => s.clip.name.StartsWith(filter)).ToList();

            sources[Random.Range(0, sources.Count - 1)].Play();

        }

        private IEnumerator PlaySFXLoop(string filter, float delay = 1)
        {

            var sources = _sfxAudioSources.Where(s => s.clip.name.StartsWith(filter)).ToList();

            while (true)
            {

                sources[Random.Range(0, sources.Count - 1)].Play();

                yield return new WaitForSeconds(delay);

            }

        }

    }

}
