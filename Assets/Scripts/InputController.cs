using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //--------------------------
    [SerializeField] private SkyboxChanger skyboxChanger = null;
    [SerializeField] private SkillsSelectionControl skillsControl = null;
    [SerializeField] private CombatControl combatControl = null;
    [SerializeField] private RPGCharacterAnimsFREE.RPGCharacterWeaponControllerFREE weapons = null;
    //--------------------------

    //--------------------------
    [SerializeField] KeyCode changeSkillPanelStatus = KeyCode.Tab;
    [SerializeField] KeyCode scrollLeft_Skill = KeyCode.A;
    [SerializeField] KeyCode scrollRight_Skill = KeyCode.D;
    [SerializeField] private KeyCode skyboxKey = KeyCode.Delete;
    [SerializeField] private KeyCode swapWeapon = KeyCode.Y;
    //--------------------------

    void Update()
    {
        //Targetting
        if (Input.GetAxisRaw("Target") != 0)
            combatControl.LookForTarget();

        //Skills

        if (Input.GetKey(scrollRight_Skill) && skillsControl && skillsControl.SkillPanelOpen)
            skillsControl.SelectElementRight();

        if (Input.GetKey(scrollLeft_Skill) && skillsControl && skillsControl.SkillPanelOpen)
            skillsControl.SelectElementLeft();

        if (Input.GetKey(changeSkillPanelStatus) && skillsControl && skillsControl.SkillPanelOpen)
            skillsControl.CloseSkills();
        if (Input.GetKey(changeSkillPanelStatus) && skillsControl && !skillsControl.SkillPanelOpen)
            skillsControl.OpenSkills();

        //Skybox
        if (Input.GetKeyDown(skyboxKey) && skyboxChanger)
            skyboxChanger.ChangeSkybox();

        //Weapons
        if (Input.GetKeyDown(swapWeapon) && weapons) 
        {
            weapons.SwitchWeaponTwoHand(1);
        }
    }
}