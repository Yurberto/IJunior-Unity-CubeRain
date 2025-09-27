using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent (typeof(SphereCollider), typeof(Rigidbody), typeof(MeshRenderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] private float _minReleaseDelay = 2.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _maxReleaseDelay = 5.0f;

    [SerializeField, Range(0.0f, 15.0f)] private float _explosionForce = 3.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _explosionRadius = 5.0f;

    [SerializeField, Range(0, 100)] private int _maxExplodedColliders = 20;

    private MeshRenderer _meshRenderer;

    private Rigidbody _rigidbody;
    private Coroutine _explosion;

    public event Action<Bomb> Exploded;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

        MaterialUtils.SetupMaterialForTransparency(_meshRenderer.material);
    }

    private void OnEnable()
    {
        SetAlpha(MaterialUtils.MaxAlpha); 
    }

    public void StartExplosion()
    {
        if (_explosion != null)
        {
            StopCoroutine(_explosion);
            _explosion = null;
        }

        _explosion = StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        float alpha = MaterialUtils.MaxAlpha;

        float explodeTime = Random.Range(_minReleaseDelay, _maxReleaseDelay);
        float timer = 0;

        while (timer < explodeTime)
        {
            float progress = timer / explodeTime;
            alpha = Mathf.Lerp(MaterialUtils.MaxAlpha, 0, progress);
            SetAlpha(alpha);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Explode();
    }

    private void Explode()
    {
        float defaultModifier = 0.0f;
        Collider[] hitted = new Collider[_maxExplodedColliders];
        int hittedQuantity = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, hitted);

        for (int i = 0; i < hittedQuantity; i++)
        {
            if (hitted[i].attachedRigidbody != null)
                hitted[i].attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, defaultModifier, ForceMode.Impulse);
        }

        Exploded?.Invoke(this);
    }

    private void SetAlpha (float alpha)
    {
        Color color = _meshRenderer.material.color;
        color.a = Mathf.Clamp01(alpha);
        _meshRenderer.material.color = color;
    }
}
