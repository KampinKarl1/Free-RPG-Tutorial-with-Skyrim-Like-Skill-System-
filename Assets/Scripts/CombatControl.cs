using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnimsFREE;

public class CombatControl : MonoBehaviour
{
    private RPGCharacterControllerFREE  controller      = null;

    [SerializeField] 
    private SkillUser                   player          = null;

    private Vector3                     heightOffset    = new Vector3(0, 1, 0);
    private LayerMask                   enemyLayermask  = 1 << 10;

    [Header("Attributes")]
    [SerializeField] 
    private float                       attackDistance  = 3.0f;

    [SerializeField] 
    private AttackList                  attackList      = null;

    public  delegate void               OnUseAttack(AttackType typeOfAttack);
    public                              OnUseAttack     onUseAttack;

    private void Start()
    {
        controller      = GetComponentInParent<RPGCharacterControllerFREE>();

        heightOffset    = new Vector3(0, transform.localScale.y / 2, 0);

        if (LayerMask.NameToLayer("Enemy") > -1 && //If enemy layer exists
            enemyLayermask != 1 << LayerMask.NameToLayer("Enemy")) //but our enemymask isn't setup
            enemyLayermask  = 1 << LayerMask.NameToLayer("Enemy");

        attackList.InitializeAttacks();
    }

    public void LookForTarget() 
    {
        GameObject targetObj = FindClosestEnemy();


        //If the controller doesnt have a target but theres an enemy nearby
        //OR
        //The controller has a target but there ISN'T an enemy nearby
        if (targetObj != null
        ||
        controller.HasTarget && targetObj == null)
        {
            controller.SetTarget(targetObj);
        }
    }

    private GameObject FindClosestEnemy()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, attackDistance, enemyLayermask);

        if (cols.Length == 0)
            return null;

        GameObject closestEnemyObj = null;

        float close = Mathf.Infinity;

        for (int i = 0; i < cols.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, cols[i].transform.position);
            if (dist < close)
            {
                close = dist;
                closestEnemyObj = cols[i].gameObject;
            }
        }
        return closestEnemyObj;
    }

    //This is called from Animation Events
    public void Hit(Object attack)
    {
        Ray ray = new Ray(transform.position + heightOffset, transform.forward);
        RaycastHit hit;

        AttackType a = attack as AttackType;

        if (Physics.Raycast(ray, out hit, attackDistance, enemyLayermask))
        {
            Enemy e;
            if (hit.collider.TryGetComponent(out e))
            {
                e.ReceiveDamage(a.GetDamage);

                float xp = e.Health > 0 ? a.GetXPForAttack : a.GetXPForKill;

                a.GetGoverningSkill.IncreaseSkill(xp);
            }
        }

        onUseAttack?.Invoke(a);
    }
}
