using UnityEngine;

public class PipeDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
