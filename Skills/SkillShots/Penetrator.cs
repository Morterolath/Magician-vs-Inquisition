using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrator : SkillShot
{
    public GameObject explosion;
    public float damageRate;
    GameObject explosionTemp;

    private void Start()
    {
        layerList.Add(3);
        damageRate = 3f;
        speed = 30f;
        range = 25f;
        isTargetFixed = true;
    }
    public override void Apply(GameObject other)
    {
        prim.DamageAccordingToCurrentArmor(other, damageRate);
    }
    public override void End(GameObject proj, Vector3 objectPosition, Vector3 targetPosition)
    {
        if (proj.GetComponent<ProjectileBehaviour>().collisionCounter>=1 || objectPosition == targetPosition)
        {
            CreateExplosion(objectPosition);
            Debug.Log("Hit");
            Destroy(proj);
        }
    }

    //Methods for Explosion
    void CreateExplosion(Vector3 objectPosition)
    {
        explosionTemp = Instantiate(explosion, objectPosition, Quaternion.identity);
        explosionTemp.GetComponent<AreaBehaviour>().layerList = this.layerList;
        explosionTemp.GetComponent<AreaBehaviour>().duration = 0;
        explosionTemp.GetComponent<AreaBehaviour>().AffectEnter = Explosion;
        explosionTemp.GetComponent<AreaBehaviour>().End = ExplosionEnd;
    }
    void Explosion(GameObject other)
    {
        prim.DamageAccordingToBaseArmor(other, damageRate);
    }
    void ExplosionEnd(GameObject obj, float duration)
    {
        StartCoroutine(ExplosionEndCoroutine(obj, duration));
    }
    IEnumerator ExplosionEndCoroutine(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(obj);
    }
}
