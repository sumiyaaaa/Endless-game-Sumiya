using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
    }
}
