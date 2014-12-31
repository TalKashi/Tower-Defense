using UnityEngine;
using System.Collections.Generic;

public class Toy : MonoBehaviour {

    public float HitPower = 10f;
    public float Speed = 0.05f;
    public int MyRow = 0;

    World s_World = World.WorldInstance;
    Animator animator;
    Queue<GameObject> soldiersQueue;
    GameObject currentSoldier;
    HealthBar soldierHealthBar;
    bool walking, hitting, dying;
    Vector2 right = new Vector2(1.0f, 0.0f);

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        soldiersQueue = new Queue<GameObject>();
        walking = true;
        hitting = false;
        dying = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleAnimation();
        if (dying)
        {
            return;
        }
        if (walking)
        {
            transform.position = Vector2.Lerp(transform.position, ((Vector2)transform.position) + right, Speed);
        }
        else if (hitting)
        {
            if (soldierHealthBar.TakeDamage(HitPower * Time.deltaTime))
            {
                s_World.OnSoldierDied(currentSoldier.name, -1);
                currentSoldier.SendMessage("Fade", currentSoldier.GetComponent<SpriteRenderer>());
                if (soldiersQueue.Count > 0)
                {
                    engageEnemy(soldiersQueue.Dequeue());
                }
                else
                {
                    currentSoldier = null;
                    walking = true;
                    hitting = false;
                }
            }
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Soldier")
        {

            if (soldiersQueue.Count == 0)
            {
                engageEnemy(other.gameObject);
                hitting = true;
                walking = false;
            }
            else
            {
                soldiersQueue.Enqueue(other.gameObject);
            }
        }
    }

    private void HandleAnimation()
    {
        animator.SetBool("fighting", hitting);
    }

    void engageEnemy(GameObject newSoldier)
    {
        currentSoldier = newSoldier;
        soldierHealthBar = currentSoldier.GetComponentInChildren<HealthBar>();
        //walking = false;
        //hitting = true;
        //animator.SetBool("fight", true);
    }

    void SetDyingState()
    {
        dying = true;
    }
}
