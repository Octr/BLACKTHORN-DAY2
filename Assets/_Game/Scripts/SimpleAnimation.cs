using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: Never got around to fleshing out this class. Was hoping to make the decal projector on the player rotate slowly to give it more character.
//Whoever works on this project, please make it work

//NOTE: I got you dw!


public class SimpleAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("Axis to rotate around (e.g., Vector3.up for Y-axis).")]
    private Vector3 rotationAxis = Vector3.up;

    [SerializeField, Tooltip("Speed of rotation.")]
    private float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the specified axis at the given speed
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
