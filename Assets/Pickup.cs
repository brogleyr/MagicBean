using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    enum PickupType {
        BEAN, MAGICBEAN
    }

    [SerializeField]
    PickupType type;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            // TODO: Add bean to bank
            if (type == PickupType.BEAN) {
                PickupBean();
            }
            else if (type == PickupType.MAGICBEAN) {
                PickupMagicBean();
            }
            
        }
    }

    void PickupBean() {
        Bank.Instance.AddBean();
        transform.parent.gameObject.GetComponent<Plant>().DestroyBean(gameObject);
    }

    void PickupMagicBean() {
        Bank.Instance.AddMagicBean();
        transform.parent.gameObject.GetComponent<Plant>().DestroyBean(gameObject);
    }
}
