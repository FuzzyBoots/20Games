using System.Collections;
using UnityEngine;

namespace JetpackJoyride
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] Vector2 _bounds = new Vector2(1, 5);

        [SerializeField] float _spawnInterval = 2f;

        [SerializeField] GameObject _obstaclePrefab;


        bool _spawning = false;

        public void Start()
        {
            StartCoroutine(SpawnObstacles());
        }


        IEnumerator SpawnObstacles()
        {
            while (true)
            {
                GameObject obstacle = Instantiate(_obstaclePrefab);
                obstacle.transform.position = new Vector3(8f, Random.Range(_bounds.x, _bounds.y), 0f);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}