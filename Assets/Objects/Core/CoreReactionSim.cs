using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreReactionSim : MonoBehaviour
{
    public ulong neutronPopulation;
    public double effectiveMultiplicationFactor;

    public double THERMAL_FISSION_FACTOR = 1.65;
    public double THERMAL_ULTILIZATION_FACTOR = 0.71;
    public double RESONANCE_ESCAPE_PROBABILITY = 0.87;
    public double FAST_FISSION_FACTOR = 1.02;
    public double FAST_NONLEAKAGE_PROBABILITY = 0.97;
    public double THERMAL_NONLEAKAGE_PROBABILITY = 0.99;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Use FixedUpdate for simulation
    void FixedUpdate()
    {
        // Calulate next generation using Six Factor Formula
        effectiveMultiplicationFactor = THERMAL_FISSION_FACTOR * 
                    THERMAL_ULTILIZATION_FACTOR *
                    RESONANCE_ESCAPE_PROBABILITY *
                    FAST_FISSION_FACTOR *
                    FAST_NONLEAKAGE_PROBABILITY *
                    THERMAL_NONLEAKAGE_PROBABILITY;

        neutronPopulation = (ulong)Math.Floor(neutronPopulation * effectiveMultiplicationFactor);
    }
}
