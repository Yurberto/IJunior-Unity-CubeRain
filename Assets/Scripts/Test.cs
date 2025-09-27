using UnityEngine;

[RequireComponent(typeof(BombSpawner))]
public class Test : MonoBehaviour
{
    private BombSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<BombSpawner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _spawner.Spawn();
        }
    }
}
