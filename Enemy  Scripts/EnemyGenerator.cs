using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject lesser;
    [SerializeField] GameObject greater;

    float lesserKnightCooldown = 3f;
    float greaterKnightCooldown = 20f;

    IEnumerator LesserGenerator;
    IEnumerator GreaterGenerator;

    private void Update()
    {
        if (LesserGenerator == null)
        {
            LesserGenerator = LesserKnightGeneratorCoroutine();
            StartCoroutine(LesserGenerator);
        }
        if (GreaterGenerator == null)
        {
            GreaterGenerator = GreaterKnightGeneratorCoroutine();
            StartCoroutine(GreaterGenerator);
        }
    }

    IEnumerator LesserKnightGeneratorCoroutine()
    {

        while (true)
        {
            float x = RandomrWithTwoRanges(-30, -10, 10, 30);
            float z = RandomrWithTwoRanges(-30, -10, 10, 30);
            yield return new WaitForSeconds(lesserKnightCooldown);
            Instantiate(lesser, new Vector3(x, 0, z), Quaternion.LookRotation(new Vector3(0, 0, 0)));
            lesserKnightCooldown = lesserKnightCooldown <= 1f ? 1f : lesserKnightCooldown - 0.05f;
        }
    }
    IEnumerator GreaterKnightGeneratorCoroutine()
    {
        while (true)
        {
            float x = RandomrWithTwoRanges(-30, -10, 10, 30);
            float z = RandomrWithTwoRanges(-30, -10, 10, 30);
            yield return new WaitForSeconds(greaterKnightCooldown);
            Instantiate(greater, new Vector3(x, 0, z), Quaternion.LookRotation(new Vector3(0, 0, 0)));
            greaterKnightCooldown = greaterKnightCooldown <= 10f ? 10f : greaterKnightCooldown - 0.05f;
        }
    }
    float RandomrWithTwoRanges(float firstMin, float firstMax, float secondMin, float secondMax)
    {
        float number1 = Random.Range(firstMin, firstMax);
        float number2 = Random.Range(secondMin, secondMax);
        int numberPicker = Random.Range(1, 3);
        Debug.Log(numberPicker);
        if (numberPicker == 1)
        {
            return number1;
        }
        else
        {
            return number2;
        }
    }
}
