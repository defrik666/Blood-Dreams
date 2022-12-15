using UnityEngine.Animations.Rigging;    
using UnityEngine;

public class RigControler : MonoBehaviour
{
    public Transform rightHandTarget;
    public Transform leftHandTarget;
    public Transform rightHandHint;
    public Transform leftHandHint;
    public Transform weapon;
    public TwoBoneIKConstraint leftHandIK;
    public TwoBoneIKConstraint rightHandIK;
    public RigBuilder rigBuilder;
    public Animator animator;   

    private void Awake()
    {
        leftHandIK = gameObject.transform.Find("left_hand_IK").GetComponent<TwoBoneIKConstraint>();
        rightHandIK = gameObject.transform.Find("right_hand_IK").GetComponent<TwoBoneIKConstraint>();
    }

    public void RigUpdate(int Weaponcount)
    {
        Debug.Log($"weapon count {Weaponcount}");
        if (Weaponcount == 1)
        {
            animator.enabled = false;
            rigBuilder.enabled = false;
            rightHandTarget = weapon.transform.GetChild(0).transform.Find("ref_right_hand_target");
            leftHandTarget = weapon.transform.GetChild(0).transform.Find("ref_left_hand_target");
            rightHandHint = weapon.transform.GetChild(0).transform.Find("ref_right_hand_hint");
            leftHandHint = weapon.transform.GetChild(0).transform.Find("ref_left_hand_hint");
            leftHandIK.data.target = leftHandTarget;
            rightHandIK.data.target = rightHandTarget;
            leftHandIK.data.hint = leftHandHint;
            rightHandIK.data.hint = rightHandHint;
            rigBuilder.Build();
            animator.Rebind();
            rigBuilder.enabled = true;
            animator.enabled = true;


        }
        else
        {
            animator.enabled = false;
            rigBuilder.enabled = false;
            leftHandIK.data.target = null;
            rightHandIK.data.target = null;
            leftHandIK.data.hint = null;
            rightHandIK.data.hint = null;
            rigBuilder.Build();
            animator.Rebind();
            rigBuilder.enabled = true;
            animator.enabled = true;
        }
    }

}
