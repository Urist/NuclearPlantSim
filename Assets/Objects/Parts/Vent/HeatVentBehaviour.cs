using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatVentBehaviour : MonoBehaviour
{
    public Animator anim;

    public int temperature;
    public int temperature_step_down;
    public int maxTemp;
    public int ambientTemp;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("temp", (int)ambientTemp);
    }

    // Use FixedUpdate for simulation
    void FixedUpdate()
    {
        if (temperature > ambientTemp)
        {
            temperature -= temperature_step_down;
        }

        anim.SetInteger("temp", temperature);
    }
}
