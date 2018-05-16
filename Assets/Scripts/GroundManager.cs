using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

    [SerializeField] private GameObject Ground1x1Prefab;
    [SerializeField] private GameObject Ground4x1Prefab;
    [SerializeField] private GameObject Ground8x1Prefab;
    public float actualPoint = -14.25f;

    // Use this for initialization
    void Awake () {
        while (actualPoint < 20)
            RandomGround();
	}


    public void RandomGround()
    {
        byte randomGround = (byte)Random.Range(0, 2);
        float randomDistance = Random.Range(10, 15);
        byte amount = 0;
        byte space = 0;
        switch (randomGround)
        {
            case 0:
                amount = (byte)Random.Range(8, 12);
                space = 1;
                SpawnGround(Ground1x1Prefab, amount, space);
                break;
            case 1:
                amount = (byte)Random.Range(4, 6);
                space = 2;
                SpawnGround(Ground4x1Prefab, amount, space);
                break;
            case 2:
                amount = (byte)Random.Range(2, 3);
                space = 4;
                SpawnGround(Ground8x1Prefab, amount, space);
                break;
        }
        actualPoint += randomDistance;
    }

    private void SpawnGround(GameObject prefab, byte amount, byte space)
    {
        for (int i = 0; i < amount; i++)
        {
            float spawnPointRange = Random.Range(-35, 35);
            spawnPointRange = spawnPointRange < 0 ? spawnPointRange + space : spawnPointRange - space;
            GameObject spawnedFloor = Instantiate(prefab, new Vector3(spawnPointRange, actualPoint, 0), Quaternion.identity);
        }
    }
}
