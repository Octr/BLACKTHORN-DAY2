using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * transform.forward, Space.World);
    }
}
