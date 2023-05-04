using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    bool zoomedIn = false;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedIn == false)
            {
                zoomedIn = true;
                fpsCamera.fieldOfView = zoomedInFOV;
            } else {
                zoomedIn = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
            }
        }
    }
}
