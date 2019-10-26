using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public abstract class ItemBase : MonoBehaviour
{
    Sprite UISprite;
    string itemname;
    public abstract bool Use(GameObject g);
    public abstract bool Pickup(GameObject g , int number);
}
