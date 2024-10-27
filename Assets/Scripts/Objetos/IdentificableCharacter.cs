using UnityEngine;

[System.Serializable] // Permite que el array sea visible y editable en el Inspector
public class IdentifiableCharacter : MonoBehaviour
{
    public int id; // ID del objeto
    public int[] values; // Hazlo público para acceder desde otros scripts
    public AudioClip callSound; // Sonido para llamar a la puerta
    public AudioClip ownSound; // Sonido propio del objeto

    private AudioSource audioSource; // Componente para reproducir sonidos

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        // Añade un componente AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Método para reproducir el sonido de llamar a la puerta
    public void PlayCallSound()
    {
        if (callSound != null)
        {
            audioSource.PlayOneShot(callSound);
        }
        else
        {
            Debug.LogWarning("El sonido de llamar a la puerta no está asignado.");
        }
    }

    // Método para reproducir el sonido propio
    public void PlayOwnSound()
    {
        if (ownSound != null)
        {
            audioSource.PlayOneShot(ownSound);
        }
        else
        {
            Debug.LogWarning("El sonido propio no está asignado.");
        }
    }


}
