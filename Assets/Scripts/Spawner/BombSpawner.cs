using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
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
