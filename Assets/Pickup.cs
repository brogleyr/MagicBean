using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField]
    Bank bank;


    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            // TODO: Add bean to bank
            Bank.Instance.AddBean();

            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
