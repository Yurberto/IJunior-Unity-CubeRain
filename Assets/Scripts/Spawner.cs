using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Collider _spawnZone;
    [SerializeField] private Cube _prefab;

    [SerializeField, Range(0, 20)] private int _poolDefaultSize = 10;
    [SerializeField, Range(0, 200)] private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;

    public event Action<Cube> CubeEnabled;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => OnActionGet(cube),
            actionOnRelease: (cube) => OnActionRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolDefaultSize,
            maxSize: _poolMaxSize
            );
    }

    private void OnValidate()
    {
        if (_poolMaxSize < _poolDefaultSize)
            _poolMaxSize = _poolDefaultSize + 1;
    }

    public void Spawn()
    {
        var cube = _pool.Get();
        cube.PlatformHitted += Release;
    }

    public void Release(Cube cube)
    {
        cube.PlatformHitted -= Release;
        _pool.Release(cube);
    }

    private void OnActionGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
    }

    private void OnActionRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.transform.position = transform.position;
        cube.transform.rotation = Quaternion.identity;
    }

    private Vector3 GetRandomPosition()
    {
        float minPositionX = _spawnZone.bounds.min.x;
        float maxPositionX = _spawnZone.bounds.max.x;
        float minPositionZ = _spawnZone.bounds.min.z;
        float maxPositionZ = _spawnZone.bounds.max.z;

        float positionY = _spawnZone.transform.position.y;
        float positionX = UnityEngine.Random.Range(minPositionX, maxPositionX);
        float positionZ = UnityEngine.Random.Range(minPositionZ, maxPositionZ);

        return new Vector3(positionX, positionY, positionZ);
    }
}
