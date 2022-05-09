using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack
{
    public string name;
    public float amount;
    public float amountLimit;
    public bool isActive;
    public Stack(string name, float amountLimit)
    {
        this.name = name;
        this.amountLimit = amountLimit;
        isActive = true;
        amount = 1;
    }
}
