using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    private int z = 1;
    private bool lastPos = false;
    private bool Shooting = false;
    public float dist;
    public float angle;


    public Rigidbody[] RigidBodies;
    public LayerMask obstructionMask;
    public Weapon_Enemy Weapon_Enemy;
    public Guns_Enemy Guns_Enemy;
    public GameObject Weapon;
    public Exit exit;
    private Camera cam;
    public GameObject player;
    private UnityEngine.AI.NavMeshAgent NMA;
    private Vector3 playerLastPos;
    private Animator anim;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        NMA = (UnityEngine.AI.NavMeshAgent)this.GetComponent("NavMeshAgent");
        RigidBodies = GetComponentsInChildren<Rigidbody>();
        exit = FindObjectOfType<Exit>();
    }

    void Start() 
    {
        playerLastPos = gameObject.transform.position;
    }


    void Update()
    {
        if (player != null && health > 0) 
        {
            anim.SetFloat("Blend", NMA.velocity.magnitude / NMA.speed);
            if (IsVisible(cam, player))
            {
                NMA.SetDestination(player.transform.position);
                z = 1;
                if (lastPos == false) StartCoroutine(LastPos());
                if (angle < 50 && Weapon_Enemy != null) Weapon_Enemy.Attack(dist);
                if (dist <= 5 && Weapon_Enemy != null) RotateTowards(player.transform);
                if (angle < 20 && Guns_Enemy != null)
                {
                    IEnumerator GunAttack = Guns_Enemy.Attack(player, IsVisible(cam, player));
                    if (Shooting == false)
                    {
                        Guns_Enemy.StartCoroutine(GunAttack);
                        Shooting = true;
                    }                     
                }
                else if (Shooting)
                {
                    Shooting = false;
                    Guns_Enemy.StopAllCoroutines();
                }
                if (dist <= 30 && Guns_Enemy != null) RotateTowards(player.transform);
            }
            else
            {
                NMA.SetDestination(playerLastPos);
            }

        }


    }

    public bool IsVisible(Camera cam,GameObject Player)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var playercam = Player.GetComponentInChildren<Camera>();
        var point = playercam.transform.position;
        Vector3 dir = (point - cam.transform.position).normalized;
        Vector3 dir2 = (Player.transform.position - cam.transform.position).normalized;
        dist = Vector3.Distance(cam.transform.position, point);
        angle = Vector3.Angle(dir,cam.transform.forward);
        Debug.DrawRay(cam.transform.position,dir,Color.blue);

        if (dist < 5)
        {
            if (!Physics.Raycast(cam.transform.position, dir, dist, obstructionMask) || !Physics.Raycast(cam.transform.position, dir2, dist, obstructionMask))
            {
                return true;
            }
            else return false;
        }
        else
        {
            foreach (var plane in planes)
            {
                if (plane.GetDistanceToPoint(point) < 0)
                {
                    return false;
                }
            }
            if (!Physics.Raycast(cam.transform.position, dir, dist, obstructionMask) || !Physics.Raycast(cam.transform.position, dir2, dist, obstructionMask))
            {
                return true;
            }
            else return false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            foreach (Rigidbody rb in RigidBodies) rb.isKinematic = false;
            NMA.enabled = false;
            anim.enabled = false;
            this.enabled = false;
            Destroy(Weapon);
            exit.CheckEnemies();
        }
    }

    IEnumerator LastPos()
    {
        lastPos = true;
        for (int x = z; x < 5; x++)
        {
            playerLastPos = player.transform.position;
            x = z;
            z++;
            yield return new WaitForSeconds(0.3f);
        }
        lastPos = false;
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360);
    }
}
