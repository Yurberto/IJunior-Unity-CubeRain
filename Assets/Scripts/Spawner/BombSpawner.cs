using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnOnPosition(Vector3 position)
    {
        Bomb spawned = Pool.Get();
        spawned.InitializePosition(position);
        SpawnedCounter++;
    }

    protected override void GetAction(Bomb @object)
    {
        base.GetAction(@object);
        @object.AttachedRigidbody.velocity = Vector3.zero;
        @object.AttachedRigidbody.angularVelocity = Vector3.zero;
        @object.StartReleaseCoroutine();
        @object.Exploded += Release;
    }

    protected override void ReleaseAction(Bomb @object)
    {
        base.ReleaseAction(@object);
        @object.Exploded -= Release;
    }
}
