using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;



    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period; //continually grow over time.
        
        const float tau = Mathf.PI * 2; // tau is defined as 6.283 or 2(Pi)
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1 

        movementFactor = (rawSinWave +1f) / 2f; // recalculate to go from 0 to 1 instead of -1 to 1 for cleaner math and understanding.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
