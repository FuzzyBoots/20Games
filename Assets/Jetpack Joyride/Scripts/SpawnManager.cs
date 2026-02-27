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

        [SerializeField] MovingItemScript[] _obstaclePrefabs;

        [SerializeField] MovingItemScript[] _floorPrefabs;
        MovingItemScript _lastFloor;
        [SerializeField] float _tileWidth = 8f;
        [SerializeField] float _spawnXPosition = 12f; // Where tiles appear on the right

        GameObject _obstacleContainer;
        GameObject _floorContainer;

        bool _spawning = false;

        public void StartSpawning()
        {
            _spawning = true;

            if (_obstacleContainer) Destroy(_obstacleContainer);
            if (_floorContainer) Destroy(_floorContainer);

            _obstacleContainer = new GameObject();
            _floorContainer = new GameObject();

            StartCoroutine(SpawnObstacles());

            BuildInitialFloor();
        }

        MovingItemScript GetRandomFloor() {             
            return _floorPrefabs[Random.Range(0, _floorPrefabs.Length)];
        }

        private void BuildInitialFloor()
        {
            // Generate tiles at -4 and 4
            _lastFloor = Instantiate(GetRandomFloor(), new Vector3(-4f, 0f, 0f), Quaternion.identity);
            _lastFloor.transform.parent = _floorContainer.transform;
            _lastFloor = Instantiate(GetRandomFloor(), new Vector3( 4f, 0f, 0f), Quaternion.identity);
            _lastFloor.transform.parent = _floorContainer.transform;
        }

        public void StopSpawning()
        {
            _spawning = false;
        }

        public void Update()
        {
            if (_lastFloor.transform.position.x < _spawnXPosition)
            {
                _lastFloor = Instantiate(GetRandomFloor(), new Vector3(_lastFloor.transform.position.x + _tileWidth, 0f, 0f), Quaternion.identity);
                _lastFloor.transform.parent = _floorContainer.transform;
            }
        }

        IEnumerator SpawnObstacles()
        {
            while (_spawning)
            {
                MovingItemScript obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)]);

                obstacle.transform.position = new Vector3(8f, Random.Range(obstacle.GetLowerBound(), obstacle.GetUpperBound()), 0f);
                obstacle.transform.parent = _obstacleContainer.transform;

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}