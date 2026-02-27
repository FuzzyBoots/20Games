using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JetpackJoyride
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class JetpackScript : MonoBehaviour
    {

        Animator _animator;
        Rigidbody _rb;
        JetpackInputs _inputs;
        [SerializeField] float _jetpackForce = 20f;
        [SerializeField] float _upperBound = 7f;
        [SerializeField] ParticleSystem _jetpackFlames;
        [SerializeField] AudioSource _jetpackAudio;
        [SerializeField] float _jetpackAudioLerpSpeed = 5f;

        float _jetpackVolume = 0.0f;

        bool _thrusting = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>(); 
            
            _inputs = new JetpackInputs();
            _inputs.Jetpack.Enable();

            _inputs.Jetpack.Jetpack.started += Jetpack_started;
            _inputs.Jetpack.Jetpack.canceled += Jetpack_canceled;
        }

        private void Jetpack_canceled(InputAction.CallbackContext obj)
        {
            _thrusting = false;

            _jetpackFlames?.Stop();
        }

        private void Jetpack_started(InputAction.CallbackContext obj)
        {
            if (!GameManager.Instance.GameSessionActive) return;
            _thrusting = true;
            _jetpackFlames?.Play();
        }

        private void Update()
        {
            _animator?.SetBool("Hovering", transform.position.y > 0.1f);
            
            float jetpackTargetVolume = _thrusting ? 1.0f : 0f;
            _jetpackVolume = Mathf.Lerp(_jetpackVolume, jetpackTargetVolume, Time.deltaTime * _jetpackAudioLerpSpeed);
            _jetpackAudio.volume = _jetpackVolume;

            if (_thrusting && transform.position.y < _upperBound)
            {
                _rb.AddForce(_jetpackForce * Time.deltaTime * Vector3.up, ForceMode.Impulse);
            }
        }

        // This probably should be handled in one place, but that feels like too much effort right now.
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                _animator.SetTrigger("Death");
            }
        }
    }
}