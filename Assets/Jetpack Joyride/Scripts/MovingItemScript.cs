using UnityEngine;

namespace JetpackJoyride
{
    public class MovingItemScript : MonoBehaviour
    {
        [SerializeField] float _leftBound = -7f;

        [SerializeField] Vector2 _bounds = new Vector2(1, 5);

        public float GetLowerBound()
        {
            return Mathf.Min(_bounds.x, _bounds.y);
        }

        public float GetUpperBound()
        {
            return Mathf.Max(_bounds.x, _bounds.y);
        }

        private void Update()
        {
            transform.Translate(GameManager.Instance.GameSpeed * Time.deltaTime * Vector3.left, Space.World);

            if (transform.position.x < _leftBound)
            {
                Destroy(gameObject);    // We should actually be doing pooling, of course
            }
        }
    }
}