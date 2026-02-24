using System.Collections;
using UnityEngine;

namespace JetpackJoyride {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

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

        [SerializeField] float _gameSpeedUpdateInterval = 5f;
        [SerializeField] float _gameSpeedUpdateAmount = 0.25f;

        Coroutine _gameSpeedCoroutine;

        [SerializeField]
        private float _baseGameSpeed = 3f;

        public float GameSpeed { get; set; }

        private const string HIGH_SCORE = "High Score";
        float distanceTraveled = 0;

        public bool GameSessionActive { get; private set; }

        IEnumerator UpdateGameSpeed()
        {
            WaitForSeconds interval = new WaitForSeconds(_gameSpeedUpdateInterval);

            while (true)
            {
                yield return interval;

                GameSpeed += _gameSpeedUpdateAmount;
            }
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            distanceTraveled = 0;
            UIManager.Instance.UpdateScore(distanceTraveled);

            float highScore = PlayerPrefs.GetFloat(HIGH_SCORE);
            UIManager.Instance.UpdateHighScore(highScore);

            SpawnManager.Instance.StartSpawning();

            if (_gameSpeedCoroutine != null)
            {
                StopCoroutine(_gameSpeedCoroutine);
            }

            GameSpeed = _baseGameSpeed;

            _gameSpeedCoroutine = StartCoroutine(UpdateGameSpeed());

            _gameSessionActive = true;
        }

        public void StopGame()
        {
            SpawnManager.Instance.StopSpawning();

            GameSpeed = 0;

            float highScore = PlayerPrefs.GetFloat(HIGH_SCORE);
            UIManager.Instance.UpdateHighScore(highScore);

            if (distanceTraveled > highScore)
            {
                PlayerPrefs.SetFloat(HIGH_SCORE, distanceTraveled);
            }

            UIManager.Instance.DisplayScoreScreen(distanceTraveled, highScore);
        }

        private void Update()
        {
            distanceTraveled += GameManager.Instance.GameSpeed * Time.deltaTime;
            UIManager.Instance.UpdateScore(distanceTraveled);
        }
    }
}
