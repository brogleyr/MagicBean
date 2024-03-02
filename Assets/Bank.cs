using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; private set; }

    [SerializeField]
    TextMeshProUGUI beanCountGUI;

    public int Beans { get; private set; } = 0;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    public void AddBean() {
        Beans += 1;
        Debug.Log(Beans);
        beanCountGUI.text = Beans.ToString();
    }


}
