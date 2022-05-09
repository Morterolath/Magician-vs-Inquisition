using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterKnight : Unit
{
    public GreaterHealthBar healthBar;
    void Start()
    {
        maxHp = 10f;
        baseArmor = 0f;
        currentArmor = baseArmor;
        currentHp = maxHp;
        baseMovementSpeed = 2f;
        currentMovementSpeed = baseMovementSpeed;
        healthBar.SetMaxValue(maxHp);
    }
}
