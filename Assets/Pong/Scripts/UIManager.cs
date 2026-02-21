using TMPro;
using UnityEngine;

namespace Pong
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TMP_Text _leftScoreText;
        [SerializeField] TMP_Text _rightScoreText;

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

        internal void SetLeftScore(int leftScore)
        {
            _leftScoreText.text = leftScore.ToString();
        }

        internal void SetRightScore(int rightScore)
        {
            _rightScoreText.text = rightScore.ToString();
        }
    }
}
