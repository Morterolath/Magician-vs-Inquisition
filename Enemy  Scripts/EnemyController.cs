using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PrimaryEffects prim;
    [SerializeField] Animator anim;
   
    Unit unitStatistics;
    IEnumerator currentAttack;      //Coroutine for ongoing attack
    Vector3 target;                 //Position of the player

    private void Start()
    {
        target = player.transform.position;
        unitStatistics = this.gameObject.GetComponent<Unit>();
    }
    private void Update()
    {
        transform.LookAt(target);
        MoveEnemy();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7)        //7 is the layer  of player
        {
            StopUnitUponCollision();
            anim.SetBool("IsToAttack", true);
            AttackWithDelay(other.gameObject);
        }

        void StopUnitUponCollision()
        {
            target = transform.position;
        }
    }
    void AttackWithDelay(GameObject other)
    {
        if (currentAttack == null)
        {
            currentAttack = AttackWithDelayCoroutine(other);
            StartCoroutine(currentAttack);
        }

        IEnumerator AttackWithDelayCoroutine(GameObject other)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                prim.Damage(other, 1f);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, unitStatistics.currentMovementSpeed * Time.deltaTime);
    }

}
