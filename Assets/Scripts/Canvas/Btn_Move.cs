using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esta línea para poder trabajar con UI

public class Btn_Move : MonoBehaviour
{
    public Button myButton; // El botón que se usará
    public Camera mainCamera; // La cámara principal
    [SerializeField]
    private float rotationAmount = -50f; // Rotación en grados al hacer clic

    [SerializeField] private float r_time = 1f; // Tiempo en segundos para la rotación

    void Start()
    {
        // Asegúrate de que el botón esté asignado en el Inspector
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick); // Añadir listener al evento OnClick
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }
    }

    void OnButtonClick()
    {
        if (mainCamera != null)
        {
            StartCoroutine(RotateCamera()); // Inicia la corrutina para rotar la cámara
        }
        else
        {
            Debug.LogError("La cámara principal no está asignada.");
        }
    }

    private IEnumerator RotateCamera()
    {
        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, rotationAmount, 0);
        float elapsedTime = 0f;

        while (elapsedTime < r_time)
        {
            // Interpolar la rotación
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / r_time);
            elapsedTime += Time.deltaTime; // Incrementar el tiempo transcurrido
            yield return null; // Esperar el siguiente frame
        }

        // Asegúrate de establecer la rotación final
        mainCamera.transform.rotation = endRotation;
        Debug.Log("Cámara rotada: " + rotationAmount + " grados.");
    }

    // Update se puede usar si necesitas realizar operaciones por frame
    void Update()
    {
        // Aquí puedes agregar lógica adicional si es necesario
        
    }
}
