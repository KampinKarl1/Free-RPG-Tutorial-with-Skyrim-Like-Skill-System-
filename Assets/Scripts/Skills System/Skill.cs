using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField] private string skillName = "New Skill Tree";
    [SerializeField] private string description = "Does stuff";
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private float xpToNextLevel = 1;
    [SerializeField, Tooltip("The amount xpToNextLevel increases (times level) when gaining a level")] 
    private float xpScalePerLevel = 50;
    [SerializeField] private float currentXP = 0;
    

    public string Name => skillName;
    public string Description => description;

    public int CurrentLevel => currentLevel;
    public float Progress => currentXP / xpToNextLevel;

    public delegate void OnSkillIncrease(int level);
    public delegate void OnChangeXP(float progress);

    public OnSkillIncrease onSkillIncrease;
    public OnChangeXP onChangeXP;

    public void IncreaseSkill(float xp) 
    {
        Debug.Log("Earned " + xp.ToString() +" xp");
        currentXP += xp;

        if (currentXP >= xpToNextLevel) 
        {
            do
            {
                currentLevel++;

                currentXP -= xpToNextLevel;

                xpToNextLevel += xpScalePerLevel * currentLevel;

                onSkillIncrease?.Invoke(currentLevel);
            } 
            while (currentXP >= xpToNextLevel);
        }

        onChangeXP?.Invoke(Progress);
    }

    private void OnValidate()
    {
        if (xpToNextLevel < 1)
        {
            Debug.LogError("XP to next level being less than one will cause stack overflow");
            xpToNextLevel = 1;
        }
    }
}
