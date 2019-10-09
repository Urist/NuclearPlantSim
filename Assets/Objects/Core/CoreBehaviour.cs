using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBehaviour : MonoBehaviour
{
    public Animator anim;

    public int temperature;
    public int temperature_step_up;
    public int temperature_step_down;
    public int temperature_transfer_rate;
    public int maxTemp;
    public int ambientTemp;

    public enum CoreState
    {
        Running,
        Exploded
    }

    public CoreState state;

    // Start is called before the first frame update
    void Start()
    {
        state = CoreState.Running;
    }

    // Use FixedUpdate for simulation
    void FixedUpdate()
    {
        int tempDelta = 0;

        if (temperature > ambientTemp)
        {
            tempDelta -= temperature_step_down;
        }

        foreach (var heatsink in GameObject.FindGameObjectsWithTag("HeatSink"))
        {
            if (temperature > ambientTemp)
            {
                tempDelta -= temperature_transfer_rate;
                (heatsink.GetComponent<HeatVentBehaviour>()).temperature += temperature_transfer_rate;
            }
        }

        switch (state)
        {
            case CoreState.Running:
                tempDelta += temperature_step_up;
            break;
            case CoreState.Exploded:
            break;
        }

        temperature = temperature + tempDelta;

        switch (state)
        {
            case CoreState.Running:
                if (temperature > maxTemp)
                {
                    state = CoreState.Exploded;
                    anim.Play("core_explode");
                }
            break;
            case CoreState.Exploded:
            break;
        }

    }
}
