using UnityEngine;
using System.Collections.Generic;

public class AreaDeamageShot : MonoBehaviour {

    public float Damage;
    public float Speed;
    public int MyRow;

    List<GameObject> toysInRadius;

    World world = World.WorldInstance;

    void Awake()
    {
        rigidbody2D.AddForce(Vector3.left * Speed);
        toysInRadius = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Toy")
        {
            HealthBar health = other.GetComponentInChildren<HealthBar>();
            if (health.currentHP > 0 && !toysInRadius.Contains(other.gameObject))
            {
                toysInRadius.Add(other.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Toy")
        {
            if(!toysInRadius.Remove(other.gameObject))
            {
                Debug.LogWarning("Tried to remove an object that wasn't in the list");
            }
        }
    }

    public List<GameObject> getToysInRadius()
    {
        return toysInRadius;
    }
}
