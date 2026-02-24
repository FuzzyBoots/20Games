using System;
using TMPro;
using UnityEngine;

namespace JetpackJoyride
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

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

        [SerializeField] TMP_Text _scoreText;
        [SerializeField] TMP_Text _highScoreText;

        public void UpdateScore(float score)
        {
            _scoreText.text = $"Distance: {score}";
        }

        public void UpdateHighScore(float score)
        {
            _highScoreText.text = $"High Score: {score}";
        }

        public void DisplayScoreScreen(float distanceTraveled, float highScore)
        {
            // How do we want to handle this?
        }
    }
}