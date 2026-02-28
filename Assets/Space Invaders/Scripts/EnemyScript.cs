using System.Collections;
using UnityEngine;

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

            if (!Physics.Raycast(transform.position, Vector3.down * 4, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                BulletScript bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                bullet.SetRotation(180);
            }

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
