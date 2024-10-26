using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageButtons : MonoBehaviour
{
    public Button leftBtn; // El botón que se usará
    public Button rightBtn; // El botón que se usará

    public Camera mainCamera; // La cámara principal

    private Image leftBtnImage; // Componente Image del botón izquierdo
    private Image rightBtnImage; // Componente Image del botón derecho

    // Tolerancia para comparar la rotación
    public float tolerance = 0.01f;

    void Start()
    {
        // Obtener el componente Image de cada botón
        leftBtnImage = leftBtn.GetComponent<Image>();
        rightBtnImage = rightBtn.GetComponent<Image>();
    }

    void Update()
    {
        float rotationY = mainCamera.transform.rotation.y;

        if (rotationY < -tolerance) // Verificar si la rotación es menor que -tolerancia
        {
            leftBtn.interactable = false; // Deshabilitar el botón
            leftBtnImage.color = new Color(leftBtnImage.color.r, leftBtnImage.color.g, leftBtnImage.color.b, 0); // Hacer el sprite invisible
        }
        else if (rotationY > tolerance) // Verificar si la rotación es mayor que tolerancia
        {
            rightBtn.interactable = false; // Deshabilitar el botón
            rightBtnImage.color = new Color(rightBtnImage.color.r, rightBtnImage.color.g, rightBtnImage.color.b, 0); // Hacer el sprite invisible
        }
        else // Si está dentro del rango tolerable de 0
        {
            leftBtn.interactable = true; // Habilitar el botón
            leftBtnImage.color = new Color(leftBtnImage.color.r, leftBtnImage.color.g, leftBtnImage.color.b, 1); // Hacer el sprite visible
            rightBtn.interactable = true; // Habilitar el botón
            rightBtnImage.color = new Color(rightBtnImage.color.r, rightBtnImage.color.g, rightBtnImage.color.b, 1); // Hacer el sprite visible
        }
    }
}
