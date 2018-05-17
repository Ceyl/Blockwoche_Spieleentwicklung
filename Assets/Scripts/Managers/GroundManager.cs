using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

    // plates to generate stages
    [SerializeField] private GameObject Ground1x1Prefab;
    [SerializeField] private byte Ground1x1Multiplier = 4;
    [SerializeField] private GameObject Ground4x1Prefab;
    [SerializeField] private byte Ground4x1Multiplier = 2;
    [SerializeField] private GameObject Ground8x1Prefab;
    [SerializeField] private byte Ground8x1Multiplier = 1;

    // distance between stages
    [SerializeField] private float minYDistance = 10;
    [SerializeField] private float maxYDistance = 15;

    // amount of blocks per stage
    [SerializeField] private byte minAmountBlocks = 2;
    [SerializeField] private byte maxAmountBlocks = 3;

    public float actualPoint = -9.25f;

    // Use this for initialization
    void Awake () {
        while (actualPoint < 20)
            RandomGround();
	}


    public void RandomGround()
    {
        // check out which plattforms shall be used to gernerate a stage
        byte randomGround = (byte)Random.Range(0, 2);
        // random distance in which a stage can be created 
        float randomDistance = Random.Range(minYDistance, maxYDistance);
        // amount of plates for next stage
        byte amount = 0;
        // space from camera borders
        byte space = 0;

        // check which plates shall be used
        switch (randomGround)
        {
            case 0:
                amount = (byte)Random.Range(minAmountBlocks * Ground1x1Multiplier, maxAmountBlocks * Ground1x1Multiplier);
                space = 1;
                SpawnGround(Ground1x1Prefab, amount, space);
                break;
            case 1:
                amount = (byte)Random.Range(minAmountBlocks * Ground4x1Multiplier, maxAmountBlocks * Ground4x1Multiplier);
                space = 2;
                SpawnGround(Ground4x1Prefab, amount, space);
                break;
            case 2:
                amount = (byte)Random.Range(minAmountBlocks * Ground8x1Multiplier, maxAmountBlocks * Ground8x1Multiplier);
                space = 4;
                SpawnGround(Ground8x1Prefab, amount, space);
                break;
        }
        actualPoint += randomDistance;
    }

    private void SpawnGround(GameObject prefab, byte amount, byte space)
    {
        // spawns plates at the current y value
        for (int i = 0; i < amount; i++)
        {
            float spawnPointRange = Random.Range(-35, 35);
            spawnPointRange = spawnPointRange < 0 ? spawnPointRange + space : spawnPointRange - space;
            GameObject spawnedFloor = Instantiate(prefab, new Vector3(spawnPointRange, actualPoint, 0), Quaternion.identity);
        }
    }
}
