using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Rigidbody>().velocity = Vector3.Lerp(other.GetComponent<Rigidbody>().velocity, Vector3.zero, 4f * Time.deltaTime);
    }
}
