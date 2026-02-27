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
        [SerializeField] GameObject _restartDialog;

        public void UpdateScore(float score)
        {
            _scoreText.text = $"Distance: {score:F2}";
        }

        public void UpdateHighScore(float score)
        {
            _highScoreText.text = $"High Score: {score:F2}";
        }

        public void DisplayRestartScreen(bool enabled)
        {
            _restartDialog.SetActive(enabled);
        }
    }
}