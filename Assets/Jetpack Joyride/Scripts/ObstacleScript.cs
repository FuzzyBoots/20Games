using UnityEngine;

namespace JetpackJoyride
{
    public class ObstacleScript : MonoBehaviour
    {
        [SerializeField] float _speed;

        [SerializeField] float _leftBound = -7f;

        private void Update()
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.left);

            if (transform.position.x < _leftBound)
            {
                Destroy(gameObject);    // We should actually be doing pooling, of course
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger?");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision!");
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Game Over");
            }
        }
    }
}