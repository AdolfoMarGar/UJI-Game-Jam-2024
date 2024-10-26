using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtonBehavior : MonoBehaviour
{
    [SerializeField] private GameObject controlesOverlay;

    public void OnCLickShowOverlay()
    {
        controlesOverlay.SetActive(true);
    }

    public void OnClickHideOverlay()
    {
        controlesOverlay.SetActive(false);
    }
}
