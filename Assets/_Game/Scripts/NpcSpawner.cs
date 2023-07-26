using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : Singleton<NpcSpawner>
{
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private float cooldown = 5;

    [SerializeField] private int maxNpcs = 5;
    public int currentNpcs;

    public bool isSpawned;

    // Update is called once per frame
    void Update()
    {
        if(!isSpawned)
        {
            if (currentNpcs > maxNpcs) return;
            int spawnSeed = Random.Range(0, npcPrefabs.Length);
            StartCoroutine(Spawn(spawnSeed));
        }
    }

    private IEnumerator Spawn(int spawnSeed)
    {
        GameObject spawnedNpc = Instantiate(npcPrefabs[spawnSeed]);
        currentNpcs++;
        isSpawned = true;
        yield return new WaitForSeconds(cooldown);
        isSpawned = false;
    }
}
