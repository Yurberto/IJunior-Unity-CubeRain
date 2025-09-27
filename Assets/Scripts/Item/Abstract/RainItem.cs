using System;
using System.Collections;
using UnityEngine;

public abstract class RainItem : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] protected float MinReleaseDelay = 2.0f;
    [SerializeField, Range(0.0f, 10.0f)] protected float MaxReleaseDelay = 5.0f;

    protected Rigidbody Rigidbody;
    protected MeshRenderer MeshRenderer;

    protected Coroutine Releaser;

    public Rigidbody AttachedRigidbody => Rigidbody;

    private void OnValidate()
    {
        if (MinReleaseDelay >= MaxReleaseDelay)
            MinReleaseDelay = MaxReleaseDelay - 1;
    }

    protected virtual void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void StartReleaseCoroutine()
    {
        if (Releaser != null)
        {
            StopCoroutine(Releaser);
            Releaser = null;
        }

        Releaser = StartCoroutine(ReleaseCoroutine());
    }

    protected abstract IEnumerator ReleaseCoroutine();
}
