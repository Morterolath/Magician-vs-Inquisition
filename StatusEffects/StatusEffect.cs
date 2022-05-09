using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public string name;
    public PrimaryEffects prim;
    public float duration;
    public float amount;
    public float frequency;

    public void TrueDot(GameObject other, string name, float damage, float duration, float frequency)
    {
        Hashtable ongoings = other.GetComponent<Unit>().effectList;

        this.duration = duration;
        if (!ongoings.ContainsKey(name))
        {
            amount = this.duration / frequency;
            ongoings.Add(name, CTrueDamageOverTime(other, damage, amount, frequency, ongoings));
            StartCoroutine(CTrueDamageOverTime(other, damage, amount, frequency, ongoings));
        }
        else
        {
            this.duration = duration;
            amount = this.duration / frequency;
        }
    }
    public void Dot(GameObject other, string name, float damage, float duration, float frequency)
    {
        Hashtable ongoings = other.GetComponent<Unit>().effectList;

        this.duration = duration;
        if (!ongoings.ContainsKey(name))
        {
            amount = this.duration / frequency;
            ongoings.Add(name, CDamageOverTime(other, damage, amount, frequency, ongoings));
            StartCoroutine(CDamageOverTime(other, damage, amount, frequency, ongoings));
        }
        else
        {
            this.duration = duration;
            amount = this.duration / frequency;
        }
    }
    IEnumerator CTrueDamageOverTime(GameObject other, float damage, float amount, float frequency, Hashtable ongoings)
    {
        prim = GetComponent<PrimaryEffects>();
        while (amount != 0)
        {
            yield return new WaitForSeconds(frequency);
            amount--;
            if (other != null)
            {
                prim.TrueDamage(other, damage);
            }
        }
        ongoings.Remove(name);
    }
    IEnumerator CDamageOverTime(GameObject other, float damage, float amount, float frequency, Hashtable ongoings)
    {
        prim = GetComponent<PrimaryEffects>();
        while (amount != 0)
        {
            yield return new WaitForSeconds(frequency);
            amount--;
            if (other != null)
            {
                prim.Damage(other, damage);
            }
        }
        ongoings.Remove(name);
    }
}
