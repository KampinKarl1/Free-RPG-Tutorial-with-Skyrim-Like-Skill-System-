using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill_UI : MonoBehaviour
{
    private Skill skill = null;

    [SerializeField] private TextMeshProUGUI skillName = null;
    [SerializeField] private TextMeshProUGUI skillLevel = null;
    [SerializeField] private Image levelBar = null;

    public void InitializeSkillTreeUI(Skill skill) 
    {
        this.skill = skill;

        skillName.text = skill.Name;
        skillLevel.text = skill.CurrentLevel.ToString();

        levelBar.fillAmount = skill.Progress;

        skill.onSkillIncrease += UpdateLevel;
        skill.onChangeXP += UpdateXP;
    }

    private void UpdateLevel(int level) => skillLevel.text = level.ToString();

    private void UpdateXP(float progress) => levelBar.fillAmount = progress;

    private void OnDisable()
    {
        skill.onChangeXP -= UpdateXP;
        skill.onSkillIncrease -= UpdateLevel;
    }
}
