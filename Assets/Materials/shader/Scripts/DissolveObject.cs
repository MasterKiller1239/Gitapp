using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DissolveObject : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;
    public bool active = false;
    private Material material;
    public bool disapear = false;

    // Movement speed in units per second.
    public float speed = 1.0F;
    public float height =3;
    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    public float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        
    }
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (active&&height<10)
        {
            
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            height += fractionOfJourney;
            SetHeight(height);
        }
        if (!active && height > -1&& disapear==true)
        {

            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            height -= fractionOfJourney;
            SetHeight(height);
        }



    }
    public void activite()
    {
        startTime = Time.time;
        active = true;
    }
    void OnMouseOver()
    {

        active = true;

    }
    void OnMouseExit()
    {
        active = false;
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
