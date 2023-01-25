using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
    public Vector3 movementVector = new(10,0,0);
    float movementFactor;
    const float tau = Mathf.PI * 2;
    public float period = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = period <= Mathf.Epsilon?0:Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

    }
}
