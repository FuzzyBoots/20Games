using System.Collections;
using UnityEngine;

namespace JetpackJoyride
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] float _spawnInterval = 2f;

        [SerializeField] ObstacleScript[] _obstaclePrefabs;

        [SerializeField] float _obstacleSpeed = 3f;


        bool _spawning = false;

        public void Start()
        {
            _spawning = true;
            StartCoroutine(SpawnObstacles());
        }


        IEnumerator SpawnObstacles()
        {
            while (_spawning)
            {
                ObstacleScript obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);

                obstacle.SetSpeed(_obstacleSpeed);

                Debug.Log($"Spawning between {obstacle.GetLowerBound()} and {obstacle.GetUpperBound()}");
                obstacle.transform.position = new Vector3(8f, Random.Range(obstacle.GetLowerBound(), obstacle.GetUpperBound()), 0f);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}