using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ActivateBean() {
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
