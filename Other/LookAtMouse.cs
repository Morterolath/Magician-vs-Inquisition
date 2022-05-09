using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Vector3 lookAt;
    private void FixedUpdate()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, 3))
        {
            lookAt = hit.point;
            lookAt.y = transform.position.y;
            this.gameObject.transform.LookAt(lookAt);
        }
    }
}
