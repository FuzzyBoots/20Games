using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Pong
{
    public class PaddleScript : MonoBehaviour
    {
        [SerializeField] Vector2 bounds = new Vector2(-3.8f, 3.8f);
        [SerializeField] float _horizontalPosition = -8.5f;
        [SerializeField] string _paddleMovementInputName;
        InputAction _paddleMovement;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        Vector3 _position;

        [SerializeField] float _speed = 5f;

        private void Start()
        {
            _position = new Vector3(_horizontalPosition, 0, 0);
            transform.position = _position;

            _paddleMovement = InputSystem.actions.FindAction(_paddleMovementInputName);

            Assert.IsNotNull(_paddleMovement, $"No input found by name {_paddleMovementInputName}");
        }


        private void Update()
        {
            float amount = -_paddleMovement.ReadValue<float>();

            float location = _position.z;
            location += amount * _speed * Time.deltaTime;
            location = Mathf.Clamp(location, bounds.x, bounds.y);

            _position.z = location;

            transform.position = _position;
        }
    }
}