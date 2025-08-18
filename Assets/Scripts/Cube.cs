using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(BoxCollider), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _minReleaseValue = 2f;
    [SerializeField, Range(0, 10)] private float _maxReleaseValue = 5f;

    private MeshRenderer _meshRenderer;
    private bool _isPlatformHitted;

    public MeshRenderer MeshRenderer => _meshRenderer;
    public float MinReleaseValue => _minReleaseValue;
    public float MaxReleaseValue => _maxReleaseValue;

    public event Action<Cube> PlatformHitted;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformHitted == true)
            return;

        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            PlatformHitted?.Invoke(this);
            _isPlatformHitted = true;
        }
    }

    private void OnDisable()
    {
        _isPlatformHitted = false;
    }

    private void OnValidate()
    {
        if (_minReleaseValue > _maxReleaseValue)
            _minReleaseValue = _maxReleaseValue - 1;
    }
}
