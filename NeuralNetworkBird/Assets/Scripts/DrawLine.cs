using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float laserLength = 30;
    public Transform startPoint;
    public Transform endPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);

        Vector3 endPosition = transform.position;
        endPosition.x -= laserLength;

        lineRenderer.SetPosition(1, endPosition);
    }
}
