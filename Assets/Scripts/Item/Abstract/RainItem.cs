using System;
using System.Collections;
using UnityEngine;

public abstract class RainItem : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] protected float _minReleaseDelay = 2.0f;
    [SerializeField, Range(0.0f, 10.0f)] protected float _maxReleaseDelay = 5.0f;

    protected Rigidbody _rigidbody;
    protected MeshRenderer _meshRenderer;

    protected Coroutine _releaseCoroutine;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnValidate()
    {
        if (_minReleaseDelay >= _maxReleaseDelay)
            _minReleaseDelay = _maxReleaseDelay - 1;
    }

    protected virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void StartReleaseCoroutine()
    {
        if (_releaseCoroutine != null)
        {
            StopCoroutine(_releaseCoroutine);
            _releaseCoroutine = null;
        }

        _releaseCoroutine = StartCoroutine(ReleaseCoroutine());
    }

    protected abstract IEnumerator ReleaseCoroutine();
}
