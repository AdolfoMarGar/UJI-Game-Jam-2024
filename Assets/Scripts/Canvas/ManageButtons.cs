using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esta línea para poder trabajar con UI

public class ManageButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public Button leftBtn; // El botón que se usará
    public Button rightBtn; // El botón que se usará

    public Camera mainCamera; // La cámara principal



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.rotation.y < 0)
        {
            leftBtn.enabled = false;
            rightBtn.enabled = true;
        }
        else
        {
            leftBtn.enabled = true;
            rightBtn.enabled = false;

        }
    }
}
