using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    public GameObject FallingInit;
    public GameObject FallingDown;

    public GameObject FallingLight;

    public AkState FallingDownEnabledState;
    public AkState FallingLightEnabledState;

    void Update()
    {
        uint fallingDownState;
        AkSoundEngine.GetState("FallingDown", out fallingDownState);

        if (fallingDownState == FallingDownEnabledState.data.Id)
        {
            FallingInit.SetActive(false);
            FallingDown.SetActive(true);
        }

        uint fallingLightState;
        AkSoundEngine.GetState("FallingLight", out fallingLightState);

        if (fallingLightState == FallingLightEnabledState.data.Id)
        {
            FallingLight.SetActive(true);
        }
    }
}
