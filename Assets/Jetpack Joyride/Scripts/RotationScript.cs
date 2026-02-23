using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] Vector3 _rotation;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
