using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{

    public GameObject[] areaPrefabs;

    public int spawnAreaCountAtStart = 3;

    public float zDistance = 20;
    private int areaIndex = 0;

    private void Awake()
    {
        for(int i = 0; i < spawnAreaCountAtStart; i++)
        {
            if (i == 0)
            {
                SpawnArea(false);
            }
            else
            {
                SpawnArea();
            }
        }
    }

    public void SpawnArea(bool isRandam = true)
    {
        GameObject clone = null;

        if (isRandam == false)
        {
            clone = Instantiate(areaPrefabs[0]);
        }
        else
        {
            int index = Random.Range(0, areaPrefabs.Length);
            clone = Instantiate(areaPrefabs[index]);
        }

        clone.transform.position = new Vector3 (0, 0, areaIndex * zDistance);

        areaIndex++;
    }


}
