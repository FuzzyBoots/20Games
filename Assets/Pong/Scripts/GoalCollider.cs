using UnityEngine;

namespace Pong
{
    public enum Side
    {
        Left, Right
    }

    [RequireComponent(typeof(Collider))]
    public class GoalCollider : MonoBehaviour
    {
        [SerializeField] Side _side;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                switch (_side)
                {
                    case Side.Left:
                        GameManager.Instance.IncrementLeftScore();
                        break;
                    case Side.Right:
                        GameManager.Instance.IncrementRightScore();
                        break;
                }
            }
        }
    }
}
