using UnityEngine;
using System.Collections;

public class RifleShot : MonoBehaviour
{
    public float Damage;
    public float Speed;

    public int MyRow;

    World world = World.WorldInstance;

	void Start ()
    {
        rigidbody2D.AddForce(Vector3.left * Speed);
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Toy")
        {
            HealthBar health = other.GetComponentInChildren<HealthBar>();
            if (health.currentHP > 0)
            {
                if (health.TakeDamage(Damage))
                {
                    world.OnToyDied(other.name, MyRow);
                    other.SendMessage("Fade", other.GetComponent<SpriteRenderer>());
                }
                Destroy(this.gameObject);
            }
        }
    }
}
