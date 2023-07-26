using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPanHandler : MonoBehaviour
{
    private CinemachineVirtualCamera[] _virtualCameras;
    private int _index;

    private void Awake()
    {
        _virtualCameras = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActiveCamera(_index);
    }

    public void IncreaseIndex()
    {
        _index++;
        if(_index > _virtualCameras.Length - 1)
        {
            _index = 0;
        }
        SetActiveCamera(_index);
    }

    public void DecreaseIndex()
    {
        _index--;
        if (_index < 0)
        {
            _index = _virtualCameras.Length - 1;
        }
        SetActiveCamera(_index);
    }

    public void SetActiveCamera(int cameraIndex)
    {
        for (int i = 0; i < _virtualCameras.Length; i++)
        {
            if(i == cameraIndex)
            {
                _virtualCameras[i].enabled = true;
            }
            else
            {
                _virtualCameras[i].enabled = false;
            }
        }
    }
}
