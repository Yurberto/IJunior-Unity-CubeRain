using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnOnPosition(Vector3 position)
    {
        Bomb spawned = _pool.Get();
        spawned.InitializePosition(position);
    }

    protected override void GetAction(Bomb @object)
    {
        base.GetAction(@object);
        @object.Rigidbody.velocity = Vector3.zero;
        @object.Rigidbody.angularVelocity = Vector3.zero;
        @object.StartReleaseCoroutine();
        @object.Exploded += Release;
    }

    protected override void ReleaseAction(Bomb @object)
    {
        base.ReleaseAction(@object);
        @object.Exploded -= Release;
    }
}
