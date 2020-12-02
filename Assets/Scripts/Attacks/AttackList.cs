using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Attacks/New Attack List"))]
public class AttackList : ScriptableObject
{
    [SerializeField] private List<AttackType> attacks = new List<AttackType>();
    public List<AttackType> Attacks => attacks;

    public void InitializeAttacks()
    {
        if (attacks.Count == 0)
            return;

        foreach (AttackType a in attacks)
            a.InitializeAttackType();
    }
}