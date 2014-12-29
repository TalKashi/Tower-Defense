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
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Toy")
            return;

        HealthBar health = other.GetComponentInChildren<HealthBar>();
        if (health.currentHP > 0)
        {
            foreach (GameObject toy in toysInRange)
            {
                if(toy != null)
                {
                    HealthBar currentHealthBar = toy.GetComponentInChildren<HealthBar>();
                    if (currentHealthBar.currentHP > 0 && currentHealthBar.TakeDamage(Damage))
                    {
                        world.OnToyDied(toy.name, MyRow);
                        toy.SendMessage("Fade", toy.GetComponent<SpriteRenderer>());
                    }
                }
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
