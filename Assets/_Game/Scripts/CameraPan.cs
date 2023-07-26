using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the panning of the Virtural Cameras. Needs a CameraPanHandler in the Scene to work
/// </summary>
public class CameraPan : MonoBehaviour
{    
    private InputReader _input;
    private CameraPanHandler _cameraPanHandler;

    private void Awake()
    {
        _input = GetComponent<InputReader>();

        _cameraPanHandler = FindObjectOfType<CameraPanHandler>();
        if(_cameraPanHandler == null)
        {
            Debug.LogError("No camera pan handler in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.PanLeftInput)
        {
            _cameraPanHandler.DecreaseIndex();
        }

        if(_input.PanRightInput)
        {
            _cameraPanHandler.IncreaseIndex();
        }
    }
}
