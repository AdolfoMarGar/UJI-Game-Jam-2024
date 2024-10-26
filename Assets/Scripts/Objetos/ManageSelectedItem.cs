using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa el espacio de nombres para UI

public class ManageSelectedItem : MonoBehaviour
{
    public Image image; // La imagen de UI a actualizar
    public GameObject[] objectsArray; // Array de objetos que se pueden seleccionar

    // Update se llama una vez por frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si se hace clic izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Lanza un rayo desde la cámara hacia donde se hizo clic
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica si el objeto clicado está en el array
                foreach (GameObject obj in objectsArray)
                {
                    if (obj != null && hit.collider.gameObject == obj)
                    {
                        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

                        if (spriteRenderer != null)
                        {
                            // Asigna el sprite del objeto clicado como Source Image en la imagen de UI
                            image.sprite = spriteRenderer.sprite; // Asigna el sprite directamente
                        }
                        else
                        {
                            Debug.LogWarning("El objeto " + obj.name + " no tiene un SpriteRenderer.");
                        }
                        break;
                    }
                }
            }
        }
    }
}
