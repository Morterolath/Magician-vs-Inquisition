using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SkillShot
{
    public GameObject explosion;
    public float damage;
    public float explosionDamage;
    GameObject explosionTemp;
    List<int> explosionLayerList = new List<int>();

    private void Start()
    {
        layerList.Add(3);

        explosionLayerList.Add(3);
        explosionLayerList.Add(7);

        damage = 1f;
        explosionDamage = 7f;
        speed = 7f;
        range = 25f;
        isTargetFixed = false;
    }
    public override void Apply(GameObject other)
    {
        prim.Damage(other, damage);
    }
    public override void End(GameObject proj, Vector3 objectPosition, Vector3 targetPosition)
    {
        if (proj.GetComponent<ProjectileBehaviour>().collisionCounter >= 1 || objectPosition == targetPosition)
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
        explosionTemp.GetComponent<AreaBehaviour>().layerList = explosionLayerList;
        explosionTemp.GetComponent<AreaBehaviour>().duration = 0;
        explosionTemp.GetComponent<AreaBehaviour>().AffectEnter = Explosion;
        explosionTemp.GetComponent<AreaBehaviour>().End = ExplosionEnd;
    }
    void Explosion(GameObject other)
    {
        prim.Damage(other, explosionDamage);
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
