using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraInteraction : MonoBehaviour
{
    Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2);


    Door door;
    Crosshair crosshair;
    DropedWeapon dropedweapon;


    public Transform weapon;


    public float rayLength;
    public LayerMask LayerMask;


    private void Start()
    {
        crosshair = FindObjectOfType<Crosshair>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Input.GetButtonDown("Interaction"))
        {
            Physics.Raycast(ray, out hit, rayLength, LayerMask);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.CompareTag("Door"))
                {
                    door = hit.collider.GetComponent<Door>();
                    door.Unlocked_Door();
                }

                else if (hit.collider.CompareTag("Weapon"))
                {
                    if (weapon.gameObject.GetComponentInChildren<Weapon>() == null && weapon.gameObject.GetComponentInChildren<Guns>() == null )
                    {
                        dropedweapon = hit.collider.GetComponentInParent<DropedWeapon>();
                        if (dropedweapon.gameObject.name.Contains("GlassBottle") == true) dropedweapon.BottleMaterial = hit.collider.GetComponentInParent<Renderer>().material;
                        dropedweapon.PickUp(weapon);    
                    }
                }
            }
        }

        Physics.Raycast(ray, out hit, rayLength, LayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 31)
            {
                crosshair.OnIntObj();
            }

            else 
            {
                crosshair.Normal();
            }
        }

        else
        {
            crosshair.Normal();
        }
    }
}

