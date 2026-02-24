using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Pong
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int _leftScore = 0;
        [SerializeField] int _rightScore = 0;

        [SerializeField] BallScript _ball;

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

        private void Start()
        {
            UIManager.Instance.SetLeftScore(0);
            UIManager.Instance.SetRightScore(0);
        }

        private void RestartGame()
        {
            _ball.ResetBall();
        }

        public void IncrementLeftScore()
        {
            _leftScore++;
            UIManager.Instance.SetLeftScore(_leftScore);

            RestartGame();
        }

        public void IncrementRightScore()
        {
            _rightScore++;
            UIManager.Instance.SetRightScore(_rightScore);

            RestartGame();
        }
    }
}