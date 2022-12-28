using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerHitFlash : MonoBehaviour
{
    readonly List<Color> originalColors = new List<Color>();
 
    private int originalColorIndex;
 
    public bool useEmission;
    public AudioSource crashAlarm;
 
    private void Start()
    {
        UpdateMeshCount();
    }

    private void UpdateMeshCount()
    {
        // Find all the children of the GameObject with MeshRenderers

        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();

        // Cycle through each child object found with a MeshRenderer

        foreach (MeshRenderer rend in children)
        {
            if (rend != null)
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
    }

    public void Flash()
    {
        crashAlarm.Play();
        if (this.isActiveAndEnabled == true)
        {
            UpdateMeshCount();
            StartCoroutine("HitFlash");
            StartCoroutine("HitFlash");
        }
    }
 
    public IEnumerator HitFlash()
    {
        // Flash color
 
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
 
        foreach (MeshRenderer rend in children)
        {
            if (rend != null)
            {
                foreach (Material mat in rend.materials)
                {

                    if (useEmission)
                        mat.SetColor("_EmissionColor", Color.white);

                    else
                        mat.SetColor("_BaseColor", Color.red);

                }
            }
        }
 
        yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
 
        foreach (MeshRenderer rend in children)
        {
            if (rend != null)
            {
                rend.enabled = false;
            }
        }
        yield return new WaitForSeconds(Random.Range(0.04f, 0.08f));

        // Restore default colors or emission
 
        foreach (MeshRenderer rend in children)
        {
            if (rend != null)
            {
                if (rend != null)
                {
                    rend.enabled = true;
                }

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
        for (var i = this.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(this.transform.GetChild(i).gameObject);
        }
        //Destroy(gameObject);
    }
}