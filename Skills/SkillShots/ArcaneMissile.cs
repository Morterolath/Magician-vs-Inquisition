using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : SkillShot
{
    public float damageIncrementation;
    private void Start()
    {
        damageIncrementation = 1;
        speed = 25f;
        range = 25f;
        layerList.Add(3);
        isTargetFixed = true;
    }
    public override void Apply(GameObject other)
    {
        Stack arcane = new Stack("arcane", 10f);
        prim.AddStack(other, arcane);
        float damage = prim.GetStackAmount(other, arcane);
        prim.Damage(other, damage);
    }
    public override void End(GameObject proj, Vector3 position, Vector3 target)
    {
        if (position==target)
        {
            Destroy(proj);
        }
    }

}
