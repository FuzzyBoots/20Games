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
        [SerializeField] float _jetpackForce = 5f;

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
        }

        private void Jetpack_started(InputAction.CallbackContext obj)
        {
            _thrusting = true;
        }

        private void Update()
        {
            if (!GameManager.Instance.GameSessionActive) return;

            _animator?.SetBool("Hovering", transform.position.y > 0.1f);
            if (_thrusting)
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