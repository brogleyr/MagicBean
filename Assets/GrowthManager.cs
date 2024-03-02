using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public static GrowthManager Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    public bool GetMagicBean() {
        return false;
    }

    public int GetBeanCount() {
        return 1;
    }
}
