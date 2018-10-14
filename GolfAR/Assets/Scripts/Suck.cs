using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position).normalized * 5000f * Time.deltaTime);
        }
    }
}
