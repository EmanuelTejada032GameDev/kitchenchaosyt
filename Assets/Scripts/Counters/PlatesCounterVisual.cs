using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform platePrefabVisual;
    [SerializeField] private Transform CounterTop;

    private List<GameObject> spawnedPlatesVisuals;

    private void Awake()
    {
        spawnedPlatesVisuals = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject lastPlateSpawnedPrefabVisual = spawnedPlatesVisuals[spawnedPlatesVisuals.Count - 1];
        spawnedPlatesVisuals.Remove(lastPlateSpawnedPrefabVisual);
        Destroy(lastPlateSpawnedPrefabVisual);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform spawnedPlatePrefab = Instantiate(platePrefabVisual,CounterTop);
        float plateVisualSpawnOffset = 0.1f;
        spawnedPlatePrefab.transform.localPosition = new Vector3(0, spawnedPlatesVisuals.Count * plateVisualSpawnOffset, 0);
        spawnedPlatesVisuals.Add(spawnedPlatePrefab.gameObject);
    }
}
