using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour
{
    [SerializeField] private List<Skill> skills = new List<Skill>();
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int perks = 0;
    [SerializeField] private float xpToNextLevel = 0;
    [SerializeField] private float progressTowardNextLevel = 0;

    public List<Skill> Skills => skills;
    public int CurrentLevel => currentLevel;
    public float ProgressToNextLevel => progressTowardNextLevel / xpToNextLevel;

    public delegate void OnLevelUp(int level);
    public delegate void OnPerkIncrease(int perks);
    public delegate void OnChangeXP(float progress);
    public delegate void OnPlayerChange(int lvl, int prks, float progress);

    public OnLevelUp onLevelUp;
    public OnPerkIncrease onPerkIncrease;
    public OnChangeXP onChangeXP;
    public OnPlayerChange onPlayerChange;

    private void Start()
    {
        foreach (Skill s in skills)
        {
            s.onSkillIncrease += IncreasePerks;
            s.onSkillIncrease += delegate { GainExperience(s.CurrentLevel * 30); };
        }        

        onPlayerChange?.Invoke(currentLevel, perks, ProgressToNextLevel);
    }

    public void GainExperience(float xp) 
    {
        progressTowardNextLevel += xp;

        if (progressTowardNextLevel >= xpToNextLevel)
            LevelUp(progressTowardNextLevel - xpToNextLevel);

        onChangeXP?.Invoke(ProgressToNextLevel);
    }

    private void LevelUp(float rolloverXP) 
    {
        progressTowardNextLevel = rolloverXP;

        xpToNextLevel += 100 * currentLevel;

        currentLevel++;

        onLevelUp?.Invoke(currentLevel);
    }

    private void IncreasePerks(int level) 
    {
        perks++;
        onPerkIncrease?.Invoke(perks);
    }

    private void OnValidate()
    {
        onPlayerChange?.Invoke(currentLevel, perks, ProgressToNextLevel);
    }
}
