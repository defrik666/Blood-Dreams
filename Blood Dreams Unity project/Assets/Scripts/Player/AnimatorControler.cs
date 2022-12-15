using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void WeaponType(string weaponName)
    {
        switch (weaponName) 
        {
            case "ShotgunWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun Hands"), 1);
                break;
            case "ShotgunWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun Hands"), 1);
                break;
            case "PMWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol Hands"), 1);
                break;
            case "PMWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol Hands"), 1);
                break;
            case "BatWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "BatWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "PipeWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "PipeWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "KatanaWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "KatanaWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "KyloWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break;
            case "KyloWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 1);
                break; 
            case "KnifeWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "KnifeWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "KitchenKnifeWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "KitchenKnifeWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "HunterKinfeWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "HunterKinfeWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "GlassBottleWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1); 
                break;
            case "GlassBottleWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "BrokenGlassBottleWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "BrokenGlassBottleWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "HammerWeapon":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "HammerWeapon(Clone)":
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 1);
                break;
            case "None":
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun Hands"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol Hands"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 0);
                break;
            default:
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Shotgun Hands"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Pistol Hands"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Melee Two Hands"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Melee One Hand"), 0);
                break;
        }
    } 

}


