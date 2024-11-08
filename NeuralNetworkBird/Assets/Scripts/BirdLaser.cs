using UnityEngine;

public class BirdLaser : MonoBehaviour
{
    [SerializeField]
    private LayerMask _pipesMask;
    [SerializeField]
    private LayerMask _wallMask;
    [SerializeField]
    private int _forwardRayLength = 30;
    [SerializeField]
    private int _upRayLength = 7;
    [SerializeField]
    private int _downRayLength = 7;

    public double topValue;
    public float centerValue;
    public double bottomValue;

    void Start()
    {
        UpdatePipesCenter();
    }

    void Update()
    {

        RaycastHit hit;

        Ray upRay = new Ray(transform.position, Vector3.up);
        if (Physics.Raycast(upRay, out hit, _upRayLength, _wallMask))
        {
            Debug.DrawLine(upRay.origin, hit.point, Color.red);
            topValue = hit.distance / _upRayLength;
        }
        else
        {
            topValue = 1;
            Debug.DrawLine(upRay.origin, upRay.origin + Vector3.up * _upRayLength, Color.green);
        }

        Ray downRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(downRay, out hit, _downRayLength, _wallMask))
        {
            Debug.DrawLine(downRay.origin, hit.point, Color.red);
            bottomValue = hit.distance / _downRayLength;
        }
        else
        {
            bottomValue = 1;
            Debug.DrawLine(downRay.origin, downRay.origin + Vector3.down * _downRayLength, Color.green);
        }
    }

    public void UpdatePipesCenter()
    {
        RaycastHit hit;
        Ray rightRay = new Ray(transform.position, Vector3.right);
        if (Physics.Raycast(rightRay, out hit, _forwardRayLength, _pipesMask))
        {
            Debug.DrawLine(rightRay.origin, hit.point, Color.red);
            centerValue = hit.transform.position.y;
        }
    }
}
