using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrimaryEffects : MonoBehaviour
{
    public void Kill(GameObject other)
    {
        Destroy(other);
    }
    public void Damage(GameObject other, float damage)
    {
        Unit sheet = other.GetComponent<Unit>();
        sheet.currentHp -= damage - sheet.currentArmor < 0 ? 0 : damage - sheet.currentArmor;

        ManageHealthBar(sheet);

        if (sheet.currentHp <= 0)
        {
            Kill(other);
            if(sheet is Player)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;
            }
        }
    }
    public void DamageAccordingToBaseArmor(GameObject other, float rate)
    {
        float armor = other.GetComponent<Unit>().baseArmor;
        Damage(other, armor * rate);
    }
    public void DamageAccordingToCurrentArmor(GameObject other, float rate)
    {
        float armor = other.GetComponent<Unit>().currentArmor;
        Damage(other, armor * rate);
    }

    public void TrueDamage(GameObject other, float damage)
    {
        Unit sheet = other.GetComponent<Unit>();
        sheet.currentHp -= damage;
        Debug.Log("Health: " + sheet.currentHp);
        if (sheet.currentHp <= 0)
        {
            Kill(other);
        }
    }
    public void SetMovementSpeed(GameObject other, float movementSpeed)
    {
        other.GetComponent<Unit>().currentMovementSpeed = movementSpeed;
    }
    public void RestoreMovementSpeed(GameObject other)
    {
        Unit statistics = other.GetComponent<Unit>();
        statistics.currentMovementSpeed = statistics.baseMovementSpeed;
    }
    public void AreaDamage(Vector3 point, float damage, float radius, List<int> layers)
    {
        Collider[] collidersOverlapped = new Collider[0];
        ApplyAreaDamage(collidersOverlapped, damage, point, radius, layers);
    }
    public void AreaTrueDamage(Vector3 point, float damage, float radius)
    {
        Collider[] collidersOverlapped = new Collider[0];
        ApplyAreaTrueDamage(collidersOverlapped, damage, point, radius);
    }
    public void AreaMaxHpDamage(Vector3 point, float ratio, float radius)
    {
        Collider[] collidersOverlapped = new Collider[0];
        ApplyAreaMaxHpDamage(collidersOverlapped, ratio, point, radius);
    }
    public void AreaDamageOverTime(Vector3 point, float damage, float duration, float frequency, float radius, List<int> layers )
    {
        StartCoroutine(ContuniousAreaDamageCoroutine(point, damage, duration, frequency, radius, layers));
    }
    public void AreaTrueDamageOverTime(Vector3 point, float damage, float duration, float frequency, float radius)
    {
        StartCoroutine(ContuniousTrueAreaDamageCoroutine(point, damage, duration, frequency, radius));
    }
    public void Execute(GameObject other, float limitHp)
    {
        if (other.GetComponent<Unit>().currentHp <= limitHp)
        {
            Kill(other);
        }
    }
    public void Heal(GameObject other, float heal)
    {
        Unit sheet = other.GetComponent<Unit>();
        sheet.currentHp += heal;
        if(sheet.currentHp > sheet.maxHp)
        {
            sheet.currentHp = sheet.maxHp;
        }
        ManageHealthBar(sheet);
    }
    public void IncreaseMaxHp(GameObject other, float increase)
    {
        Unit sheet = other.GetComponent<Unit>();
        sheet.maxHp += increase;
    }
    public void ChangeColor(GameObject other, Color color)
    {
        other.GetComponent<Renderer>().material.SetColor("_Color", color);
    }
    public void DamageMaxHp(GameObject other, float ratio)
    {

        float damage = other.GetComponent<Unit>().maxHp * ratio / 100;
        Damage(other, damage);
    }
    public void AddStack(GameObject other, Stack stack)
    {
        Unit sheet = other.GetComponent<Unit>();
        if (sheet.stackList.ContainsKey(stack.name))
        {
            stack = sheet.stackList[stack.name] as Stack;
            if (stack.amount != stack.amountLimit)
            {
                stack.amount++;
            }
        }
        else
        {
            sheet.stackList.Add(stack.name, stack);
        }
    }
    public void RemoveStack(GameObject other, Stack stack)
    {
        Unit sheet = other.GetComponent<Unit>();
        if (sheet.stackList.ContainsKey(stack.name))
        {
            stack = sheet.stackList[stack.name] as Stack;
            stack.amount--;
            Debug.Log("Death Mark" + stack.amount);
            if (stack.amount == 0)
            {
                sheet.stackList.Remove(stack.name);
            }
        }
    }
    public void RemoveAllStack(GameObject other, Stack stack)
    {
        other.GetComponent<Unit>().stackList.Remove(stack);
    }
    public float GetStackAmount(GameObject other, Stack stack)
    {
        Stack stackTemp = other.GetComponent<Unit>().stackList[stack.name] as Stack;
        return stackTemp.amount;
    }
    public void ReduceArmor(GameObject other, float amount)
    {
        float currentArmor = other.GetComponent<Unit>().currentArmor;
        other.GetComponent<Unit>().currentArmor -= currentArmor > amount ? amount : currentArmor;
    }
    public void IncreaseArmor(GameObject other, float amount)
    {
        other.GetComponent<Unit>().currentArmor += amount;
    }
    public void SetArmor(GameObject other, float amount)
    {
        other.GetComponent<Unit>().currentArmor = amount;
    }
    public void RestoreArmor(GameObject other)
    {
        other.GetComponent<Unit>().currentArmor = other.GetComponent<Unit>().baseArmor;
    }
    void ApplyAreaDamage(Collider[] collidersOverlapped, float damage, Vector3 point, float radius, List<int> layers)
    {
        collidersOverlapped = Physics.OverlapSphere(point, radius);
        foreach (Collider col in collidersOverlapped)
        {
            GameObject objectCollided = col.gameObject;
            if (layers.Contains(objectCollided.layer))
            {
                Damage(objectCollided, damage);
            }
        }
    }
    void ApplyAreaMaxHpDamage(Collider[] collidersOverlapped, float ratio, Vector3 point, float radius)
    {
        collidersOverlapped = Physics.OverlapSphere(point, radius);
        foreach (Collider col in collidersOverlapped)
        {
            GameObject objectCollided = col.gameObject;
            if (objectCollided.layer == 3)
            {
                DamageMaxHp(objectCollided, ratio);
            }
        }
    }
    void ApplyAreaTrueDamage(Collider[] collidersOverlapped, float ratio, Vector3 point, float radius)
    {
        collidersOverlapped = Physics.OverlapSphere(point, radius);
        foreach (Collider col in collidersOverlapped)
        {
            GameObject objectCollided = col.gameObject;
            if (objectCollided.layer == 3)
            {
                TrueDamage(objectCollided, ratio);
            }
        }
    }
    IEnumerator ContuniousAreaDamageCoroutine(Vector3 point, float damage, float duration, float frequency, float radius, List<int> layers)
    {
        float amount = duration / frequency;
        Collider[] collidersOverlapped = new Collider[0];
        while (amount > 0)
        {
            yield return new WaitForSeconds(frequency);
            amount--;
            ApplyAreaDamage(collidersOverlapped, damage, point, radius, layers);
        }
    }
    IEnumerator ContuniousTrueAreaDamageCoroutine(Vector3 point, float damage, float duration, float frequency, float radius)
    {
        float amount = duration / frequency;
        Collider[] collidersOverlapped = new Collider[0];
        while (amount > 0)
        {
            yield return new WaitForSeconds(frequency);
            amount--;
            ApplyAreaTrueDamage(collidersOverlapped, damage, point, radius);
        }
    }
    void ManageHealthBar(Unit sheet)
    {
        if (sheet is Player)
        {
            Player playerSheet = sheet as Player;
            playerSheet.healthBar.SetValue(sheet.currentHp);
        }
        else if (sheet is LesserKnight)
        {
            LesserKnight playerSheet = sheet as LesserKnight;
            playerSheet.healthBar.SetValue(sheet.currentHp);
        }
        else if (sheet is GreaterKnight)
        {
            GreaterKnight playerSheet = sheet as GreaterKnight;
            playerSheet.healthBar.SetValue(sheet.currentHp);
        }
    }
}
