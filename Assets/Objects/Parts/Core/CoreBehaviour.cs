using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBehaviour : MonoBehaviour
{
    public Animator anim;

    public int temperature_step_up;
    public int maxTemp;

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

        switch (state)
        {
            case CoreState.Running:
                tempDelta += temperature_step_up;
            break;
            case CoreState.Exploded:
            break;
        }

        GridManager.Instance.AddHeat(this, tempDelta);

        switch (state)
        {
            case CoreState.Running:
                if (GridManager.Instance.GetMaxTemp(this) > maxTemp)
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
