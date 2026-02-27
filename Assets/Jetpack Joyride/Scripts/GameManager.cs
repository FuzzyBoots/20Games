using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        [field: SerializeField]
        public float GameSpeed { get; set; }

        private const string HIGH_SCORE = "High Score";
        float distanceTraveled = 0;
        private JetpackInputs _inputs;

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
            GameSessionActive = true;
            _inputs = new JetpackInputs();
            _inputs.Jetpack.Enable();

            _inputs.Jetpack.Restart.performed += Restart_performed;

            StartGame();
        }

        private void Restart_performed(InputAction.CallbackContext context)
        {
            Debug.Log(GameSessionActive);
            if (!GameSessionActive)
            {
                SceneManager.LoadScene(0);
            }
        }

        public void StartGame()
        {
            distanceTraveled = 0;
            UIManager.Instance.UpdateScore(distanceTraveled);

            float highScore = PlayerPrefs.GetFloat(HIGH_SCORE);
            UIManager.Instance.UpdateHighScore(highScore);

            SpawnManager.Instance.StartSpawning();

            GameSpeed = _baseGameSpeed;

            _gameSpeedCoroutine = StartCoroutine(UpdateGameSpeed());

            Debug.Log("Game Started");
            GameSessionActive = true;
        }

        public void StopGame()
        {
            SpawnManager.Instance.StopSpawning();

            GameSpeed = 0;

            if (_gameSpeedCoroutine != null)
            {
                StopCoroutine(_gameSpeedCoroutine);
            }

            float highScore = PlayerPrefs.GetFloat(HIGH_SCORE);
            UIManager.Instance.UpdateHighScore(highScore);

            if (distanceTraveled > highScore)
            {
                PlayerPrefs.SetFloat(HIGH_SCORE, distanceTraveled);

                UIManager.Instance.UpdateHighScore(distanceTraveled);
            }

            UIManager.Instance.DisplayRestartScreen(true);
            GameSessionActive = false;
        }

        private void Update()
        {
            if (!GameManager.Instance.GameSessionActive) return;

            distanceTraveled += GameManager.Instance.GameSpeed * Time.deltaTime;
            UIManager.Instance.UpdateScore(distanceTraveled);
        }
    }
}
