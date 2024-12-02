using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : SpawnedObject<Cube>
{
    [SerializeField] private Material _blackBlueMaterial;
    [SerializeField] private Material _blueMaterial;

    private MeshRenderer _meshRenderer;
    private Coroutine _coroutine;
    public event Action<Cube> Died;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && _coroutine == null)
            _coroutine = StartCoroutine(LifeTimer(GenerateRandomNumber()));
    }

    private float GenerateRandomNumber()
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
