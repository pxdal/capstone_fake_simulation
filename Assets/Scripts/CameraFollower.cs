using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;

    public float lerpFactor = 0.1f;

    private Vector3 m_offset;
    private float m_initialY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_offset = transform.position - target.position;
        m_initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + m_offset;
        targetPosition.y = m_initialY;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpFactor * Time.deltaTime);
    }
}
