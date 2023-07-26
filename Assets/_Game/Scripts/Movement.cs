using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("The speed of the player")]
    [SerializeField] private float _moveSpeed = 10f;
    [Tooltip("The maximum movement the player can make away from world center in both the negative and position direction. X = horizontal movement, y = forward movement. Used when using the keyboard controls")]
    [SerializeField] private Vector2 _movementRestriction;

    private InputReader _input;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(_input.HasMouseControl)
        {
            MoveWithMouse();
        }
        else
        {
            MoveWithKeyboard();
        }

    }

    private void MoveWithMouse()
    {
        transform.position = _input.MouseWorldPosition;
    }

    private void MoveWithKeyboard()
    {
        //Move the player based on move speed and move input
        transform.Translate(_moveSpeed * Time.deltaTime * _input.MoveInput);

        //Restrict horizontal movment
        if(transform.position.x > _movementRestriction.x)
        {
            transform.position = new Vector3(_movementRestriction.x, transform.position.y, transform.position.z);
        }
        else if( transform.position.x < -_movementRestriction.x)
        {
            transform.position = new Vector3(-_movementRestriction.x, transform.position.y, transform.position.z);
        }

        //Restrict forward movement
        if (transform.position.z > _movementRestriction.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _movementRestriction.y);
        }
        else if (transform.position.z < -_movementRestriction.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_movementRestriction.y);
        }
    }


    private void OnDrawGizmos()
    {
        //Draw boundary of the player's maximum movement
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_movementRestriction.x * 2, 3f, _movementRestriction.y * 2)); //Times by 2 to take into account the positive and negative extents of the boundary
    }
}
