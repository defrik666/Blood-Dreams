using UnityEngine;

public class DropedWeapon : MonoBehaviour
{
    [Header("General Effects")]
    [SerializeField] GameObject metalHitEffect;
    [SerializeField] GameObject sandHitEffect;
    [SerializeField] GameObject stoneHitEffect;
    [SerializeField] GameObject waterLeakEffect;
    [SerializeField] GameObject waterLeakExtinguishEffect;
    [SerializeField] GameObject[] fleshHitEffects;
    [SerializeField] GameObject woodHitEffect;

    public GameObject Weapon;
    private Rigidbody rig;

    public int damage = 1;
    public bool Thrown = false;

    [Header("Bottle")]
    public Material BottleMaterial;


    private void Awake()
    {
        if (Thrown == true) 
        {
            rig = gameObject.GetComponent<Rigidbody>();
        }
    }

    public void PickUp(Transform weapon)
    {
        if (gameObject.name.Contains("GlassBottle"))
        {
            Weapon.GetComponentInChildren<Renderer>().material = BottleMaterial;
            Weapon.GetComponentInChildren<Weapon>().GlassMaterial = BottleMaterial;
        }
        Instantiate<GameObject>(Weapon, weapon);
        Destroy(gameObject);
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
        if(Thrown == true && collision.collider.CompareTag("Enemy") == true) 
        {
            HandleHit(collision);
            Destroy(gameObject);
            collision.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            Debug.Log(collision.collider.gameObject.GetComponentInParent<Enemy>().health);
            Thrown = false;
        }
        else if (Thrown == true && collision.collider.CompareTag("Player") != true)
        {
            Destroy(gameObject);
            HandleHit(collision);
            Thrown = false;
        }
    }
}