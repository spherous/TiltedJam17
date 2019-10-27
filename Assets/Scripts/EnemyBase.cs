using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public string CurrentState;
    public abstract void EnterScaredState(float f);
}
