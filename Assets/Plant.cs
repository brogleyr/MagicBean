using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    bool isMagicBean;
    int beanCount = 0;
    
    void Start() {
        isMagicBean = GrowthManager.Instance.GetMagicBean();

        if (isMagicBean) {
            // Instantiate a magic bean as a child
        }
        else {
            beanCount = GrowthManager.Instance.GetBeanCount();
            //Instantiate
            
        }
    }

    void ActivateBean() {
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
