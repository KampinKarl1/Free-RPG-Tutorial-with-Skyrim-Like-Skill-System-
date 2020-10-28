using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTest : MonoBehaviour
{
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
}
