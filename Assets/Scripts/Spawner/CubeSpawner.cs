using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField, Range(0.0f, 10.0f)] private float _spawnDelay = 1.0f;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    protected override void GetAction(Cube @object)
    {
        base.GetAction(@object);
        @object.Rigidbody.velocity = Vector3.zero;
        @object.Rigidbody.angularVelocity = Vector3.zero;
        @object.StartReleaseCoroutine();
        @object.ReleaseTimeCome += Release; 
    }

    protected override void ReleaseAction(Cube @object)
    {
        base.ReleaseAction(@object);
        @object.ReleaseTimeCome -= Release;
    }

    private IEnumerator SpawnCoroutine()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }
}
