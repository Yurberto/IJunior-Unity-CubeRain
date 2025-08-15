using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour 
{
    [SerializeField, Range(0, 5)] private float _minReleaseDelay = 2f;
    [SerializeField, Range(0, 10)] private float _maxReleaseDelay = 5f;

    public event Action<Cube> PlatformHitted;

    private void OnValidate()
    {
        if (_minReleaseDelay >= _maxReleaseDelay)
            _minReleaseDelay = _maxReleaseDelay - 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
            StartCoroutine(InvokeAfterDelay());
    }

    private IEnumerator InvokeAfterDelay()
    {
        float releaseDelay = UnityEngine.Random.Range(_minReleaseDelay, _maxReleaseDelay);
        var wait = new WaitForSeconds(releaseDelay);
        yield return wait;

        PlatformHitted?.Invoke(this);
    }
}

