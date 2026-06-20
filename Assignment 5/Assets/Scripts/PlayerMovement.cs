using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float forwardSpeed = 8f;
    float sideSpeed = 6f;

    Rigidbody rb;

    Vector2 moveInput;

    float currentSideInput;
    float sideVelocity;

    float originalForwardSpeed;

    bool controlsReversed = false;
    Coroutine reverseControlsCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;
    }

    public void ActivateSpeedBoost(float boostSpeed, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(boostSpeed, duration));
    }

    private IEnumerator SpeedBoostRoutine(float boostSpeed, float duration)
    {
        forwardSpeed = boostSpeed;
        yield return new WaitForSeconds(duration);
        forwardSpeed = originalForwardSpeed;
    }

    public void ActivateReverseControls(float duration)
    {
        if (reverseControlsCoroutine != null)
        {
            StopCoroutine(reverseControlsCoroutine);
        }

        reverseControlsCoroutine = StartCoroutine(ReverseControlsRoutine(duration));
    }

    private IEnumerator ReverseControlsRoutine(float duration)
    {
        controlsReversed = true;

        yield return new WaitForSeconds(duration);

        controlsReversed = false;
        reverseControlsCoroutine = null;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        float targetSideInput = moveInput.x;

        if (controlsReversed)
        {
            targetSideInput *= -1f;
        }

        currentSideInput = Mathf.SmoothDamp(
            currentSideInput,
            targetSideInput,
            ref sideVelocity,
            0.1f
        );

        Vector3 movement = new Vector3(
            currentSideInput * sideSpeed,
            rb.velocity.y,
            forwardSpeed
        );

        rb.velocity = movement;
    }
}