using UnityEngine;

public class SpawnedObject<T> : MonoBehaviour where T : SpawnedObject<T>
{
    protected Spawner<T> Spawner;

    public void AddSpawner(Spawner<T> spawner)
    {
        Spawner = spawner;
    }
}
