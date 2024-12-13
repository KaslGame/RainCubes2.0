using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : SpawnedObject<Cube>
{
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public event Action<Cube> Died;

    private Color _defaultColor;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _defaultColor = _meshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && _coroutine == null)
            _coroutine = StartCoroutine(LifeTimer(GenerateNumber()));
    }

    public void ResetParameters()
    {
        _meshRenderer.material.color = _defaultColor;
        _rigidbody.velocity = Vector3.zero;
    }

    private float GenerateNumber()
    {
        int minLifeTime = 2;
        int maxLifeTime = 5;
        
        return UnityEngine.Random.Range(minLifeTime, maxLifeTime);
    }

    private IEnumerator LifeTimer(float time)
    {
        WaitForSeconds wait = new WaitForSeconds(time);

        _meshRenderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        yield return wait;

        Died?.Invoke(this);
        _coroutine = null;
    }
}
