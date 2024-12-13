using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour where T : SpawnedObject<T>
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TMP_Text _textSpawn;
    [SerializeField] private TMP_Text _textCreated;
    [SerializeField] private TMP_Text _textActive;

    private int _countSpawns;
    private int _countCreated;

    private void OnEnable()
    {
        _spawner.Spawned += OnSpawned;
        _spawner.ObjectCreated += OnObjectCreated;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= OnSpawned;
        _spawner.ObjectCreated -= OnObjectCreated;
    }

    private void Update()
    {
        _textActive.text = "Количество активных объектов на сцене: " + _spawner.PoolActive;
    }

    private void OnSpawned()
    {
        _countSpawns++;
        _textSpawn.text = "Количество заспавненых объектов за всё время: " + _countSpawns;
    }

    private void OnObjectCreated()
    {
        _countCreated++;
        _textCreated.text = "Количество созданных объектов: " + _countCreated;
    }
}
