using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;

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
        @object.transform.rotation = Quaternion.identity;
    }

    protected virtual void ReleaseAction(T @object)
    {
        @object.gameObject.SetActive(false);
    }
}
