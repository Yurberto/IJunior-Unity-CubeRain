using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Collider _spawnZone;

    protected ObjectPool<T> _pool;
    protected Coroutine _spawnCoroutine;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>
            (
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (@object) => GetAction(@object),
                actionOnRelease: (@object) => ReleaseAction(@object),
                actionOnDestroy: (@object) => Destroy(@object.gameObject)
            );
    }

    public virtual void Spawn()
    {
        _pool.Get();
    }

    public virtual void Release(T @object)
    {
        _pool.Release(@object);
    }

    protected virtual void GetAction(T @object)
    {
        @object.gameObject.SetActive(true);
        @object.transform.position = GetRandomPosition();
        @object.transform.rotation = Quaternion.identity;
    }

    protected virtual void ReleaseAction(T @object)
    {
        @object.gameObject.SetActive(false);
    }

    protected Vector3 GetRandomPosition()
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
