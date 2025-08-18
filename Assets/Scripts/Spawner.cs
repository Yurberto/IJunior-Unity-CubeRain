using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Color _prefabColor;
    [SerializeField] private Collider _spawnZone;
    [SerializeField] private Colorer _colorer;
    [SerializeField] private Releaser _releaser;

    [SerializeField, Range(0, 10)] private int _defaultPoolSize = 10;
    [SerializeField, Range(11, 100)] private int _maxPoolSize = 15;
    [SerializeField, Range(0, 10)] private float _spawnRate = 0.5f;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => GetCube(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _defaultPoolSize,
            maxSize: _maxPoolSize            
            );
    }

    private void OnEnable()
    {
        _releaser.ReleaseTimeCome += ReleaseCube;
    }

    private void OnDisable()
    {
        _releaser.ReleaseTimeCome += ReleaseCube;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.0f, _spawnRate);
    }

    private void Spawn()
    {
        _pool.Get();
    }

    private void GetCube(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.MeshRenderer.material.color = _prefabColor;

        cube.PlatformHitted += _colorer.SetRandomColor;
        cube.PlatformHitted += _releaser.StartReleaseCoroutine;
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
        cube.PlatformHitted -= _colorer.SetRandomColor;
        cube.PlatformHitted -= _releaser.StartReleaseCoroutine;
        Debug.Log("Release");
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
}
