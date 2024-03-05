using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public static GrowthManager Instance { get; private set; }

    [SerializeField]
    float meanBeans = 1.0f, sigmaBeans = 0.25f, magicChance = 0.01f;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    public bool GetMagicBean() {
        if (Random.value < magicChance) {
            return true;
        }
        return false;
    }

    public int GetBeanCount() {
        int roundedBeanCount = (int) Mathf.Round(GenerateGaussian(meanBeans, sigmaBeans));
        return Mathf.Max(roundedBeanCount, 1);
    }

    public static float GenerateGaussian(float mean, float stdDev) {
        float u, v, s;
        do {
            u = Random.value * 2 - 1;
            v = Random.value * 2 - 1;
            s = u * u + v * v;
        } while (s >= 1 || s == 0);
        s = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
        return mean + stdDev * u * s;
    }
}
