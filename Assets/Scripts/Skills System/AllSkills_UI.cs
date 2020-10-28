using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSkills_UI : MonoBehaviour
{
    [SerializeField] private SkillUser player = null;
    [SerializeField] private SkillsSelectionControl skillsControl = null;

    [SerializeField] private GameObject skillUIPrefab = null;

    [SerializeField] private Transform contentParent = null;

    private void Start()
    {
        InitializeSkills();
    }

    private void InitializeSkills() 
    {
        int numSkills = player.Skills.Count;
        GameObject[] skillsObjs = new GameObject[numSkills];

        for (int i = 0; i < numSkills; ++i) 
        {
            GameObject skillObj = Instantiate(skillUIPrefab, contentParent);
            skillsObjs[i] = skillObj;

            skillObj.GetComponent<Skill_UI>().InitializeSkillTreeUI(player.Skills[i]);
        }

        skillsControl.InitializeSkillInput(skillsObjs);
        
    }
}
