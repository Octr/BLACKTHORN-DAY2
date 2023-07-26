using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : Singleton<TargetSpawner>
{
    [SerializeField] private GameObject[] targetPrefabs;
    [SerializeField] private Transform[] livingRoomSpawns;
    [SerializeField] private Transform[] kitchRoomSpawns;

    public bool isSpawned;
    [SerializeField] private float lifeTime = 5;

    public RoomType room;

    public int targetKillCount;
    public int roomCounter;

    private int cachedSpawn;

    // Start is called before the first frame update
    void Start()
    {
        targetKillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LifetimeCheck();
        RoomCheck();

        if(!isSpawned)
        {
            int spawnSeed = 0;
            switch (room)
            {
                case RoomType.LIVING_ROOM:
                    spawnSeed = Random.Range(0, livingRoomSpawns.Length);
                    break;
                case RoomType.KITCHEN:
                    spawnSeed = Random.Range(0, kitchRoomSpawns.Length);
                    break;
                default:
                    break;
            }

            int targetSeed = Random.Range(0, targetPrefabs.Length);
            if (spawnSeed == cachedSpawn) return;
            StartCoroutine(Spawn(spawnSeed, targetSeed));
        }
    }

    private IEnumerator Spawn(int spawnSeed, int targetSeed)
    {
        GameObject spawnedTarget = null;

        switch (room)
        {
            case RoomType.LIVING_ROOM:
                spawnedTarget = Instantiate(targetPrefabs[targetSeed], livingRoomSpawns[spawnSeed]);
                break;
            case RoomType.KITCHEN:
                spawnedTarget = Instantiate(targetPrefabs[targetSeed], kitchRoomSpawns[spawnSeed]);
                break;
            default:
                break;
        }

        cachedSpawn = spawnSeed;
        isSpawned = true;
        yield return new WaitForSeconds(lifeTime);
        Destroy(spawnedTarget);
        isSpawned = false;
    }

    private void LifetimeCheck()
    {
        if (lifeTime == 5)
        {
            if (targetKillCount == 5)
            {
                lifeTime = 2.5f;
                RocketSpawnHandler.Instance.CoolDownTime = 0.5f;
            }
        }

        if (lifeTime == 2.5f)
        {
            if (targetKillCount == 10)
            {
                lifeTime = 1.5f;
                RocketSpawnHandler.Instance.CoolDownTime = 0.25f;
            }
        }

        if (lifeTime == 1.5f)
        {
            if (targetKillCount == 20)
            {
                lifeTime = 1;
                RocketSpawnHandler.Instance.CoolDownTime = 0.1f;
            }
        }
    }

    private void RoomCheck()
    {
        if(roomCounter == 0)
        {
            room = RoomType.LIVING_ROOM;
            CameraPan.Instance.SwapCamera(RoomType.LIVING_ROOM);
        }

        if(roomCounter == 10)
        {
            room = RoomType.KITCHEN;
            CameraPan.Instance.SwapCamera(RoomType.KITCHEN);
        }

        if(roomCounter == 20)
        {
            room = RoomType.LIVING_ROOM;
            CameraPan.Instance.SwapCamera(RoomType.LIVING_ROOM);
            roomCounter = 0;
        }
    }
}
