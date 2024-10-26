using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Objeto Nuevo", menuName = "Objeto", order = 0)]
public class ObjetosScriptables : ScriptableObject
{
    [SerializeField] public new string nombre;
    [SerializeField] public string descripcion;
    [SerializeField] public Sprite artwork;
    [SerializeField] public int costeReputacion;

}

