using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Skill[] skills;
    [SerializeField] Animator myAnimator;

    Vector3 target;
    IEnumerator currentAction;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsCharReady())
        {
            CastWithCooldown(0);
        }
        else if (Input.GetKeyDown(KeyCode.W) && IsCharReady())
        {
            CastWithCooldown(1);
        }
        else if (Input.GetKeyDown(KeyCode.E) && IsCharReady())
        {
            CastWithCooldown(2);
        }
        else if (Input.GetKeyDown(KeyCode.A) && IsCharReady())
        {
            CastWithCooldown(3);
        }
        else if (Input.GetKeyDown(KeyCode.S) && IsCharReady())
        {
            CastWithCooldown(4);
        }
        else if (Input.GetKeyDown(KeyCode.D) && IsCharReady())
        {
            CastWithCooldown(5);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && IsCharReady())
        {
            CastWithCooldown(6);
        }
        else if (Input.GetKeyDown(KeyCode.X) && IsCharReady())
        {
            CastWithCooldown(7);
        }
        else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            gameObject.GetComponent<Player>().maxHp = gameObject.GetComponent<Player>().maxHp == 50f ? 30000f : 50f;
            gameObject.GetComponent<Player>().currentHp = gameObject.GetComponent<Player>().maxHp;
        }
    }
    void UseSkill(int skillNumber)
    {
        GetMousePosition();
        if (skills[skillNumber] is SkillShot || skills[skillNumber] is AreaSkill)
        {
            skills[skillNumber].Use(transform.position, this.target);
        }
    }
    void GetMousePosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, 3))
        {
            this.target = hit.point;
            this.target.y = transform.position.y;
        }
    }
    bool IsCharReady()
    {
        return currentAction == null ? true : false;
    }
    void CastWithCooldown(int index)
    {
        currentAction = CooldownCoroutine(index);
        StartCoroutine(currentAction);
    }
    IEnumerator CooldownCoroutine(int index)
    {
        float cooldown = myAnimator.GetCurrentAnimatorStateInfo(0).length;
        myAnimator.SetTrigger("myTrig");
        UseSkill(index);
        yield return new WaitForSeconds(cooldown);
        currentAction = null;
    }
}
