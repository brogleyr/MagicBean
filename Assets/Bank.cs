using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; private set; }

    [SerializeField]
    TextMeshProUGUI beanCountGUI, magicBeanCounterGUI;

    public int Beans { get; private set; } = 0;
    public int MagicBeans { get; private set; } = 0;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    public void AddBean() {
        Beans += 1;
        beanCountGUI.text = Beans.ToString();
    }

    public void AddMagicBean() {
        MagicBeans += 1;
        magicBeanCounterGUI.text = MagicBeans.ToString();
    }


}
