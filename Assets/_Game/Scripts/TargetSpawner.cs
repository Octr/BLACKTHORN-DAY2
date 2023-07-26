using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : Singleton<TargetSpawner>
{
    [SerializeField] private GameObject[] targetPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private bool isSpawned;
    [SerializeField] private float lifeTime = 5;

    public int targetKillCount;

    private int cachedSpawn;

    // Start is called before the first frame update
    void Start()
    {
        targetKillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime == 5)
        {
            if(targetKillCount == 10)
            {
                lifeTime = 3f;
            }
        }

        if(lifeTime == 3)
        {
            if(targetKillCount == 20)
            {
                lifeTime = 2f;
            }
        }

        if(!isSpawned)
        {
            int spawnSeed = Random.Range(0, spawnPositions.Length);
            int targetSeed = Random.Range(0, targetPrefabs.Length);

            if (spawnSeed == cachedSpawn) return;
            StartCoroutine(Spawn(spawnSeed, targetSeed));
        }
    }

    private IEnumerator Spawn(int spawnSeed, int targetSeed)
    {
        GameObject spawnedTarget = Instantiate(targetPrefabs[targetSeed], spawnPositions[spawnSeed]);
        cachedSpawn = spawnSeed;
        isSpawned = true;
        yield return new WaitForSeconds(lifeTime);
        Destroy(spawnedTarget);
        isSpawned = false;
    }
}
