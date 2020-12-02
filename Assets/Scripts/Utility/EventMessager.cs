using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMessager : MonoBehaviour
{
    void Start()
    {
        if (!JumpTextController.Instance)
        {
            throw new System.Exception("Event messager won't work without something to print messages to the screen");
        }

        SkillUser[] skillUsers = FindObjectsOfType<SkillUser>();

        foreach (SkillUser u in skillUsers) 
        {
            foreach (Skill s in u.Skills) 
            {
                s.onSkillIncrease += delegate { JumpTextController.PlayScreenMessage($"{u.name} leveled up {s.Name} to {s.CurrentLevel}!"); };
            }
        }
    }
}
