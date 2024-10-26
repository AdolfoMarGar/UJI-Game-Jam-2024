using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject click_i;
    [SerializeField] private GameObject click_d;

    

    // Movimiento camara
    [SerializeField] private Vector3 cameraRotation;
    [SerializeField] private float r_time;
    [SerializeField] private bool rotation = false;

    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = Quaternion.Euler(cameraRotation);
    }

    void Update() 
    {
        if (rotation) 
        {
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetRotation, Time.deltaTime / r_time);

            if (Quaternion.Angle(cam.transform.rotation, targetRotation) < 0.01f) 
            {
                cam.transform.rotation = targetRotation;
                rotation = false; 
            }
        }
    }

    void OnMouseDown()
    {
        rotation = true;
    }
}
