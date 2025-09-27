using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(BoxCollider), typeof(Rigidbody))]
public class Cube : RainItem
{
    [SerializeField] private Color _defaultColor = Color.white;

    private bool _isPlatformHitted;

    public event Action<Cube> ReleaseTimeCome;
    public event Action<Cube> PlatformHitted;

    private void OnDisable()
    {
        _isPlatformHitted = false;
        MeshRenderer.material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformHitted)
            return;

        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            PlatformHitted?.Invoke(this);
            _isPlatformHitted = true;
            MeshRenderer.material.color = Random.ColorHSV();
            StartCoroutine(ReleaseCoroutine());
        }
    }

    protected override IEnumerator ReleaseCoroutine()
    {
        float delay = Random.Range(MinReleaseDelay, MaxReleaseDelay);
        yield return new WaitForSeconds(delay);
        ReleaseTimeCome?.Invoke(this);
    }
}
