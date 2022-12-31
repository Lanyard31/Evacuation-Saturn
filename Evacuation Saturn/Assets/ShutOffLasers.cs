using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutOffLasers : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject TimeFold;

    private void OnEnable()
    {
        var playercontrols = Player.GetComponent<PlayerControls>();
        var timefoldscript = TimeFold.GetComponent<TimeFold>();
        playercontrols.finaleDisabled = true;
        timefoldscript.finaleDisabled = true;


    }
}
