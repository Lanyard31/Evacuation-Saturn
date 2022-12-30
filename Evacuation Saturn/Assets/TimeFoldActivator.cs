using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFoldActivator : MonoBehaviour
{

     [SerializeField] GameObject MasterTimeline;

    private void OnEnable() {
        MasterTimeline.GetComponent<TimeFold>().enabled = true;
        MasterTimeline.GetComponent<TimeFold>().FoldOnline = true;
        Debug.Log("Time Fold Active.");
    }

}
