using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Enemy : MonoBehaviour
{
    [Header("General Effects")]
    [SerializeField] GameObject metalHitEffect;
    [SerializeField] GameObject sandHitEffect;
    [SerializeField] GameObject stoneHitEffect;
    [SerializeField] GameObject waterLeakEffect;
    [SerializeField] GameObject waterLeakExtinguishEffect;
    [SerializeField] GameObject[] fleshHitEffects;
    [SerializeField] GameObject woodHitEffect;

    [SerializeField] float distanceOfAttack = 2.5f;
    [SerializeField] float timeBetweenAttacks = 2f;

    public bool attack = false;

    private Animator anim;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Attack(float dist)
    {
        if (attack == false) StartCoroutine(Attack1(dist));
    }

    void HandleHit(Collision collision)
    {
        if (collision.collider.sharedMaterial != null)
        {
            string materialName = collision.collider.sharedMaterial.name;

            switch (materialName)
            {
                case "Metal":
                    SpawnDecal(collision, metalHitEffect);
                    break;
                case "Sand":
                    SpawnDecal(collision, sandHitEffect);
                    break;
                case "Stone":
                    SpawnDecal(collision, stoneHitEffect);
                    break;
                case "WaterFilled":
                    SpawnDecal(collision, waterLeakEffect);
                    SpawnDecal(collision, metalHitEffect);
                    break;
                case "Wood":
                    SpawnDecal(collision, woodHitEffect);
                    break;
                case "Meat":
                    SpawnDecal(collision, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "Character":
                    SpawnDecal(collision, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "WaterFilledExtinguish":
                    SpawnDecal(collision, waterLeakExtinguishEffect);
                    SpawnDecal(collision, metalHitEffect);
                    break;
            }
        }
    }

    void SpawnDecal(Collision collision, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
        spawnedDecal.transform.SetParent(collision.transform);
        new WaitForSeconds(5f);
        Destroy(spawnedDecal);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (collision.collider.CompareTag("Enemy") != true)
            {
                if (collision.collider.CompareTag("Player"))
                {
                    HandleHit(collision);
                    collision.collider.gameObject.GetComponent<PlayerCharacter>().Hurt(1);
                }
            }
        }
    }

    IEnumerator Attack1(float dist)
    {
        if (dist < distanceOfAttack)
        {
            anim.Play("Attack");
            attack = true;
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        attack = false;
        yield return null;

    }
}
