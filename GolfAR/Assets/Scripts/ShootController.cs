using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

    [SerializeField]
    private LayerMask shootMask;
    [SerializeField]
    private Transform ball;
    [SerializeField]
    private Transform arrowPivot;

    void CastRayToWorld(){

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100000f, shootMask))
        {
            arrowPivot.LookAt(ball.position + (GetXZVector(ball.position) - GetXZVector(hit.point)).normalized);
            arrowPivot.rotation = Quaternion.Euler(0, arrowPivot.eulerAngles.y, 0);
        }
    }

    Vector3 GetXZVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
