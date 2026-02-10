using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class MuzzleFlash: MonoBehaviour

{

    [Header("Light Settings")]

    public Light2D circleLight; // Reference to the 2D Circle Light

    public Transform firePoint; // The position where the light should appear

    public float flashDuration = 0.3f; // Duration the light stays active

    private void Start()

    {

        // Ensure the light is initially inactive

        if (circleLight != null)

        {

            circleLight.enabled = false;

        }

        else

        {

            Debug.LogError("LightFlash: CircleLight reference is missing. Assign it in the Inspector.");

        }

    }

    private void Update()

    {

        // Check for left mouse button press

        if (Input.GetMouseButtonDown(0))

        {

            FlashLight();

        }

    }

    private void FlashLight()

    {

        if (circleLight != null && firePoint != null)

        {

            // Position the light at the FirePoint

            circleLight.transform.position = firePoint.position;

            // Enable the light and start the flash coroutine

            circleLight.enabled = true;

            StartCoroutine(DisableLightAfterDelay());

        }

        else

        {

            Debug.LogError("LightFlash: Missing reference to circleLight or firePoint. Ensure both are assigned in the Inspector.");

        }

    }

    private IEnumerator DisableLightAfterDelay()

    {

        // Wait for the specified duration

        yield return new WaitForSeconds(flashDuration);

        // Disable the light

        circleLight.enabled = false;

    }

}

/*

Purpose of the Script:

This script activates a 2D Freeform > Circle Light for a short duration at a specified FirePoint position whenever the left mouse button is pressed.

How to Implement:

1. Attach this script to the ‘FirePoint’ GameObject.

2. Assign a 2D Circle Light to the `Circle Light` field in the Inspector.

3. Assign the FirePoint Transform to the `Fire Point` field in the Inspector.

4. Adjust the `Flash Duration` in the Inspector to change how long the light stays active.

5. Press the left mouse button during gameplay to see the light flash effect.

*/