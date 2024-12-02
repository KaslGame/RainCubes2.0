using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : SpawnedObject<Bomb>
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _minDuration = 2;
    [SerializeField] private float _maxDuration = 5;

    private FadeObject _fadeObject;
    private Coroutine _lifeTimeCoroutine;

    public event Action<Bomb> Died;

    private void Awake()
    {
        _fadeObject = GetComponent<FadeObject>();
    }

    public void Ready()
    {
        StartLifeTimeCoroutine();
    }

    public void ResetBomb()
    {
        _fadeObject.ResetFade();
        _lifeTimeCoroutine = null;
    }

    private IEnumerator WaitExplode(float time)
    {
        WaitForSeconds wait = new WaitForSeconds(time);

        yield return wait;

        Explode(GetExployedableObjects(transform.position), transform.position);
    }

    private void Explode(List<Rigidbody> objects, Vector3 postion)
    {
        foreach (Rigidbody explodableObject in objects)
            explodableObject.AddExplosionForce(_explosionForce, postion, _explosionRadius);
    }

    private List<Rigidbody> GetExployedableObjects(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _explosionRadius);

        List<Rigidbody> objects = (from Collider hit in hits where hit.attachedRigidbody != null select hit.attachedRigidbody).ToList();

        return objects;
    }

    private float RandomDuration()
    {
        return UnityEngine.Random.Range(_minDuration, _maxDuration);
    }

    private IEnumerator LifeTimeCoroutine()
    {
        float LifeTime = RandomDuration();
        _fadeObject.FadeOut(LifeTime);
        yield return new WaitForSeconds(LifeTime);
        Explode(GetExployedableObjects(transform.position), transform.position);
        StopCoroutine(_lifeTimeCoroutine);
        Died?.Invoke(this);
    }

    private void StartLifeTimeCoroutine()
    {
        if (_lifeTimeCoroutine != null)
            StopCoroutine(_lifeTimeCoroutine);

        _lifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine());
    }
}
