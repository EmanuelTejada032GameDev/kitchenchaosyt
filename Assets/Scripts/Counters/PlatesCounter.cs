using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    private float plateSpawnTimer;
    private float plateSpawnTimerMax = 4f;
    private int maxPlateToSpawn = 4;
    private int platesSpawned = 0;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public EventHandler OnPlateSpawned;
    public EventHandler OnPlateRemoved;


    private void Start()
    {
      
    }

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        if (plateSpawnTimer >= plateSpawnTimerMax)
        {
            plateSpawnTimer = 0f;

            if (platesSpawned == maxPlateToSpawn) return;
            platesSpawned++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Interact(Player player)
    {
        if (platesSpawned == 0) return;
        
        if (!player.HasKitchenObject)
        {
            platesSpawned--;
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
