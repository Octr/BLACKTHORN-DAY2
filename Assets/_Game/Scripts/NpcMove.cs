using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float speed = 5f;

    private int currentTargetIndex = 0;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to NpcMove script!");
            enabled = false; // Disable the script if there are no spawn points to move between.
            return;
        }

        SetNextTarget();
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (spawnPoints.Length == 0)
            return;

        Vector3 targetDirection = spawnPoints[currentTargetIndex].position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, spawnPoints[currentTargetIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spawnPoints[currentTargetIndex].position) < 0.01f)
        {
            // If the NPC reached the current target, set the next target in the array.
            SetNextTarget();
        }

        // Rotate the NPC to face its next spawn point.
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    private void SetNextTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % spawnPoints.Length;
    }
}
