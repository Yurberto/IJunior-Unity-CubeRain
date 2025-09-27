using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;

    protected float SpawnedCounter = 0;

    protected ObjectPool<T> Pool;
    protected Coroutine SpawnCoroutine;

    public float SpawnedCount => SpawnedCounter;
    public float CreatedCount => Pool.CountAll;
    public float ActiveCount => Pool.CountActive;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>
            (
                createFunc: () => Instantiate(Prefab),
                actionOnGet: (@object) => GetAction(@object),
                actionOnRelease: (@object) => ReleaseAction(@object),
                actionOnDestroy: (@object) => Destroy(@object.gameObject)
            );
    }

    public virtual void Spawn()
    {
        Pool.Get();
        SpawnedCounter++;
    }

    public virtual void Release(T @object)
    {
        Pool.Release(@object);
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
