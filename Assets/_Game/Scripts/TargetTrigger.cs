using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Rocket")) return;
        TargetSpawner.Instance.targetKillCount++;
        TargetSpawner.Instance.roomCounter++;
        Destroy(gameObject);
    }
}
