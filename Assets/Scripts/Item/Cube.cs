using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(BoxCollider), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;

    [SerializeField, Range(0.0f, 10.0f)] private float _minReleaseDelay = 2.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _maxReleaseDelay = 5.0f;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    
    private Coroutine _releaseCoroutine;
    private bool _isPlatformHitted;

    public event Action<Cube> ReleaseTimeCome;
    public event Action<Cube> PlatformHitted;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnValidate()
    {
        if (_minReleaseDelay >= _maxReleaseDelay)
            _minReleaseDelay = _maxReleaseDelay - 1;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDisable()
    {
        _isPlatformHitted = false;
        _meshRenderer.material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformHitted)
            return;

        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            PlatformHitted?.Invoke(this);
            _isPlatformHitted = true;
            _meshRenderer.material.color = Random.ColorHSV();
            StartCoroutine(ReleaseCoroutine());
        }
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

    private IEnumerator ReleaseCoroutine()
    {
        float delay = Random.Range(_minReleaseDelay, _maxReleaseDelay);
        yield return new WaitForSeconds(delay);
        ReleaseTimeCome?.Invoke(this);
    }
}
