using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(BoxCollider), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public bool ColorChanged { get; private set; }

    public event Action<Cube> PlatformHitted;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;

        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            PlatformHitted?.Invoke(this);
            ColorChanged = true;
        }
    }
}
