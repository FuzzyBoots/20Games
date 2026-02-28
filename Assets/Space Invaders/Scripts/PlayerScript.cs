using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceInvaders
{
    public class PlayerScript : MonoBehaviour
    {
        SpaceInvadersInput _input;

        [SerializeField] private float _speed;
        [SerializeField] private GameObject _bulletPrefab;

        Coroutine _movementCoroutine;
        Coroutine _fireCoroutine;

        GameObject _bullet;
        private Vector3 _firePosition;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            transform.position = new Vector3(0, 0, 0);

            _input = new SpaceInvadersInput();
            _input.Enable();

            _input.SpaceInvaders.Movement.performed += HandleMovement;
            _input.SpaceInvaders.Movement.canceled += HandleMovementStopped;
            _input.SpaceInvaders.Fire.performed += HandleFire;
            _input.SpaceInvaders.Fire.canceled += HandleStopFiring;
        }

        private void HandleMovement(InputAction.CallbackContext context)
        {
            if (_movementCoroutine == null)
            {
                _movementCoroutine = StartCoroutine(MovementCoroutine());
            }
        }

        private IEnumerator MovementCoroutine()
        {
            while (true)
            {
                transform.Translate(_input.SpaceInvaders.Movement.ReadValue<float>() * Time.deltaTime * _speed * Vector3.right);
                yield return null;
            }
        }

        private void HandleMovementStopped(InputAction.CallbackContext context)
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }
        }

        private void HandleFire(InputAction.CallbackContext context)
        {
            if (_fireCoroutine == null)
            {
                _fireCoroutine = StartCoroutine(FireCoroutine());
            }
        }

        private IEnumerator FireCoroutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => _bullet == null);
                _bullet = Instantiate(_bulletPrefab, _firePosition, Quaternion.identity);
            }
        }

        private void HandleStopFiring(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
    }
}