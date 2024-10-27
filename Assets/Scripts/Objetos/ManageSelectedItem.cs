using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSelectedItem : MonoBehaviour
{
    public Image image; // La imagen de UI a actualizar
    public GameObject[] objectsArray; // Array de objetos que se pueden seleccionar
    public GameObject[] characterArray; // Array de personajes que se pueden seleccionar
    private GameObject selectedObject; // Variable para almacenar el objeto seleccionado
    public GameObject puerta; // Variable para almacenar la puerta
    private Sprite originalSprite; // Variable para almacenar el sprite original de la imagen
    private int reputacion = 0; // Variable para almacenar la reputación, inicializada a 0

    // Nuevas variables
    public GameObject spriteRendererObject; // Objeto con SpriteRenderer que se actualizará
    public Sprite originalSpriteObject; // Sprite "original" a usar al inicio
    public Sprite alternateSprite; // Sprite alternativo que se utilizará si el objeto seleccionado no es el primero

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        InitializeOriginalSprite();
        SetInitialSprite();
    }

    // Update se llama una vez por frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si se hace clic izquierdo
        {
            HandleMouseClick();
        }
    }

    // Método para inicializar el sprite original
    private void InitializeOriginalSprite()
    {
        if (image != null)
        {
            originalSprite = image.sprite;
        }
        else
        {
            Debug.LogError("La imagen no está asignada en el inspector.");
        }
    }

    // Método para establecer el sprite inicial del objeto con SpriteRenderer
    private void SetInitialSprite()
    {
        if (spriteRendererObject != null && originalSpriteObject != null)
        {
            SpriteRenderer renderer = spriteRendererObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = originalSpriteObject; // Asigna el sprite original
            }
            else
            {
                Debug.LogWarning("El objeto con SpriteRenderer no tiene un componente SpriteRenderer.");
            }
        }
        else
        {
            Debug.LogWarning("spriteRendererObject o originalSpriteObject no están asignados en el inspector.");
        }
    }

    // Método para manejar el clic del ratón
    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Lanza un rayo desde la cámara hacia donde se hizo clic
        if (Physics.Raycast(ray, out hit))
        {
            // Verifica si se clickeó en la puerta
            if (hit.collider.gameObject == puerta)
            {
                HandleDoorClick();
            }
            else
            {
                HandleObjectClick(hit);
            }
        }
    }

    // Método para manejar el clic en la puerta
    private void HandleDoorClick()
    {
        RestoreOriginalSprite();

        // Desactiva el objeto seleccionado si existe
        if (selectedObject != null)
        {
            selectedObject.SetActive(false); // Desactiva el objeto seleccionado
            selectedObject = null; // Resetea la variable
        }

        // Cambia el sprite del spriteRendererObject al sprite original
        SetInitialSprite();
    }

    // Método para manejar el clic en un objeto seleccionable
    private void HandleObjectClick(RaycastHit hit)
    {
        foreach (GameObject obj in objectsArray)
        {
            if (obj != null && hit.collider.gameObject == obj)
            {
                ProcessSelectedObject(obj);
                break;
            }
        }
    }

    // Método para procesar el objeto seleccionado
    private void ProcessSelectedObject(GameObject obj)
    {
        IdentifiableObject identifiableObject = obj.GetComponent<IdentifiableObject>();

        if (identifiableObject != null)
        {
            Debug.Log("ID del objeto clicado: " + identifiableObject.id);
            UpdateUIImage(obj);
            selectedObject = obj; // Almacena el objeto seleccionado

            // Comprueba si el objeto seleccionado es el primero en el array
            if (identifiableObject.id == 0)
            {
                SetOriginalSprite();
            }
            else
            {
                SetAlternateSprite();
            }
        }
        else
        {
            Debug.LogWarning("El objeto " + obj.name + " no tiene un componente IdentifiableObject.");
        }
    }

    // Método para actualizar la imagen de UI con el sprite del objeto seleccionado
    private void UpdateUIImage(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            image.sprite = spriteRenderer.sprite; // Asigna el sprite directamente
        }
        else
        {
            Debug.LogWarning("El objeto " + obj.name + " no tiene un SpriteRenderer.");
        }
    }

    // Método para obtener el objeto seleccionado
    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    // Método para restaurar el sprite original de la UI y del objeto spriteRendererObject
    public void RestoreOriginalSprite()
    {
        if (image != null && originalSprite != null)
        {
            image.sprite = originalSprite; // Restaura el sprite original de la UI
        }
        else
        {
            Debug.LogError("No se puede restaurar el sprite. Asegúrate de que la imagen y el sprite original estén asignados.");
        }

        // Restaurar el sprite original del spriteRendererObject
        SetInitialSprite();
    }

    // Método para asignar el sprite original al objeto con SpriteRenderer
    private void SetOriginalSprite()
    {
        if (spriteRendererObject != null && originalSpriteObject != null)
        {
            SpriteRenderer renderer = spriteRendererObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = originalSpriteObject; // Asigna el sprite original
            }
            else
            {
                Debug.LogWarning("El objeto con SpriteRenderer no tiene un componente SpriteRenderer.");
            }
        }
    }

    // Método para asignar el sprite alternativo al objeto con SpriteRenderer
    private void SetAlternateSprite()
    {
        if (spriteRendererObject != null && alternateSprite != null)
        {
            SpriteRenderer renderer = spriteRendererObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = alternateSprite; // Cambia al sprite alternativo
            }
            else
            {
                Debug.LogWarning("El objeto con SpriteRenderer no tiene un componente SpriteRenderer.");
            }
        }
    }
}
