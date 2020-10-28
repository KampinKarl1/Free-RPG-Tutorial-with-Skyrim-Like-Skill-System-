using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    private Material currentSkybox = null;
    [SerializeField] Material levelupSkybox = null;
    [SerializeField] Material normalSkybox = null;

    public void ChangeSkybox() 
    {
        currentSkybox = currentSkybox == normalSkybox ? levelupSkybox : normalSkybox;

        RenderSettings.skybox = currentSkybox;
    }
}
