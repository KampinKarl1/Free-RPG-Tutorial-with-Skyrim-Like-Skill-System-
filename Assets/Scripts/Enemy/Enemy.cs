using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player = null;

    [SerializeField] private GameObject prototypePickup = null;

    [SerializeField] private float health = 100;
    public float Health => health;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InitializeHitEffect();
    }

    private void Update()
    {
        transform.LookAt(player);
    }

    public void ReceiveDamage(float damage) 
    {
        //So we dont get credit for multiple kills on the same object
        if (health <= 0)
            return;

        health -= damage;

        DoHitEffect();

        if (health <= 0)
            Die();
    }

    private void Die() 
    {
        prototypePickup.transform.SetParent(null);
        prototypePickup.SetActive(true);

        Destroy(gameObject);
    }

    #region Stuff for Particles 
    const float HIT_FX_DUR = .75f;
    [SerializeField] private GameObject hitEffectPrefab = null;
    private GameObject hitEffect = null;
    private ParticleSystem ps = null;
    #endregion
    #region Helper
    private void InitializeHitEffect() 
    {
        hitEffect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        hitEffect.SetActive(false);

        ps = hitEffect.GetComponentInChildren<ParticleSystem>();
    }

    private void DoHitEffect() 
    {
        StartCoroutine(HitEffect());
    }

    private IEnumerator HitEffect() 
    {
        hitEffect.SetActive(true);
        hitEffect.transform.position = transform.position + new Vector3(0, transform.localScale.y / 2, 0);
        ps.Play();
        yield return new WaitForSeconds(HIT_FX_DUR);
        hitEffect.SetActive(false);
    }
    #endregion
}
