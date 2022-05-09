using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShot : Skill
{
    public GameObject proj;
    public bool isTargetFixed;      //Whether object goes to the exact point, or through the direction
    public float speed;             //Speed of the shot
    public GameObject projTemp;            //Referance for the launched object
    IEnumerator curretnCast;
    float latency = 0.425f;

    public override void Use(Vector3 start, Vector3 end)    //Instantiates the projectile and moves it accordingly. All other methods are called in this method.
    {
        Vector3 instantiationPoint = CalculateStart(start, end);
        end = CalculateEnd(start, end, range);
        CreateProj(instantiationPoint, end);
    }
    public virtual void Movement(GameObject obj, Vector3 start, Vector3 target, float speed)
    {
        obj.transform.position = Vector3.MoveTowards(start, target, speed);
    }
    public virtual void Apply(GameObject other) { }   //Affects the objects collided
    public virtual void End(GameObject proj, Vector3 position, Vector3 target) { }  //Destroys projectile if the condition is met
    void CreateProj(Vector3 start, Vector3 target)  //Launches projectile
    {
        if (curretnCast == null)
        {
            curretnCast = CreateProjWithLatency(start, target);
            StartCoroutine(curretnCast);
        }
    }
    Vector3 CalculateEnd(Vector3 start, Vector3 end, float r)
    {
        if (!isTargetFixed && GetDistance(start, end) <= r)
        {
            end.y = start.y;
            return end;
        }

        return CalculateNormalizedRange(start, end, r);
    }
    Vector3 CalculateStart(Vector3 start, Vector3 end)
    {
        end.y = start.y;
        Vector3 newStart = end - start;
        newStart.Normalize();
        newStart *= 2.5f;
        newStart += start;
        return newStart;
    }
    Vector3 CalculateNormalizedRange(Vector3 start, Vector3 end, float r)
    {
        end.y = start.y;
        Vector3 newEnd = end - start;
        newEnd.Normalize();
        newEnd *= r;
        newEnd += start;
        return newEnd;
    }
    float GetDistance(Vector3 start, Vector3 end)   //Calculates point to be travelled
    {
        return (end - start).magnitude;
    }   
    IEnumerator CreateProjWithLatency(Vector3 start, Vector3 target)
    {
        yield return new WaitForSeconds(latency);
        projTemp = Instantiate(this.proj, start, Quaternion.LookRotation(target));
        projTemp.GetComponent<ProjectileBehaviour>().speed = speed;
        if (!isTargetFixed && Vector3.Distance(transform.position, start) > Vector3.Distance(transform.position, target))
        {
            target = start;
        }
        projTemp.GetComponent<ProjectileBehaviour>().target = target;
        projTemp.GetComponent<ProjectileBehaviour>().range = this.range;
        projTemp.GetComponent<ProjectileBehaviour>().layerList = this.layerList;
        projTemp.GetComponent<ProjectileBehaviour>().Affect = Apply;
        projTemp.GetComponent<ProjectileBehaviour>().End = this.End;
        projTemp.GetComponent<ProjectileBehaviour>().Movemet += this.Movement;
        curretnCast = null;
    }
    
}