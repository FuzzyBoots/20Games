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
        }

        private void Update()
        {
            if (!GameManager.Instance.GameSessionActive) return;

            _animator?.SetBool("Hovering", transform.position.y > 0.1f);
            if (_thrusting)
            {
                _rb.AddForce(_jetpackForce * Vector3.up * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
}