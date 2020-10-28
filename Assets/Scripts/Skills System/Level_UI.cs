using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_UI : MonoBehaviour
{
    [SerializeField] private SkillUser user = null;

    [SerializeField] private TextMeshProUGUI levelText = null;
    [SerializeField] private TextMeshProUGUI perksToIncreaseText = null;
    [SerializeField] private Image levelProgressImage = null;

    void Awake()
    {
        if (user)
        {
            user.onPlayerChange += UpdateUI;

            user.onChangeXP += UpdateProgressBar;
            user.onLevelUp += UpdateLevel;
            user.onPerkIncrease += UpdatePerks;
        }
    }

    private void UpdateProgressBar(float progress) => levelProgressImage.fillAmount = progress;

    private void UpdateLevel(int level) => levelText.text = level.ToString();

    private void UpdatePerks(int perks) => perksToIncreaseText.text = "Perks to increase: " + perks.ToString();

    private void UpdateUI(int level, int perks, float progress) 
    {
        levelText.text = level.ToString();

        perksToIncreaseText.text = "Perks to increase: " + perks.ToString();

        levelProgressImage.fillAmount = progress;
    }
}
