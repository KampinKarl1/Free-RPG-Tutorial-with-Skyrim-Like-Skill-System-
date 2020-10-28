using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //--------------------------
    [SerializeField] private SkyboxChanger skyboxChanger = null;
    [SerializeField] private SkillsSelectionControl skillsControl = null;
    //--------------------------

    //--------------------------
    [SerializeField] KeyCode scrollLeft = KeyCode.A;
    [SerializeField] KeyCode scrollRight = KeyCode.D;
    [SerializeField] private KeyCode skyboxKey = KeyCode.Delete;
    //--------------------------

    void Start()
    {
        
    }

    void Update()
    {
        //Skills
        if (Input.GetKey(scrollRight) && skillsControl)
            skillsControl.SelectElementRight();
        if (Input.GetKey(scrollLeft) && skillsControl)
            skillsControl.SelectElementLeft();
        //Skybox
        if (Input.GetKeyDown(skyboxKey) && skyboxChanger)
            skyboxChanger.ChangeSkybox();
    }
}
