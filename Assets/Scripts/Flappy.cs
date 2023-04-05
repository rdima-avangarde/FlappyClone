using UnityEngine;

namespace FlappyGame
{
    public class Flappy : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _flapSpeed;
        [SerializeField] private float _gravityMultiplier;

        private Vector3 _cachedVelocity;

        private void Start()
        {
            Pause(true);
        }

        public void Pause(bool paused)
        {
            this.enabled = !paused;
            _rigidBody.isKinematic = paused;

            if (paused)
            {
                _cachedVelocity = _rigidBody.velocity;
                _animator.speed = 0;
            }
            else
            {
                _rigidBody.velocity = _cachedVelocity;
                _animator.speed = 1;
            }
        }

        private void Update()
        {
            if (_gameManager.HasFlapInput)
            {
                _rigidBody.AddForce(_flapSpeed * Vector3.up - _rigidBody.velocity, ForceMode.VelocityChange);
            }
        }

        private void FixedUpdate()
        {
            _rigidBody.AddForce(_gravityMultiplier * Physics.gravity, ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            this.enabled = false;
            _animator.StopPlayback();
            _gameManager.EndGame();
        }

        private void OnTriggerEnter(Collider other)
        {
            _gameManager.FlappyScored();
        }
    }
}
