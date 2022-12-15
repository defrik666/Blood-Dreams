using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScreen : MonoBehaviour
{
    private Animator anim;
    private int BlendHash;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        BlendHash = Animator.StringToHash("Blend");
    }

    private void Start()
    {
    }

    public void ChangeScreen(int health, int maxHealth)
    {
        anim.SetFloat(BlendHash, (float)health / (float)maxHealth);
    }
}
