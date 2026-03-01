using UnityEngine;

namespace SpaceInvaders
{
    public class BulletScript : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        [SerializeField] bool _isEnemy = false;

        private void Start()
        {
            Debug.Log("Start");
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        public void SetIsEnemy(bool isEnemy)
        {
            _isEnemy = isEnemy;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet")) {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Debug.Log("Forward: " + transform.forward);
            if (_isEnemy)
            {
                transform.Translate(Time.fixedDeltaTime * _speed * -transform.forward);

                if (transform.position.y < 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.Translate(Time.fixedDeltaTime * _speed * transform.forward);

                if (transform.position.y > 15)
                {
                    Destroy(gameObject);
                }
            }

            
        }
    }
}