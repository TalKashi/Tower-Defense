using UnityEngine;
using System.Collections.Generic;

public class GrenadeScript : MonoBehaviour {

    public float TimeTillExplode = 5f;
    public float JourneyTime = 3f;
    public int RotationSpeed = 20;
    public float Offset = 2;

    private float startTime;

    float Damage;
    float Speed;
    int MyRow;    
    bool hasExploded = false;

    Vector3 destination;
    Vector3 source;
    Animator anim;
    List<GameObject> toysInRange;
    World world = World.WorldInstance;

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        this.destination.x += Offset;
    }

    void Awake()
    {
        AreaDeamageShot parentScript = GetComponent<AreaDeamageShot>();
        this.Damage = parentScript.Damage;
        this.Speed = parentScript.Speed;
        this.MyRow = parentScript.MyRow;
        toysInRange = parentScript.getToysInRadius();
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        source = transform.position;
        startTime = Time.time;
	}

    void Update()
    {
        if (toysInRange == null)
        {
            AreaDeamageShot parentScript = GetComponent<AreaDeamageShot>();
            toysInRange = parentScript.getToysInRadius();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time - startTime >= TimeTillExplode)
        {
            Explode();
            return;
        }
        
        transform.Rotate(Vector3.forward, RotationSpeed);
        Vector3 center = (source + destination) * 1.5f;
        center -= new Vector3(0, 1f, 0);
        Vector3 meRelCenter = source - center;
        Vector3 destRelCenter = destination - center;
        float fracComplete = (Time.time - startTime) / JourneyTime;
        transform.position = Vector3.Slerp(meRelCenter, destRelCenter, fracComplete);
        transform.position += center;
	}

    void Explode()
    {
        if (hasExploded)
            return;

        anim.SetTrigger("explode");

        
        foreach (GameObject toy in toysInRange)
        {
            if (toy != null)
            {
                HealthBar currentHealthBar = toy.GetComponentInChildren<HealthBar>();
                if (currentHealthBar.currentHP > 0 && currentHealthBar.TakeDamage(Damage))
                {
                    world.OnToyDied(toy, MyRow);
                    toy.SendMessage("Fade", toy.GetComponent<SpriteRenderer>());
                }
            }
        }

        hasExploded = true;
    }

    void DestoryOnAnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
