using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneExplosion : AreaSkill
{
    [SerializeField] float outerRadius;
    [SerializeField] float innerRadius;
    [SerializeField] float centralRadius;
    [SerializeField] float outerDamage;
    [SerializeField] float innerDamage;
    [SerializeField] float centralDamage;
    IEnumerator areaDamage;

    private void Start()
    {
        layerList.Add(3);
        layerList.Add(7);

        outerRadius = 5f;
        innerRadius = 2.5f;
        centralRadius = 1.75f;

        outerDamage = 1f;
        innerDamage = 1f;
        centralDamage = 2f;

        range = 25;
        duration = 0f;
    }
    public override void ApplyOnCheck(Vector3 point)
    {
        if (currentCast == null)
        {
            areaDamage = CastWithLatency(point);
            StartCoroutine(areaDamage);
        }
    }
    IEnumerator CastWithLatency(Vector3 point)
    {
        Debug.Log("hit");
        yield return new WaitForSeconds(latency);
        prim.AreaDamage(point, outerDamage, outerRadius, layerList);
        prim.AreaDamage(point, innerDamage, innerRadius, layerList);
        prim.AreaDamage(point, centralDamage, centralRadius, layerList);
        currentCast = null;
    }
    public override void End(GameObject obj, float duration)
    {
        StartCoroutine(SkillFinisher(obj, 0.01f));
    }
}
