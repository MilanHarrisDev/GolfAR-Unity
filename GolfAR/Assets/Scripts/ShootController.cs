using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour {

    [SerializeField]
    private LayerMask shootMask;
    [SerializeField]
    private Transform ball;
    [SerializeField]
    private Transform arrowPivot;
    [SerializeField]
    private Image arrow;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            RotateArrowPivot(Input.mousePosition);
        else if (Input.touchCount > 0)
            RotateArrowPivot(Input.GetTouch(0).position);
    }

    void RotateArrowPivot(Vector3 pos){
        Debug.Log("rotating");

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100000f, shootMask))
        {
            arrowPivot.LookAt(ball.position + (GetXZVector(ball.position) - GetXZVector(hit.point)).normalized);
            arrowPivot.rotation = Quaternion.Euler(0, arrowPivot.eulerAngles.y + 90, 0);

            float fill = Vector3.Distance(GetXZVector(ball.position), GetXZVector(hit.point)) / 5f;
            arrow.fillAmount = (fill > 0.232f) ? fill : 0.232f;
        }
    }

    Vector3 GetXZVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
