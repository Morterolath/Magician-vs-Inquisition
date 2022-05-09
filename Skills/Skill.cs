using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public List<int> layerList;     //Layer which will be affected by skill
    public float range;             //Range of the skill
    public StatusEffect status;     //Collection of Status Effects
    public PrimaryEffects prim;     //Collection of Primitive Effects

    public virtual void Use() { }
    public virtual void Use(Vector3 start, Vector3 end) { }
    public virtual void Use(Vector3 target) { }
    public virtual void Use(GameObject other) { }
}
