using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] targetPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private bool isSpawned;
    [SerializeField] private float lifeTime = 5;

    private int cachedSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
