using UnityEngine;
using System.Collections.Generic;

public class Explotion : MonoBehaviour {

    float Damage;
    float Speed;
    int MyRow;

    List<GameObject> toysInRange;
    World world = World.WorldInstance;

	// Use this for initialization
	void Awake () {
        AreaDeamageShot parentScript = GetComponentInParent<AreaDeamageShot>();
        this.Damage = parentScript.Damage;
        this.Speed = parentScript.Speed;
        this.MyRow = parentScript.MyRow;
        toysInRange = parentScript.getToysInRadius();
        rigidbody2D.AddForce(Vector3.left * Speed);
	}

    void Update()
    {
        if (toysInRange == null)
        {
            AreaDeamageShot parentScript = GetComponentInParent<AreaDeamageShot>();
            toysInRange = parentScript.getToysInRadius();
        }
    }
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Toy")
            return;

        HealthBar health = other.GetComponentInChildren<HealthBar>();
        if (health.currentHP > 0)
        {
            if (toysInRange == null)
            {
                ApplyDamage(other.gameObject);
            }
            else
            {
                foreach (GameObject toy in toysInRange)
                {
                    if (toy != null)
                    {
                        ApplyDamage(toy);
                    }
                }
            }
            Destroy(transform.parent.gameObject);
        }
    }

    void ApplyDamage(GameObject toy)
    {
        HealthBar health = toy.GetComponentInChildren<HealthBar>();
        if (health.currentHP > 0 && health.TakeDamage(Damage))
        {
            //world.OnToyDied(toy, MyRow);
            toy.SendMessage("Fade", toy.GetComponent<SpriteRenderer>());
        }
    }
}
