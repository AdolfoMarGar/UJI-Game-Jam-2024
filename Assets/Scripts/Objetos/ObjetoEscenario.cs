using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoEscenario : MonoBehaviour
{
    public ObjetosScriptables objeto;
    [SerializeField] private SpriteRenderer artworkImage;

    void Start()
    {
        artworkImage.sprite = objeto.artwork;
    }
}
