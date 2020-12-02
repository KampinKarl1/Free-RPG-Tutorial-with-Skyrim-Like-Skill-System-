using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    private bool CollidingWithPlayer(GameObject colliderObj) => colliderObj.CompareTag("Player");

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollidingWithPlayer(other.gameObject))
            MoveAwayFromPlayer(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
    }

    private void MoveAwayFromPlayer(Transform player) 
    {
        Vector3 dest = transform.position - player.position;
        agent.SetDestination(dest);
    }
}
