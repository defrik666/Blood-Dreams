using UnityEngine;

public class CharMovement : MonoBehaviour
{

    float velocityForward = 0f;
    float velocityLeft = 0f;
    [SerializeField] float acceleration = 2f;
    [SerializeField] float deceleration = 2f;
    [SerializeField] float SpeedMod = 1f;

    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        MoveChar();
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;

        velocity.y = -9.8f * Time.deltaTime;

        characterController.Move(velocity * SpeedMod);
    }

    void MoveChar()
    {
        bool ForwardPressed = Input.GetKey("w");
        bool BackwardPressed = Input.GetKey("s");
        bool LeftPressed = Input.GetKey("a");
        bool RightPressed = Input.GetKey("d");

        if (ForwardPressed == true && velocityForward < 2f)
        {
            velocityForward += Time.deltaTime * acceleration;
        }

        if (BackwardPressed == true && velocityForward > -2f)
        {
            velocityForward -= Time.deltaTime * acceleration;
        }

        if (LeftPressed == true && velocityLeft < 2f)
        {
            velocityLeft += Time.deltaTime * acceleration;
        }

        if (RightPressed == true && velocityLeft > -2f)
        {
            velocityLeft -= Time.deltaTime * acceleration;
        }

        if (ForwardPressed == false && BackwardPressed == false && velocityForward > 0f)
        {
            velocityForward -= Time.deltaTime * deceleration;
            if (velocityForward < -0.01f)
            {
                velocityForward = 0f;
            }
        }

        if (ForwardPressed == false && BackwardPressed == false && velocityForward < 0f)
        {
            velocityForward += Time.deltaTime * deceleration;
            if (velocityForward > 0.01f)
            {
                velocityForward = 0f;
            }
        }

        if (LeftPressed == false && RightPressed == false && velocityLeft > 0f)
        {
            velocityLeft -= Time.deltaTime * deceleration;
            if (velocityLeft < -0.01f)
            {
                velocityLeft = 0f;
            }
        }

        if (LeftPressed == false && RightPressed == false && velocityLeft < 0f)
        {
            velocityLeft += Time.deltaTime * deceleration;
            if (velocityLeft > 0.01f)
            {
                velocityLeft = 0f;
            }
        }


        animator.SetFloat("SpeedForward", velocityForward);
        animator.SetFloat("SpeedLeft", velocityLeft);
    }
}