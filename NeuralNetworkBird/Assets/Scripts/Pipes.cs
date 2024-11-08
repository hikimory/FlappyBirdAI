using System.Collections;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30f;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
