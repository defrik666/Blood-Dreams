using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private float Crosshair_Speed = 0.0f;
    private int BlendHash;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        BlendHash = Animator.StringToHash("Blend");
    }

    public void OnIntObj()
    {
        if (Crosshair_Speed != 1)
        {
            Crosshair_Speed += 3 * (1.5f - Crosshair_Speed) * Time.deltaTime;
            anim.SetFloat(BlendHash, Crosshair_Speed);
            if (Crosshair_Speed > 1)
            {
                Crosshair_Speed = 1;
                anim.SetFloat(BlendHash, Crosshair_Speed);
            }
        }

    }

    public void Normal()
    {
        if (Crosshair_Speed != 0)
        {
            Crosshair_Speed -= 3 * (1.1f + (Crosshair_Speed - 1)) * Time.deltaTime;
            anim.SetFloat(BlendHash, Crosshair_Speed);
            if (Crosshair_Speed < 0)
            {
                Crosshair_Speed = 0;
                anim.SetFloat(BlendHash, Crosshair_Speed);
            }
        }
   
    }
}
