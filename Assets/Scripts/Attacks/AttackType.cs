using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Attacks/New Attack Type")]
public class AttackType : ScriptableObject
{
    [SerializeField] private Skill governingSkill = null;
    [SerializeField] private float baseDamage = 0;
    [SerializeField] private float increasePerLevel = 0;
    [SerializeField] private float baseXPAttack = 0;
    [SerializeField] private float baseXPKill = 0;

    private float currentDamage = 0;
    private float currentXPAttack = 0;
    private float currentXPKill;

    public Skill GetGoverningSkill => governingSkill;
    public float GetDamage => currentDamage;
    public float GetXPForAttack => currentXPAttack;
    public float GetXPForKill => currentXPKill;

    public void InitializeAttackType()
    {
        currentDamage = baseDamage;
        currentXPAttack = baseXPAttack;
        currentXPKill = baseXPKill;

        for (int i = 0; i < governingSkill.CurrentLevel; ++i)
            IncreaseSkill(i);

        governingSkill.onSkillIncrease += IncreaseSkill;
    }

    private void IncreaseSkill(int skillLevel)
    {
        currentDamage += increasePerLevel;
        currentXPAttack += baseXPAttack + (1 * skillLevel);
        currentXPKill += baseXPKill + (1 * skillLevel);
    }
}
