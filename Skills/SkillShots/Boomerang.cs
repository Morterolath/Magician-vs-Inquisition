using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : SkillShot
{
    [SerializeField] float damage;
    [SerializeField] float trueDamage;
    private void Start()
    {
        trueDamage = 3f;
        damage = 1f;
        range = 25f;
        speed = 20f;
        layerList.Add(3);
        isTargetFixed = true;
    }
    public override void Apply(GameObject other)
    {
        if (projTemp.GetComponent<ProjectileBehaviour>().collisionCounter >= 6)
        {
            prim.TrueDamage(other, trueDamage);
        }
        else
        {
            prim.Damage(other, damage);

        }
    }
    public override void End(GameObject proj, Vector3 position, Vector3 target)
    {
        if (position == transform.position)
        {
            Destroy(proj);
        }
    }
    public override void Movement(GameObject obj, Vector3 start, Vector3 target, float speed)
    {
        if (obj.transform.position == target)
        {
            obj.GetComponent<ProjectileBehaviour>().target = transform.position;
        }
        obj.transform.position=  Vector3.MoveTowards(start, target, speed);
    }
}
