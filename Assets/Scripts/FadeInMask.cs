using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInMask : MonoBehaviour
{
    public void DestroyOnFinish()
    {
        Destroy(gameObject);
    }
}
