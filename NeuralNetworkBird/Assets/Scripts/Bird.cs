using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private LayerMask _collisionMask;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _fallSpeed = 2f;
    [SerializeField]
    private float _upTiltAngle = 15f;
    [SerializeField]
    private float _downTiltAngle = 60f;
    [SerializeField]
    private float _maxSpeedY = 4.8038f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y > _maxSpeedY)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, _maxSpeedY, rb.linearVelocity.z);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }
        Rotate();
    }

    private void Jump()
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(0, 180, -_upTiltAngle);
    }

    private void Rotate()
    {
        if (rb.linearVelocity.y < 0)
        {
            float angle = Mathf.LerpAngle(transform.eulerAngles.z, _downTiltAngle, Time.deltaTime * _fallSpeed);
            transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else if (rb.linearVelocity.y > 0)
        {
            float angle = Mathf.LerpAngle(transform.eulerAngles.z, -_upTiltAngle, Time.deltaTime * _fallSpeed);
            transform.rotation = Quaternion.Euler(0, 180, angle);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer & _collisionMask) == 0)
        {
            Time.timeScale = 0;
        }
    }
}
