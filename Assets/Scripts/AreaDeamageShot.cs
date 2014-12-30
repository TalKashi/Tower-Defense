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
    }

    void Start()
    {
        toysInRadius = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + " has entered!");
        if (other.tag == "Toy")
        {
            HealthBar health = other.GetComponentInChildren<HealthBar>();
            if (health.currentHP > 0 && !toysInRadius.Contains(other.gameObject))
            {
                toysInRadius.Add(other.gameObject);
                Debug.Log(other.name + " has been added to queue!");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name + " has exited!");
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
