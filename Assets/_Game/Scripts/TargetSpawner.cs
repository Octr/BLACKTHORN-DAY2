using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] targetPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private bool isSpawned;
    [SerializeField] private float lifeTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawned)
        {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        int spawnSeed = Random.Range(0, spawnPositions.Length);
        int targetSeed = Random.Range(0, targetPrefabs.Length);
        GameObject spawnedTarget = Instantiate(targetPrefabs[targetSeed], spawnPositions[spawnSeed]);
        isSpawned = true;
        yield return new WaitForSeconds(lifeTime);
        Destroy(spawnedTarget);
        isSpawned = false;
    }
}
