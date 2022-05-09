using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Hashtable stackList = new Hashtable();
    public Hashtable effectList = new Hashtable();
    public float maxHp;
    public float currentHp;
    public float baseArmor;
    public float currentArmor;
    public float currentMovementSpeed;
    public float baseMovementSpeed;
}
