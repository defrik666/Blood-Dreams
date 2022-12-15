using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool Door_State = false;
    private float Door_Speed = 0.0f;
    private int BlendHash;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        BlendHash = Animator.StringToHash("Blend");
    }

    private void Update()
    {
        if (Door_State == false)
        {
            if (Door_Speed > 0)
            {
                Door_Speed -= Mathf.Clamp(3 * (1.1f + (Door_Speed - 1)) * Time.deltaTime, 0, 1);
                anim.SetFloat(BlendHash, Door_Speed);
            }
        }

        if (Door_State == true)
        {
            if (Door_Speed < 1)
            {
                Door_Speed += Mathf.Clamp(3 * (1.1f - Door_Speed) * Time.deltaTime, 0, 1);
                anim.SetFloat(BlendHash, Door_Speed);
            }

        }

    }

    public void Unlocked_Door()
    {
        Door_State = !Door_State;
        anim.SetBool("Door_State", Door_State);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            Door_State = true;
            anim.SetBool("Door_State", Door_State);
            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Door_State = false;
            anim.SetBool("Door_State", Door_State);
            Debug.Log(other.name);
        }
    }
}
