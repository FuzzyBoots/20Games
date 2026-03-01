using System.Collections;
using UnityEngine;

namespace SpaceInvaders
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField] float[] _shotIntervals = { 1f, 2f, 3f };
        [SerializeField] BulletScript _bulletPrefab;

        private void Start()
        {
            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(_shotIntervals[Random.Range(0, _shotIntervals.Length)]);

                if (!Physics.Raycast(transform.position, Vector3.back * 4, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
                {
                    BulletScript bullet = Instantiate(_bulletPrefab, transform.position + Vector3.back, Quaternion.identity);
                    bullet.tag = "EnemyBullet";
                    bullet.SetIsEnemy(true);
                    Debug.DrawRay(transform.position, Vector3.back * 4);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Actual collision with " + collision.gameObject.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enemy collided with: " + other.gameObject.name + " tag of " + other.gameObject.tag);
            if (other.CompareTag("Bullet"))
            {
                Debug.Log("Bullet?");
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}