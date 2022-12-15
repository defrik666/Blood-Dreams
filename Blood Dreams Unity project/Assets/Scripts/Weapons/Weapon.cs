using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("General Effects")]
    [SerializeField] GameObject metalHitEffect;
    [SerializeField] GameObject sandHitEffect;
    [SerializeField] GameObject stoneHitEffect;
    [SerializeField] GameObject waterLeakEffect;
    [SerializeField] GameObject waterLeakExtinguishEffect;
    [SerializeField] GameObject[] fleshHitEffects;
    [SerializeField] GameObject woodHitEffect;

    public GameObject WeaponThrow;
    public Camera PlayerCam;


    private Animator anim;

    public int health = 3;
    public int damage = 1;
    [SerializeField] int ThrowForce = 20;
    [SerializeField] int ThrowUpForce = 0;

    [Header("Animaton Controler")]
    public AnimatorControler animatorControler;
    public RigControler rigControler;

    [SerializeField] private LayerMask layermask;
    private bool isHit;

    [Header("GlassBottle")]
    public GameObject BrokenBottle;
    public GameObject GlassBreak;
    public Material GlassMaterial;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animatorControler = FindObjectOfType<AnimatorControler>();
        rigControler = FindObjectOfType<RigControler>();
    }

    void Start()
    {
        StartCoroutine(StartPrep());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHit == false)
        {
            anim.Play("Attack");
        }

        if (Input.GetButtonDown("Drop"))
        {
            Throw();
        }
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
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && isHit == false && collision.collider.CompareTag("Player") != true)
        {
            isHit = true;
            StartCoroutine(Hit());
            HandleHit(collision);
            if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
                Debug.Log(collision.collider.gameObject.GetComponentInParent<Enemy>().health);
                health -= 1;
                if (health <= 0)
                {
                    if (rigControler.weapon.GetChild(0).name.StartsWith("GlassBottleWeapon"))
                    {
                        Destroy(gameObject);
                        GameObject brokenbottle = Instantiate<GameObject>(BrokenBottle, rigControler.weapon);
                        brokenbottle.GetComponent<Weapon>().GlassMaterial = GlassMaterial;
                        brokenbottle.GetComponent<Renderer>().material = GlassMaterial;
                        SpawnDecal(collision, GlassBreak);
                    }
                    else
                    {
                        Destroy(gameObject);
                        rigControler.RigUpdate(0);
                        animatorControler.WeaponType("None");
                    }
                }

            }
        }
    }


    private void Throw()
    {
        GameObject projectile = Instantiate(WeaponThrow, gameObject.transform.position, gameObject.transform.rotation);
        projectile.GetComponent<DropedWeapon>().Thrown = true;
        projectile.GetComponentInChildren<Animator>().Play("Throw");
        if (this.gameObject.name.Contains("GlassBottle") == true) projectile.GetComponentInChildren<Renderer>().material = GlassMaterial;
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = PlayerCam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, 500f, layermask))
        {
            forceDirection = (hit.point - gameObject.transform.position).normalized;
        }
        Vector3 forceToAdd = forceDirection * ThrowForce + transform.up * ThrowUpForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        rigControler.RigUpdate(0);
        animatorControler.WeaponType("None");
        Destroy(this.gameObject);
    }


    IEnumerator Hit()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return new WaitForSeconds(0.1f);
        }
        isHit = false;
        yield return null;
    }

    IEnumerator StartPrep()
    {
        yield return new WaitForEndOfFrame();
        rigControler.RigUpdate(1);
        animatorControler.WeaponType(gameObject.name);
        yield return null;
    }
}
