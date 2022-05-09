using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public delegate void AffectDel(GameObject go);
    public AffectDel Affect;
    public delegate void MovementDel(GameObject obj, Vector3 start, Vector3 target, float speed);
    public MovementDel Movemet;
    public delegate void EndDel(GameObject proj, Vector3 position, Vector3 target);
    public EndDel End;

    
    public float collisionCounter = 0;
    public Vector3 target;
    public List<int> layerList;
    public float speed;
    public float range;


    void Update()
    {
        Movemet(this.gameObject, transform.position, target, this.speed * Time.deltaTime);
        End(this.gameObject, transform.position, target);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (layerList.Contains(other.gameObject.layer))
        {
            Affect(other.gameObject);
            collisionCounter++;
        }
    }
}
