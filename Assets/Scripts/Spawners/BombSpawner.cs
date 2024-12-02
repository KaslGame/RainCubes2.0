using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private float _offsetY = 1f;

    public event Action BombSpawned;

    protected override void ActionOnGet(Bomb obj)
    {
        base.ActionOnGet(obj);
        obj.ResetBomb();
    }

    public void Spawn(Vector3 position)
    {
        Bomb bomb = Get();
        bomb.Died += OnDied;
        bomb.Ready();
        bomb.transform.position = new Vector3(position.x, position.y + _offsetY, position.z);
        BombSpawned?.Invoke();
    }

    private void OnDied(Bomb bomb)
    {
        bomb.Died -= OnDied;
        Release(bomb);
    }
}
