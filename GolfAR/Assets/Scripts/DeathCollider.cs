﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameManager.Instance.PlayerDeath();
    }

}
