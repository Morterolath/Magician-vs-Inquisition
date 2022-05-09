using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserKnight : Unit
{
    public LesserHealthBar healthBar;
    private void Start()
    {
        maxHp = 5f;
        currentHp = maxHp;
        baseArmor = 1f;
        currentArmor = baseArmor;
        baseMovementSpeed = 5f;
        currentMovementSpeed = baseMovementSpeed;
        healthBar.SetMaxValue(maxHp);
    }
}
