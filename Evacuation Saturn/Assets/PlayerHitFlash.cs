using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerHitFlash : MonoBehaviour
{
    readonly List<Color> originalColors = new List<Color>();
 
    private int originalColorIndex;
 
    public bool useEmission;
 
    private void Start()
    {
        // Find all the children of the GameObject with MeshRenderers
 
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
 
        // Cycle through each child object found with a MeshRenderer
 
        foreach (MeshRenderer rend in children)
        {
            // And for each child, cycle through each material
 
            foreach (Material mat in rend.materials)
            {
                if (useEmission)
                {
                    // Enable Keyword EMISSION for each material
 
                    mat.EnableKeyword("_EMISSION");
                }
 
                else
                {
                    // Store original colors
 
                    originalColors.Add(mat.color);
                }
            }
        }
    }
 
    public void Flash()
    {
        StartCoroutine("HitFlash");
    }
 
    public IEnumerator HitFlash()
    {
        // Flash color
 
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
 
        foreach (MeshRenderer rend in children)
        {
            foreach (Material mat in rend.materials)
            {
                if (useEmission)
                    mat.SetColor("_EmissionColor", Color.white);
 
                else
                    mat.SetColor("_BaseColor", Color.red);
                    Debug.Log("Red");
            }
        }
 
        yield return new WaitForSeconds(0.001f);
 
        // Restore default colors or emission
 
        foreach (MeshRenderer rend in children)
        {
            foreach (Material mat in rend.materials)
            {
                if (useEmission)
                    mat.SetColor("_EmissionColor", Color.black);
 
                else
                {
                    mat.SetColor("_BaseColor", originalColors[originalColorIndex]);
 
                    // Increment originalColorIndex by 1
 
                    originalColorIndex += 1;
                }
            }
        }
 
        if (useEmission)
            StopCoroutine("HitFlash");
 
        else
        {
            // Reset originalColorIndex
 
            originalColorIndex = 0;
 
            StopCoroutine("HitFlash");
        }
    }

    public void boom()
    {
        Invoke("boom2", Random.Range(0f, 0.1f));
    }

    private void boom2()
    {
        Destroy(gameObject);
    }
}