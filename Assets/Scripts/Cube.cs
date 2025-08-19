using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer), typeof(BoxCollider), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;

    [SerializeField, Range(0, 5)] private float _minReleaseDelay = 2.0f;
    [SerializeField, Range(0, 10)] private float _maxReleaseDelay = 5.0f;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private bool _isPlatformHitted;

    public Rigidbody Rigidbody => _rigidbody;

    public event Action<Cube> PlatformHitted;
    public event Action<Cube> ReleaseTimeCome;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
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
            StartCoroutine(ReleaseEventCorutine());
        }
    }

    private void OnDisable()
    {
        _isPlatformHitted = false;
        _meshRenderer.material.color = _defaultColor;
    }

    private void OnValidate()
    {
        if (_minReleaseDelay >= _maxReleaseDelay)
            _minReleaseDelay = _maxReleaseDelay - 1;
    }

    private IEnumerator ReleaseEventCorutine()
    {
        float delay = Random.Range(_minReleaseDelay, _maxReleaseDelay);
        yield return new WaitForSeconds(delay);
        ReleaseTimeCome?.Invoke(this);
    }
}
