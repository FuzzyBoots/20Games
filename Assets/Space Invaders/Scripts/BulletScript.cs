using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]  float _speed = 5f;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * _speed * transform.up);

        if (transform.position.y > 10)
            {
                Destroy(gameObject);
        }
    }
}
