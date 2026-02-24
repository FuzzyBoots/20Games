using System.Collections;
using UnityEngine;

namespace JetpackJoyride
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        [SerializeField] float _spawnInterval = 2f;

        [SerializeField] ObstacleScript[] _obstaclePrefabs;

        
        bool _spawning = false;

        public void StartSpawning()
        {
            _spawning = true;
            StartCoroutine(SpawnObstacles());
        }

        public void StopSpawning()
        {
            _spawning = false;
        }


        IEnumerator SpawnObstacles()
        {
            while (_spawning)
            {
                ObstacleScript obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);

                obstacle.transform.position = new Vector3(8f, Random.Range(obstacle.GetLowerBound(), obstacle.GetUpperBound()), 0f);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}