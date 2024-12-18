using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : SpawnedObject<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolSize = 5;

    private ObjectPool<T> _pool;

    public event Action ObjectCreated;
    public event Action Spawned;

    public int PoolCapacity => _poolCapacity;
    public int PoolInactive => _pool.CountInactive;
    public int PoolActive => _pool.CountActive;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => CreateObject(),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolSize);
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    protected virtual void ActionOnGet(T obj) 
    { 
        obj.gameObject.SetActive(true);
    }

    protected T Get()
    {
        Spawned?.Invoke();
        return _pool.Get();
    }

    private T CreateObject()
    {
        ObjectCreated?.Invoke();
        return Instantiate(_prefab);
    }
}
