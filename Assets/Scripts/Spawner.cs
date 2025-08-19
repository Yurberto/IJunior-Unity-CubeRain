using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Color _prefabColor;
    [SerializeField] private Collider _spawnZone;

    [SerializeField, Range(0, 10)] private int _defaultPoolSize = 10;
    [SerializeField, Range(11, 100)] private int _maxPoolSize = 15;
    [SerializeField, Range(0, 10)] private float _spawnRate = 0.5f;

    private ObjectPool<Cube> _pool;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => GetCube(cube),
            actionOnRelease: (cube) => ReleaseCube(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _defaultPoolSize,
            maxSize: _maxPoolSize            
            );
    }

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void Spawn()
    {
        _pool.Get();
    }

    private void Delete(Cube cube)
    {
        _pool.Release(cube);
        cube.ReleaseTimeCome -= Delete;
    }

    private void GetCube(Cube cube)
    {
        cube.gameObject.SetActive(true);

        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.Rigidbody.velocity = Vector3.zero;
        cube.Rigidbody.angularVelocity = Vector3.zero;
    
        cube.ReleaseTimeCome += Delete;
    }

    private void ReleaseCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private Vector3 GetRandomPosition()
    {
        float minX = _spawnZone.bounds.min.x;
        float maxX = _spawnZone.bounds.max.x;

        float minZ = _spawnZone.bounds.min.z;
        float maxZ = _spawnZone.bounds.max.z;

        float positionY = _spawnZone.bounds.min.y;
        float positionX = Random.Range(minX, maxX);
        float positionZ = Random.Range(minZ, maxZ);

        return new Vector3(positionX, positionY, positionZ);
    }

    private IEnumerator SpawnCoroutine()
    {
        var wait = new WaitForSeconds(_spawnRate);

        while (enabled)
        {
            yield return wait;
            Spawn();
        }
    }
}
