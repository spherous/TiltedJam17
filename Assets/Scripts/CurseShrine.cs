using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseShrine : MonoBehaviour
{
    GameManager gm => GameManager.Instance;
    bool used = false;
    public void Curse()
    {
        if(!used)
        {
            gm.ApplyCurse(1.1f);
        }
        used = true;
        UnityEngine.Experimental.Rendering.Universal.Light2D light;
        if( TryGetComponent(out light) ) 
        {
            light.enabled = true;
        }
    }
}
