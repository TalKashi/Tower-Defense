using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float startHP = 100;
    public float currentHP;
    public bool isToy = false;

    int row = -1;
    bool hasAnnouncedDead = false;
    

    Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        currentHP = startHP;
    }

    void Update()
    {
        if (isToy && currentHP <= 0 && !hasAnnouncedDead)
        {
            World.WorldInstance.OnToyDied(this.transform.parent.parent.gameObject, row);
            hasAnnouncedDead = true;
        }
    }

    public bool TakeDamage(float amount)
    {
        currentHP -= amount;
        image.fillAmount = currentHP / startHP;
        return currentHP <= 0f;
    }

    public void HealToMax()
    {
        currentHP = startHP;
        image.fillAmount = 1;
    }
}
