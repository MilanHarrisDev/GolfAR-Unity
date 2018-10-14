using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{

    [SerializeField]
    private LayerMask shootMask;
    [SerializeField]
    private Transform ball;
    [SerializeField]
    private Transform arrowPivot;
    [SerializeField]
    private Image arrow;

    public bool canShoot = false;

    [SerializeField]
    private GameObject pullbackInterface;

    [SerializeField]
    private Transform imageTarget;

    private Rigidbody rb;

    private Vector3 shootVector = Vector3.zero;
    private float shootSpeed = 0;

    private Vector3 lastVelocity = Vector3.zero;

    public bool Moving { get { return moving; } }
    private bool moving = false;

    private void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Debug.Log("velocity: " + rb.velocity + ", magnitude: " + rb.velocity.magnitude);

        Physics.gravity = -imageTarget.up * 9.8f;

        if (Input.GetMouseButton(0))
            RotateArrowPivot(Input.mousePosition);
        else if (Input.touchCount > 0)
            RotateArrowPivot(Input.GetTouch(0).position);


        if (rb.velocity != lastVelocity)//change in velocity
        {
            if (lastVelocity == Vector3.zero)
            {
                moving = true;
                pullbackInterface.SetActive(false);
                canShoot = false;
                Debug.Log("Ball started moving");
            }

            if (rb.velocity.magnitude < 0.05f)
            {
                moving = false;
                GameManager.Instance.EndTurn();
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;

                Debug.Log("ball stopped");
            }
        }

        lastVelocity = rb.velocity;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = shootVector * shootSpeed;
        }
    }

    void RotateArrowPivot(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100000f, shootMask))
        {
            if (hit.transform.tag == "ShootPanel")
            {
                arrowPivot.LookAt(ball.position + (GetXZVector(ball.position) - GetXZVector(hit.point)).normalized);
                arrowPivot.rotation = Quaternion.Euler(0, arrowPivot.eulerAngles.y + 90, 0);

                float fill = Vector3.Distance(GetXZVector(ball.position), GetXZVector(hit.point)) / 5f;
                arrow.fillAmount = fill;

                shootVector = (GetXZVector(ball.position) - GetXZVector(hit.point)).normalized;
                shootSpeed = fill * 20f;
            }
        }

    }

    Vector3 GetXZVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }

    public void ResetShoot()
    {
        canShoot = true;
        pullbackInterface.SetActive(true);
        pullbackInterface.transform.position = ball.transform.position;
    }
}
