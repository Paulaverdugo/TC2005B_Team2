// script for the circuit breaker sound effect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBreakerPlayer : MonoBehaviour
{
    public void Play()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
