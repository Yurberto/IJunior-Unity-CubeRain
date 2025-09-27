using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField, Range(0.0f, 10.0f)] private float _spawnDelay = 1.0f;
    [SerializeField] protected Collider _spawnZone;

    public event Action<Cube> CubeReleased;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    protected override void GetAction(Cube @object)
    {
        base.GetAction(@object);
        @object.transform.position = GetRandomPosition();
        @object.Rigidbody.velocity = Vector3.zero;
        @object.Rigidbody.angularVelocity = Vector3.zero;
        @object.StartReleaseCoroutine();
        @object.ReleaseTimeCome += Release; 
    }

    protected override void ReleaseAction(Cube @object)
    {
        base.ReleaseAction(@object);
        @object.ReleaseTimeCome -= Release;
        CubeReleased?.Invoke(@object);
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
