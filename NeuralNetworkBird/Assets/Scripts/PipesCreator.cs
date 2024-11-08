using UnityEngine;

public class PipesCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Transform _spawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("CreateObject", 2f, 2.5f);
    }

    void CreateObject()
    {
        Vector3 pos = _spawnPosition.position;
        pos.y = Random.Range(-3.5f, 3.5f);
        GameObject pipes = Instantiate(_prefab, pos, _spawnPosition.rotation);
        pipes.transform.SetParent(_spawnPosition);
    }
}
