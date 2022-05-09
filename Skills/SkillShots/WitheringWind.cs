using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitheringWind : SkillShot
{
    public float damage = 1f;
    private void Start()
    {
        layerList.Add(3);
        speed = 50f;
        range = 50;
        isTargetFixed = true;
    }
    public override void Apply(GameObject other)
    {
        if (other != null)
        {
            prim.Damage(other, damage);
            float heal = (damage - other.GetComponent<Unit>().currentArmor)*3;
            prim.Heal(this.gameObject, heal);
        }
    }
    public override void End(GameObject proj, Vector3 position, Vector3 target)
    {
        if (position == target)
        {
            Destroy(proj);
        }
    }
}
