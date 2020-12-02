using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillsSelectionControl : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect = null;
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] GameObject skillsPanel = null;

    Button[] buttons = null;

    private enum Direction { Left, Right }    

    private int currentSelection = 0;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float timeBetweenSelections = .25f;
    private float cooldown = 0;

    public bool SkillPanelOpen => skillsPanel.activeSelf;

    public void InitializeSkillInput(GameObject [] skillObjects)
    {

        buttons = new Button[skillObjects.Length];

        int i = 0;
        foreach (GameObject o in skillObjects)
        {
            buttons[i] = o.GetComponent<Button>();
            int j = i;
            buttons[i].onClick.AddListener(delegate { SelectButton(j); });
            i++;
        }

        if (buttons.Length > 0)
            SelectButton(currentSelection);
    }

    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    #region Skill tree selection
    public void SelectElementRight() => SelectElement(Direction.Right);

    public void SelectElementLeft() => SelectElement(Direction.Left);
        
    private void SelectElement(Direction direction)
    {
        if (cooldown > 0)
            return;

        cooldown = timeBetweenSelections;

        switch (direction)
        {
            case Direction.Left:
                if (currentSelection == 0)
                    currentSelection = buttons.Length - 1;
                else
                    currentSelection--;
                break;
            case Direction.Right:
                if (currentSelection == buttons.Length - 1)
                    currentSelection = 0;
                else
                    currentSelection++;
                break;
        }

        SelectButton(currentSelection);
    }

    private void SelectButton(int index) 
    {
        buttons[index].Select();
        StopAllCoroutines();

        StartCoroutine(AnimateToPos(index));
    }

    private float GetSelectedButtonPos(int index) => (float)index / (float)buttons.Length;
    #endregion
    IEnumerator AnimateToPos(int index) 
    {
        if (!scrollRect.gameObject.activeSelf)
            yield return null;

        float targetXPos = GetSelectedButtonPos(index);
        float distFromTarget = 1f;

        while (Mathf.Abs(distFromTarget) > .05f) 
        {
            scrollRect.horizontalNormalizedPosition =  Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetXPos, Time.deltaTime * scrollSpeed);

            distFromTarget = targetXPos - scrollRect.horizontalNormalizedPosition;

            cameraTransform.Rotate(Vector3.up, distFromTarget);

            yield return null;
        }
    }

    #region UI Control
    public void OpenSkills() => skillsPanel.SetActive(true);
    public void CloseSkills() => skillsPanel.SetActive(false);
    #endregion
}
