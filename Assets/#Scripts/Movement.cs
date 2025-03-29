using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    float activeForward, activeStrafe, activeHover;
    float horizontalRotate, verticalRotate;
    Vector2 lookInput, screenCentre, mouseDistance;
    public Vector3 movement;

    [Header("Script References")]

    public Timer timer;

    [Header("positional movement")]

    // speeds
    [SerializeField] float forwardSpeed = 25f;
    [SerializeField] float strafeSpeed = 7.5f;
    [SerializeField] float hoverSpeed = 5f;

    // lerp accelerations
    [SerializeField] float forwardAcceleration = 2.5f;
    [SerializeField] float strafeAcceleration = 2f;
    [SerializeField] float hoverAcceleration = 2f;

    [Header("Steer movement")]

    // speeds
    [SerializeField] float horizontalRotationSpeed = 40f;
    [SerializeField] float verticalRotationSpeed = 40f;

    void Start()
    {

    }

    void Update()
    {
        // steering
        if (Input.GetAxisRaw("HRotation") != 0)
        {
            int dir = Input.GetAxisRaw("HRotation") > 0 ? 1 : -1;
            HorizontalSteer(dir);
        }

        if (Input.GetAxisRaw("VRotation") != 0)
        {
            int dir = Input.GetAxisRaw("VRotation") > 0 ? 1 : -1;
            VerticalSteer(dir);
        }

        // positional movement
        activeForward = Mathf.Lerp(activeForward, forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafe = Mathf.Lerp(activeStrafe, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHover = Mathf.Lerp(activeHover, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        Vector3 forwardMovement = transform.forward * activeForward * Time.deltaTime;
        Vector3 strafeMovement = transform.right * activeStrafe * Time.deltaTime;
        Vector3 hoverMovement = transform.up * activeHover * Time.deltaTime;

        // Combine movement
        movement = forwardMovement + strafeMovement + hoverMovement;

        CollisionDetection(ref movement);

        // Apply movement
        transform.position += movement;

        // restart on r
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    void HorizontalSteer(int direction)
    {
        horizontalRotate = direction * horizontalRotationSpeed * Time.deltaTime;
        transform.Rotate(0f, horizontalRotate, 0f, Space.Self);
    }

    void VerticalSteer(int direction)
    {
        verticalRotate = direction * verticalRotationSpeed * Time.deltaTime;
        transform.Rotate(-verticalRotate, 0f, 0f, Space.Self);
    }

    void CollisionDetection(ref Vector3 movement)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
        {
            if (hit.collider.tag == "Untagged")
            {
                movement = movement.normalized * (hit.distance - 0.1f); // Adjust to avoid overlap
                Invoke(nameof(RestartLevel), 1f);
            }
        }
    }

    void RestartLevel()
    {
        timer.currentTime = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}