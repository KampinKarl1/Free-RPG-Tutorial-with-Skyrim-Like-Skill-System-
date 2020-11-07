using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnimsFREE;

public class CombatTest : MonoBehaviour
{
    [SerializeField] private RPGCharacterControllerFREE controller = null;
    
    [SerializeField] private LayerMask enemyMask = 1 << 10;
    [SerializeField] private Vector3 heightOffset = new Vector3();

    [SerializeField] private float attackRadius = 5.0f;
    [SerializeField] private float damage = 50f;

    private void Start()
    {
        enemyMask = 1 << LayerMask.NameToLayer("Enemy");

        heightOffset = new Vector3(0, transform.localScale.y / 2, 0);
    }

    public void Hit() 
    {
        Ray ray = new Ray(transform.position + heightOffset, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRadius, enemyMask))
        {
            Enemy e;

            if (hit.collider.TryGetComponent(out e))
                e.ReceiveDamage(damage);
        }        
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
        Collider[] cols = Physics.OverlapSphere(transform.position, attackRadius, enemyMask);

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
}
