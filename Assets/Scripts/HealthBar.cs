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
    bool isAtomActivated = false;
    bool notRegisteredToNukeEvent = false;
    
    World world = World.WorldInstance;
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
            world.OnToyDied(this.transform.parent.parent.gameObject, row);
            hasAnnouncedDead = true;
            this.transform.parent.parent.gameObject.SendMessage("Fade", 
                this.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>());
        }
        if (isToy && !notRegisteredToNukeEvent)
        {
            world.NukeActivated += AtomListener;
            notRegisteredToNukeEvent = true;
        }
        if (isAtomActivated)
        {
            currentHP = 0;
            image.fillAmount = currentHP / startHP;
        }
    }

    void AtomListener()
    {
        isAtomActivated = true;
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
