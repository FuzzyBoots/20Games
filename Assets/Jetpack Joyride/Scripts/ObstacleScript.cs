using UnityEngine;

namespace JetpackJoyride
{
    public class ObstacleScript : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Game Over");

                GameManager.Instance.StopGame();
            }
        }
    }
}