using UnityEngine;
using System.Collections.Generic;


public class MFPCustomizationDef
{
    /// <summary>
    /// model that is only visible to player
    /// </summary>
    public SkinnedMeshRenderer[] clientRenderers;
    /// <summary>
    /// model that is seen to anyone else
    /// </summary>
    public SkinnedMeshRenderer[] ghostRenderers;


    public void DisableAll()
    {
        if (clientRenderers != null)
            foreach (SkinnedMeshRenderer renderer in clientRenderers)
                renderer.enabled = false;

        if (ghostRenderers != null)
            foreach (SkinnedMeshRenderer renderer in ghostRenderers)
                renderer.enabled = false;
    }

    public void DisableGhostRenderer()
    { 
        if (ghostRenderers != null)
            foreach (SkinnedMeshRenderer renderer in ghostRenderers)
                renderer.enabled = false;
    }

    public void DisableClientRenderer()
    {
        if (clientRenderers != null)
            foreach (SkinnedMeshRenderer renderer in clientRenderers)
                renderer.enabled = false;
    }

    public void EnableAll()
    {
        EnableClientRenderer();
        EnableGhostRenderer();
    }

    public void EnableClientRenderer()
    {
        if (clientRenderers != null)
            foreach (SkinnedMeshRenderer renderer in clientRenderers)
            {
                renderer.gameObject.SetActive(true);
                renderer.enabled = true;
            }
    }

    public void EnableGhostRenderer()
    {
        if (ghostRenderers != null)
            foreach (SkinnedMeshRenderer renderer in ghostRenderers)
            {
                renderer.gameObject.SetActive(true);
                renderer.enabled = true;
            }
    }

    public void EnableSituational(bool owner)
    {
        DisableAll();

        if (owner)
            EnableClientRenderer();
        else
            EnableGhostRenderer();
    }
}
