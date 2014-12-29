using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float startHP = 100;
    public float currentHP;

    Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        currentHP = startHP;
    }

    public bool TakeDamage(float amount)
    {
        currentHP -= amount;
        image.fillAmount = currentHP / startHP;
        return currentHP <= 0f;
    }
}
