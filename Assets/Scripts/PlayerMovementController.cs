using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private bool _isGrounded;

        private float _horizontalMovement;

        private Coroutine _horizontalMovementSFX;

        private void Awake()
        {

            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            _sfxAudioSources = GetComponentsInChildren<AudioSource>();

        }

        private void OnMovement(InputValue value)
        {

            _horizontalMovement = value.Get<Vector2>().x;

            if (Mathf.Abs(_horizontalMovement) > 0.5f && _isGrounded)
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

        }

        private void OnJumpPrepare()
        {

            if (!_isGrounded)
            {
                return;
            }

            PlaySFX("blender_jump_prepare");

        }

        private void OnJumpRelease()
        {

            if (!_isGrounded)
            {
                return;
            }

            _rigidbody2D.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);

            PlaySFX("blender_jump_release");

        }

        private void Update()
        {

            var hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10f, TruckController.layerMask);

            _isGrounded = hit.distance < GROUND_TEST_DISTANCE;

            if (_isGrounded && _rigidbody2D.velocity.y < -1.5f)
            {

                PlaySFX("blender_land");

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
