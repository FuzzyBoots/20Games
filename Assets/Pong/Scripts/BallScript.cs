using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallScript : MonoBehaviour
    {
        [SerializeField] float _launchDelay = 2f;
        Rigidbody _body;
        WaitForSeconds _launchWait = new WaitForSeconds(2f);
        private void OnValidate()
        {
            _launchWait = new WaitForSeconds(_launchDelay);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _body = GetComponent<Rigidbody>();

            Assert.IsNotNull(_body, "Null RB!");

            Debug.Log($"Current RigidBody is defined: {_body != null}");

            ResetBall();
        }

        private IEnumerator LaunchBall()
        {
            yield return _launchWait;

            float angle = Random.Range(-60f, 60f);
            float force = Random.Range(6f, 8f);

            bool goingRight = Random.Range(0, 2) == 0;

            Vector3 forceVector = Quaternion.Euler(0, goingRight ? angle : 180 + angle, 0) * (force * Vector3.right);

            _body.AddForce(forceVector, ForceMode.VelocityChange);
        }

        public void ResetBall()
        {
            Debug.Log($"Entering ResetBall. RB is not null? {_body != null}");
            transform.position = Vector3.zero;

            _body.linearVelocity = Vector3.zero;
            _body.angularVelocity = Vector3.zero;

            StartCoroutine(LaunchBall());
        }
    }
}