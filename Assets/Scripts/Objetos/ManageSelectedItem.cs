using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa el espacio de nombres para UI

public class ManageSelectedItem : MonoBehaviour
{
    public Image image; // La imagen de UI a actualizar
    public GameObject[] objectsArray; // Array de objetos que se pueden seleccionar
    private GameObject selectedObject; // Variable para almacenar el objeto seleccionado
    public GameObject puerta; // Variable para almacenar la puerta
    private Sprite originalSprite; // Variable para almacenar el sprite original de la imagen

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        // Guarda el sprite original al iniciar el juego
        if (image != null)
        {
            originalSprite = image.sprite;
        }
        else
        {
            Debug.LogError("La imagen no está asignada en el inspector.");
        }
    }

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
                // Verifica si se clickeó en la puerta
                if (hit.collider.gameObject == puerta)
                {
                    // Restaura el sprite original en la imagen de UI
                    RestoreOriginalSprite();

                    // Desactiva el objeto seleccionado si existe
                    if (selectedObject != null)
                    {
                        selectedObject.SetActive(false); // Desactiva el objeto seleccionado
                        selectedObject = null; // Resetea la variable
                    }
                    return; // Sale del método Update
                }

                // Verifica si el objeto clicado está en el array
                foreach (GameObject obj in objectsArray)
                {
                    if (obj != null && hit.collider.gameObject == obj)
                    {
                        // Obtiene el componente IdentifiableObject
                        IdentifiableObject identifiableObject = obj.GetComponent<IdentifiableObject>();

                        if (identifiableObject != null)
                        {
                            // Muestra el ID del objeto clicado en la consola
                            Debug.Log("ID del objeto clicado: " + identifiableObject.id);
                            
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

                            // Almacena el objeto seleccionado
                            selectedObject = obj;
                        }
                        else
                        {
                            Debug.LogWarning("El objeto " + obj.name + " no tiene un componente IdentifiableObject.");
                        }
                        break;
                    }
                }
            }
        }
    }

    // Método para obtener el objeto seleccionado
    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    // Método para restaurar el sprite original
    public void RestoreOriginalSprite()
    {
        if (image != null && originalSprite != null)
        {
            image.sprite = originalSprite; // Restaura el sprite original
        }
        else
        {
            Debug.LogError("No se puede restaurar el sprite. Asegúrate de que la imagen y el sprite original estén asignados.");
        }
    }
}
