using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
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

    public GameObject fondoPersonajes;
    public Sprite originalSpriteObject; // Sprite "original" a usar al inicio
    public Sprite alternateSprite; // Sprite alternativo que se utilizará si el objeto seleccionado no es el primero

    private IdentifiableObject identifiableObject; // Referencia global a IdentifiableObject

    // Variables para el manejo de los objetos "sofa" y "tele"
    public GameObject sofa; // Referencia al objeto sofa
    public GameObject tele; // Referencia al objeto tele

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        InitializeOriginalSprite();
        SetInitialSprite();

        // Muestra el sprite del sofá al inicio
        ShowSofa();
        //StartCoroutine(ExecuteCharacterMethods());

    }

    // Método para manejar el clic en el sofá
    public void OnSofaClick()
    {
        if (sofa != null)
        {
            sofa.SetActive(false); // Oculta el sofá
            ShowTele(); // Muestra el tele
        }
    }

    // Método para mostrar el sofá y ocultar el tele
    private void ShowSofa()
    {
        if (sofa != null)
        {
            sofa.SetActive(true); // Activa el sofá
            tele.SetActive(false); // Asegúrate de que el tele esté oculto
        }
        else
        {
            Debug.LogError("El objeto sofá no está asignado correctamente.");
        }
    }

    // Método para mostrar el tele y ocultar el sofá
    private void ShowTele()
    {
        if (tele != null)
        {
            tele.SetActive(true); // Activa el tele
            sofa.SetActive(false); // Asegúrate de que el sofá esté oculto
        }
        else
        {
            Debug.LogError("El objeto tele no está asignado correctamente.");
        }
    }

    // Método para manejar el clic en el tele
    public void OnTeleClick()
    {
        if (tele != null)
        {
            tele.SetActive(false); // Oculta el tele
            // Comienza la corutina para ejecutar los métodos de todos los personajes en characterArray
            StartCoroutine(ExecuteCharacterMethods());
        }
    }
    private IEnumerator ExecuteCharacterLogic(GameObject character, IdentifiableCharacter characterData, int itemValue)
    {
        // Activa el fondo de personajes
        fondoPersonajes.SetActive(true);

        // Obtén el SpriteRenderer del personaje
        SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();

        // Verifica que el SpriteRenderer no sea nulo antes de activarlo
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // Activa el SpriteRenderer
            Debug.Log("Iniciando lógica para: " + character.name);

            // Simula alguna lógica con el personaje (puedes reemplazarlo con tu propia lógica)
            // Aquí puedes usar corutinas adicionales, llamadas a métodos, etc.
            yield return new WaitForSeconds(4f); // Simula un tiempo de espera para la lógica

            // Puedes usar characterData o itemValue en tu lógica aquí
            // Por ejemplo, procesar valores de reputación, animaciones, etc.
            Debug.Log("Lógica completada para: " + character.name);

            spriteRenderer.enabled = false; // Desactiva el SpriteRenderer
        }
        else
        {
            Debug.LogWarning("El SpriteRenderer no se encontró en el objeto: " + character.name);
        }

        // Desactiva el fondo de personajes
        fondoPersonajes.SetActive(false);
    }


    // Corutina para ejecutar métodos de todos los personajes en characterArray con pausas
    private IEnumerator ExecuteCharacterMethods()
    {
        // Recorre los índices desde 0 hasta 6, asegurándose de no exceder el tamaño del array
        for (int n = 0; n < characterArray.Length && n <= 6; n++)
        {
            GameObject character = characterArray[n];
            Debug.Log("Personaje: " + character.name);
            if (character != null)
            {
                // Llama a los métodos
                character.SendMessage("PlayCallSound", SendMessageOptions.DontRequireReceiver);
                yield return new WaitForSeconds(3f); // Espera por el sonido de llamada

                character.SendMessage("PlayOwnSound", SendMessageOptions.DontRequireReceiver);

                // Accede al componente IdentifiableCharacter para obtener el array values
                IdentifiableCharacter characterData = character.GetComponent<IdentifiableCharacter>();
                // Espera hasta que se interactúe con la puerta
                yield return StartCoroutine(WaitForDoorInteraction());
                yield return StartCoroutine(ExecuteCharacterLogic(character, characterData, characterData.values[identifiableObject.id - 1]));

                if (characterData != null && identifiableObject != null)
                {
                    reputacion += characterData.values[identifiableObject.id - 1];
                }
                else
                {
                    Debug.LogWarning("IdentifiableCharacter o IdentifiableObject no está asignado.");
                }

            }
            else
            {
                Debug.LogWarning("El objeto en characterArray[" + n + "] es nulo.");
            }

            Debug.Log("Reputacion: " + reputacion);

            // Espera adicional de 2 segundos
            yield return new WaitForSeconds(2f); // Pausa adicional entre personajes
        }
    }

    // Nueva corutina para esperar interacción con la puerta
    private IEnumerator WaitForDoorInteraction()
    {
        bool interacted = false;

        // Mientras no se haya interactuado con la puerta, espera
        while (!interacted)
        {
            // Verifica si se hace clic izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Lanza un rayo desde la cámara hacia donde se hizo clic
                if (Physics.Raycast(ray, out hit))
                {
                    // Verifica si se clickeó en la puerta
                    if (hit.collider.gameObject == puerta)
                    {
                        interacted = true; // Marca que se ha interactuado
                    }
                }
            }
            yield return null; // Espera un frame antes de volver a verificar
        }
    }

    // Update se llama una vez por frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si se hace clic izquierdo
        {
            HandleMouseClick();
        }

        CheckDoorState();
    }

    // Método para comprobar el estado de la puerta
    private void CheckDoorState()
    {
        // Si identifiableObject es null o su id es 0, desactiva la puerta, de lo contrario, actívala
        if (identifiableObject == null || identifiableObject.id == 0)
        {
            puerta.SetActive(false);
        }
        else
        {
            puerta.SetActive(true);
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
            // Muestra el nombre del objeto que ha sido tocado
            Debug.Log("Tocado: " + hit.collider.gameObject.name);

            // Verifica si se clickeó en la puerta
            if (hit.collider.gameObject == puerta)
            {
                HandleDoorClick();
            }
            else if (hit.collider.gameObject == sofa)
            {
                OnSofaClick(); // Maneja el clic en el sofá
            }
            else if (hit.collider.gameObject == tele)
            {
                OnTeleClick(); // Maneja el clic en el tele
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
        identifiableObject = obj.GetComponent<IdentifiableObject>(); // Asignar globalmente

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
