using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField, Range(0f, 0.5f)]
    private float _speed = 0.2f;
    private Material _mat;
    private float _distance;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        _distance += Time.deltaTime * _speed;
        _mat.SetTextureOffset("_MainTex", Vector2.right * _distance);
    }
}
