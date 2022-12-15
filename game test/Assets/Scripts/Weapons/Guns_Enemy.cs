using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns_Enemy : MonoBehaviour
{
    [Header("General Effects")]
    [SerializeField] GameObject metalHitEffect;
    [SerializeField] GameObject sandHitEffect;
    [SerializeField] GameObject stoneHitEffect;
    [SerializeField] GameObject waterLeakEffect;
    [SerializeField] GameObject waterLeakExtinguishEffect;
    [SerializeField] GameObject[] fleshHitEffects;
    [SerializeField] GameObject woodHitEffect;

    [Header("Gun Effects")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem cartridgeEjection;


    [SerializeField] Transform gun;
    bool FirstShot = true;
    RaycastHit RayHit;
    private Animator anim;
    [SerializeField] private LayerMask layermask;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void HandleHit(RaycastHit hit)
    {
        if (hit.collider.sharedMaterial != null)
        {
            string materialName = hit.collider.sharedMaterial.name;

            switch (materialName)
            {
                case "Metal":
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Sand":
                    SpawnDecal(hit, sandHitEffect);
                    break;
                case "Stone":
                    SpawnDecal(hit, stoneHitEffect);
                    break;
                case "WaterFilled":
                    SpawnDecal(hit, waterLeakEffect);
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Wood":
                    SpawnDecal(hit, woodHitEffect);
                    break;
                case "Meat":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "Character":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "WaterFilledExtinguish":
                    SpawnDecal(hit, waterLeakExtinguishEffect);
                    SpawnDecal(hit, metalHitEffect);
                    break;
            }
        }
    }

    private void Shoot(GameObject player)
    {
        muzzleFlash.Play();
        cartridgeEjection.Play();
        anim.Play("Attack");
        Vector3 dir = (player.transform.position - gun.position).normalized;
        dir.y = 0;
 
        if (Physics.Raycast(gun.position,dir ,out RayHit ,1000f ,layermask))
        {
            Debug.Log($"hit{RayHit.collider.name}");
            if (RayHit.collider.CompareTag("Player"))
            {
                SpawnDecal(RayHit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                RayHit.collider.GetComponentInParent<PlayerCharacter>().Hurt(1);
            }
            else
            {
                HandleHit(RayHit);
            }
        }

    }

    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
    }

    public IEnumerator Attack(GameObject player, bool IsVisible)
    {
        Debug.Log("work");
        if (FirstShot)
        {
            yield return new WaitForSeconds(0.5f);
            Shoot(player);
            Debug.Log("work1");
                
        }
        while (IsVisible)
        {
            yield return new WaitForSeconds(2f);
            Shoot(player);
            Debug.Log("work2");
        }
        Debug.Log("work3");

        yield return null;
    }
}
