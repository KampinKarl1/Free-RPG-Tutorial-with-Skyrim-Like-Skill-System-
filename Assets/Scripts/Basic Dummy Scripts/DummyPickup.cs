using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupNoise = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))

            Pickup();
    }

    private void Pickup() 
    {
        AudioSource.PlayClipAtPoint(pickupNoise, transform.position);

        int randAmt = Random.Range(50, 500);

        JumpTextController.PlayWorldJumpMessageAt($"+${randAmt}", transform.position);

        Destroy(gameObject);
    }
}
