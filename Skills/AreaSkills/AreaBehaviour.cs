using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBehaviour : MonoBehaviour
{
    public delegate void AffectEnterDel(GameObject go);
    public AffectEnterDel AffectEnter;
    public delegate void AffectExitDel(GameObject go);
    public AffectExitDel AffectExit;
    public delegate void AffectStayDel(GameObject go);
    public AffectExitDel AffectStay;
    public delegate void EndDel(GameObject other, float time);
    public EndDel End;

    public List<int> layerList;
    public float duration;

    private void Update()
    {
        End(this.gameObject, duration+0.05f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (layerList.Contains(other.gameObject.layer))
        {
            AffectEnter(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (layerList.Contains(other.gameObject.layer))
        {
            AffectExit(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (layerList.Contains(other.gameObject.layer) && AffectStay!=null)
        {
            AffectStay(other.gameObject);
        }
    }
}
