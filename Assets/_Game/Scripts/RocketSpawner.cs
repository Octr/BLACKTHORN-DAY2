using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _rocketPrefabs;

    //Object that all rockets become children of. To organise heirarchy
    private Transform _rocketSpawnParent;

    private void Awake()
    {
        _rocketSpawnParent = GameObject.Find("ROCKET OBJECTS").transform;
    }

    public void Spawn()
    {
        int seed = Random.Range(0, _rocketPrefabs.Length);
        GameObject rocket = Instantiate(_rocketPrefabs[seed], this.transform.position, Quaternion.identity, _rocketSpawnParent);
        rocket.transform.LookAt(this.transform.position + Vector3.down); //Have the rockets all point towards the ground when spawning in
    }
}
