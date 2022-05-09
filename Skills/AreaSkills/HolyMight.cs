using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMight : AreaSkill
{
    private void Start()
    {
        layerList.Add(3);
        duration = 5f;
        range = 25f;    
    }
    public override void ApplyOnStay(GameObject other)
    {
        prim.SetArmor(other, 0f);
    }
    public override void ApplyOnExit(GameObject other)
    {
        prim.RestoreArmor(other);
    }
    public override void End(GameObject obj, float duration)
    {
        StartCoroutine(SkillFinisher(obj, duration));
    }
}
