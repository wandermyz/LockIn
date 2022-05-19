using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyselfRTPCController : MonoBehaviour
{
    public AK.Wwise.RTPC MyEyeDistance;
    public AK.Wwise.RTPC MyFootDistance;

    public Transform MyEye;
    public Transform MyFoot;

    void Update()
    {
        Vector3 playerPos = GameManager.Instance.PlayerController.transform.position;
        float eyeDistance = Vector3.Distance(playerPos, MyEye.position);
        float footDistance = Vector3.Distance(playerPos, MyFoot.position);

        BGMManager.Instance.SetRTPCValue(MyEyeDistance, eyeDistance);
        BGMManager.Instance.SetRTPCValue(MyFootDistance, footDistance);
    }
}
