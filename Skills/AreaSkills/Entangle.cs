using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entangle : AreaSkill
{
    [SerializeField] float movementSpeed;
    private void Start()
    {
        layerList.Add(3);
        movementSpeed = 1f;
        layerList.Add(3);
        duration = 8f;
        range = 25f;
    }
    public override void ApplyOnStay(GameObject other)
    {
        prim.SetMovementSpeed(other, movementSpeed);
    }
    public override void ApplyOnExit(GameObject other)
    {
        prim.RestoreMovementSpeed(other);
    }
    public override void End(GameObject obj, float duration)
    {
        StartCoroutine(SkillFinisher(obj, duration));
    }
}
