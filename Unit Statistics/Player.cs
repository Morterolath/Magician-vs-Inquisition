using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public PlayerBar healthBar;
    private void Start()
    {
        maxHp = 50f;
        currentHp = maxHp;
        baseArmor = 0f;
        currentArmor = baseArmor;
        healthBar.SetMaxValue(maxHp);
    }
}
