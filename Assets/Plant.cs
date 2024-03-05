using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    [SerializeField]
    bool isMagicBean = false;
    int beanCount;

    [SerializeField]
    GameObject beanPrefab, magicBeanPrefab;
    Vector3 beanDisplacementCenter = new Vector3(0f, 0f ,0f);
    float beanDisplacementRadius = 0.25f;
    
    void Start() {
        if (!isMagicBean) {
            isMagicBean = GrowthManager.Instance.GetMagicBean();
        }
        GameObject newBean;
        if (isMagicBean) {
            // Instantiate a magic bean as a child
            newBean = Instantiate(magicBeanPrefab, transform.position + beanDisplacementCenter, Quaternion.identity, transform);
            newBean.SetActive(false);
        }
        else {
            beanCount = GrowthManager.Instance.GetBeanCount();
            //Instantiate
            for (int i = 0; i < beanCount; i++) {
                float dist = Random.Range(0f, beanDisplacementRadius);
                float angle = Random.Range(0f, 360f);
                Vector3 wiggle = Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(dist, 0f, 0f);
                newBean = Instantiate(beanPrefab, transform.position + beanDisplacementCenter + wiggle, Quaternion.identity, transform);
                newBean.SetActive(false);
            }
        }
    }

    void ActivateBean() {
        for (int i = 1; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
    }

    public void DestroyBean(GameObject bean) {
        Destroy(bean);
    }

    void Update() {
        if (transform.childCount <= 1) {
            Destroy(gameObject);
        }
    }
}
