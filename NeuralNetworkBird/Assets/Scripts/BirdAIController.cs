using System.IO;
using System.Text;
using UnityEngine;

public class BirdAIController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _raycastMask;
    [SerializeField]
    private LayerMask _collisionMask;
    [SerializeField]
    private int _forwardRayLength = 15;
    [SerializeField]
    private int _upRayLength = 7;
    [SerializeField]
    private int _downRayLength = 7;

    [SerializeField]
    private BirdLaser _birdLaser;

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

    private NeuralNetwork.NeuralNetwork nn;

    private void Awake()
    {
        nn = new NeuralNetwork.NeuralNetwork(new NeuralNetwork.ActivationFunctions.SigmoidActivationFunction(), new uint[] { 4, 5, 1 });
        nn.LoadWeightsAndBiases("Assets/Data_NN/Values.txt");
    }

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

    void Update()
    {
        // Neural Network
        var top = _birdLaser.topValue;
        var bottom = _birdLaser.bottomValue;
        var centerDist = transform.position.y - _birdLaser.centerValue;
        var centerDistNorm = Mathf.Clamp((float)centerDist / 2.23f, -1f, 1f);
        var velocity = Mathf.Clamp(rb.linearVelocity.y / _maxSpeedY, -1f, 1f);
        var output = nn.FeedForward(new double[] { top, bottom, centerDistNorm, velocity })[0];

        if (output > 0.5)
        {
            Jump();
        }
        Rotate();

        // Training
        //var top = _birdLaser.topValue;
        //var bottom = _birdLaser.bottomValue;
        //var centerDist = transform.position.y - _birdLaser.centerValue;
        //var centerDistNorm = Mathf.Clamp((float)centerDist / 2.23f, -1f, 1f);
        //var velocity = Mathf.Clamp(rb.linearVelocity.y / _maxSpeedY, -1f, 1f);
        //int pressed = 0;
        //if (centerDistNorm < -0.62)
        //{
        //    pressed = 1;
        //    Jump();
        //}
        //Rotate();
        //SaveData("Test.txt", new double[] { top, bottom, centerDistNorm, velocity, pressed });
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
        {
            _birdLaser.UpdatePipesCenter();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer & _collisionMask) == 0)
        {
            Time.timeScale = 0;
        }
    }

    private void SaveData(string filePath, double[] input)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Join(" ", input));
            writer.WriteLine(sb.ToString());
        }
    }
}
