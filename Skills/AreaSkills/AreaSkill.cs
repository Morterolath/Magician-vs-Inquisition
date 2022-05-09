using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// There are 4 types of area skills: Constant, Enter-Oriented, Sinlgly, Repeating
/// Constant:           Affects throughout the time while object is in the area
/// Enter-Oriented:     Affects objects upon entering the area
/// Singly:             Affects objects once upon being used
/// Repeating:          Affects objects frequently, over time.
/// An area skill can be more than one of these.
/// </summary>
public class AreaSkill : Skill
{
    public float duration;
    public GameObject area;
    GameObject areaTemp;
    public IEnumerator currentCast;
    public float latency = 0.425f;

    public override void Use(Vector3 start, Vector3 target)
    {
        if (IsInRange(range,start,target))
        {
            ApplyOnCheck(target);
            if (IsConstant() || IsEnterOriented())
            {
                CreateArea(target);
            }
        }
    }
    public virtual void ApplyOnEnter(GameObject other) { }      //Effect method which will be applied upon entering the area for Constant or Enter-Oriented area skills
    public virtual void ApplyOnExit(GameObject other) { }   //Effect method which will be applied upon exiting the area for Constant or Enter-Oriented area skills
    public virtual void ApplyOnStay(GameObject other) { }          
    public virtual void ApplyOnCheck(Vector3 point) { }     //Effect method for Singly or Repeating area skills
    public virtual void End(GameObject obj, float duration) { } //End Condition for skill
    public IEnumerator SkillFinisher(GameObject obj, float duration)    //End counter for Constant or Enter-Oriented skills
    {
        yield return new WaitForSeconds(duration);
        if (obj != null)    //There are some odd stuff. Gives me nullexception when I dont check if obj is null before changing the position of it.
        {
            obj.transform.position = new Vector3(100000, 0, 0);    //This first sends area to the location in order to apply TriggerOnExit method
            yield return new WaitForSeconds(1f);
            Destroy(obj);
        }
    }       


    void CreateArea(Vector3 target) //Hurtbox creation for Constant or Enter-Oriented area skills
    {
        if (currentCast == null)
        {
            currentCast = CreateAreaWithLatency(target);
            StartCoroutine(currentCast);
        }
    }   
    bool IsInRange(float range, Vector3 start, Vector3 target)  //Determines if point is in range or not
    {
        return range >= GetDistance(start,target) ? true : false;
    }   
    float GetDistance(Vector3 start, Vector3 target)    //Calculates distance between player and point clicked
    {
        return (target - start).magnitude;
    }       
    bool IsConstant()
    {
        return area != null ? true : false;
    }
    bool IsEnterOriented()
    {
        return area != null ? true : false;
    }
    bool IsSingly()
    {
        return area == null ? true : false;
    }
    bool IsRepeating()
    {
        return area == null ? true : false;
    }
    IEnumerator CreateAreaWithLatency(Vector3 target)
    {
        yield return new WaitForSeconds(latency);
        areaTemp = Instantiate(this.area, target, Quaternion.identity);
        areaTemp.GetComponent<AreaBehaviour>().layerList = this.layerList;
        areaTemp.GetComponent<AreaBehaviour>().duration = this.duration;
        areaTemp.GetComponent<AreaBehaviour>().AffectEnter = ApplyOnEnter;
        areaTemp.GetComponent<AreaBehaviour>().AffectExit = ApplyOnExit;
        areaTemp.GetComponent<AreaBehaviour>().AffectStay = ApplyOnStay;
        areaTemp.GetComponent<AreaBehaviour>().End = End;
        currentCast = null;
    }
}
