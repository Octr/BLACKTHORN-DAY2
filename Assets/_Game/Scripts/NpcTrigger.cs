using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Rocket")) return;

        if (TargetSpawner.Instance.targetKillCount == 0)
        {
            TargetSpawner.Instance.targetKillCount = 0;
        }
        else
        {
            TargetSpawner.Instance.targetKillCount--;
        }


        

        if (NpcSpawner.Instance.currentNpcs == 0)
        {
            NpcSpawner.Instance.currentNpcs = 0;
        }
        else
        {
            NpcSpawner.Instance.currentNpcs--;
        }
        
        animator.SetTrigger("Die");
        StartCoroutine(Destroy());
        GetComponent<NpcMove>().enabled = false;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
