using UnityEngine;
public class Guns : MonoBehaviour
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

    [Header("Objects")]
    [SerializeField] GameObject WeaponThrow;
    RaycastHit RayHit;
    private Camera PlayerCam;
    private Animator anim;

    [Header("General Gun Stats")]
    [SerializeField] int Bullets = 12;
    [SerializeField] int Range = 100;
    [SerializeField] int ThrowForce = 20;
    [SerializeField] int ThrowUpForce = 0;
    public int damage = 1;


    [Header("Shotgun")]
    [SerializeField] bool Shotgun = false;
    [SerializeField] int BulletsPerShoot = 6;
    [SerializeField] float inaccurasyDistance = 5f;

    [Header("Animaton Controler")]
    [SerializeField] public AnimatorControler animatorControler;
    [SerializeField] public RigControler rigControler;

    [SerializeField] private LayerMask layermask;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animatorControler = FindObjectOfType<AnimatorControler>();
        rigControler = FindObjectOfType<RigControler>();
    }

    void Start()
    {
        rigControler.RigUpdate(rigControler.weapon.childCount);
        animatorControler.WeaponType(this.gameObject.name);
    }

    void Update()
    {      
        if (Input.GetMouseButtonDown(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && Bullets > 0)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Drop"))
        {
            Throw();
        }
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

    private void Shoot()
    {
        if (Shotgun == false)
        {
            muzzleFlash.Play();
            cartridgeEjection.Play();
            if (Bullets > 1) anim.Play("Attack");
            else anim.Play("Shoot_Last");

            Bullets--;

            if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out RayHit, Range))
            {
                if (RayHit.collider.CompareTag("Enemy"))
                {
                    SpawnDecal(RayHit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    RayHit.collider.GetComponentInParent<Enemy>().TakeDamage(damage);
                }
                else
                {
                    HandleHit(RayHit);
                }
            }
        }

        else
        {
            muzzleFlash.Play();
            cartridgeEjection.Play();
            anim.Play("Attack");
            Bullets--;

            for (int i = 0; i < BulletsPerShoot; i++)
            {
                if (Physics.Raycast(PlayerCam.transform.position, GetShootingDirection(), out RayHit, Range))
                {
                    if (RayHit.collider.CompareTag("Enemy"))
                    {
                        RayHit.collider.GetComponentInParent<Enemy>().TakeDamage(damage);
                        HandleHit(RayHit);
                    }
                    else
                    {
                        HandleHit(RayHit);
                    }
                }
            }
        }
    }

    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
    }

    Vector3 GetShootingDirection()
    {
        Vector3 targetPos = PlayerCam.transform.position + PlayerCam.transform.forward * Range;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-inaccurasyDistance, inaccurasyDistance),
            targetPos.y + Random.Range(-inaccurasyDistance, inaccurasyDistance),
            targetPos.z + Random.Range(-inaccurasyDistance, inaccurasyDistance)
            );

        Vector3 direction = targetPos - PlayerCam.transform.position;
        return direction.normalized;
    }

    private void Throw()
    {
        GameObject projectile = Instantiate(WeaponThrow, gameObject.transform.position, PlayerCam.transform.rotation);
        projectile.GetComponent<DropedWeapon>().Thrown = true;
        projectile.GetComponentInChildren<Animator>().Play("Throw");
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
}
