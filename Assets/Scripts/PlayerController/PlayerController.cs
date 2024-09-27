using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReaderSO input;
    [SerializeField] private float moveSpeed;
    [Range(0.0f, 0.3f)]
    [SerializeField] private float RotationSmoothTime = 0.12f;
    [SerializeField] private float SpeedChangeRate = 10.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private float jumpTimeout = 0.50f;
    private float fallTimeout = 0.15f;
    private float gravity = -15.0f;
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    private Vector2 moveDirection;
    private float speed;
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;
    private const string Speed = "Speed";
    private bool isAnalogMovement = false;
    private bool hasAnimator;

    private void Start()
    {
        input.SetGamePlayInput();
        hasAnimator = animator != null;
        input.MoveEvent += MoveHandle;
    }
    private void MoveHandle(Vector2 direction)
    {
        moveDirection = direction;
    }
    private void Update()
    {
        GroundedCheck();
        Movement();
    }
    private void Movement()
    {
        float targetSpeed = moveSpeed;
        if (moveDirection == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
        float speedOffset = 0.1f;
        float inputMagnitude = isAnalogMovement ? moveDirection.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.y).normalized;
        if (moveDirection != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

        if (hasAnimator)
        {
            animator.SetFloat(Speed, animationBlend);
        }
    }
    private void GroundedCheck()
    {
        if (controller.isGrounded)
        {
            fallTimeoutDelta = fallTimeout;

            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }
            if (jumpTimeoutDelta >= 0.0f)
            {
                jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        if(!controller.isGrounded)
        {
            jumpTimeoutDelta = jumpTimeout;

            if (fallTimeoutDelta >= 0.0f)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
        }

        if (verticalVelocity < terminalVelocity)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

}
