using UnityEngine;
using System.Collections.Generic;

public class Soldier : MonoBehaviour {

    public GameObject Bullet;
    public int MyRow;
    public float TimeBetweenShots;

    private Object MyLock = new Object();

    GameObject currentToy;
    Animator anim;
    Transform burstPoint;
    World world = World.WorldInstance;
    LinkedList<GameObject> toysQueue;
    int toysCount;
    float timeOfLastShot = 0;
    bool dying;

    void Start()
    {
        toysCount = 0;
        burstPoint = transform.FindChild("BurstPoint");
        world.ToyDied += ToyDiedListener;
        toysQueue = new LinkedList<GameObject>();
        dying = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (dying)
            return;

        if (toysQueue.Count > 0)
        {
            shoot();
        }

    }

    void shoot()
    {
        if (currentToy == null)
        {
            UpdateCurrentToy();
            return;
        }

        if (Time.time - timeOfLastShot >= TimeBetweenShots)
        {
            anim.SetBool("shoot", true);
            GameObject clone = (GameObject) Instantiate(Bullet, new Vector3(burstPoint.position.x, burstPoint.position.y, 0), Quaternion.identity);
            if(gameObject.name == "Shotgun_Soldier")
                clone.GetComponent<RifleShot>().MyRow = MyRow;
            else
                clone.GetComponent<AreaDeamageShot>().MyRow = MyRow;
            if (gameObject.name == "Grenade_Soldier")
                clone.GetComponent<GrenadeScript>().SetDestination(currentToy.transform.position);
            timeOfLastShot = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        lock (MyLock)
        {
            Debug.Log(other.name + " entered soldier range");
            if (other.tag == "Toy")
            {
                if (toysQueue.Count == 0)
                {
                    currentToy = other.gameObject;
                }
                if (!toysQueue.Contains(other.gameObject))
                {
                    toysCount++;
                    toysQueue.AddLast(other.gameObject);
                }
            }
        }
    }

    void ToyDiedListener(GameObject TypeOfToy, int row)
    {
        lock (MyLock)
        {
            if (MyRow == row)
            {
                if (toysQueue.Count > 1)
                {
                    if (currentToy == TypeOfToy)
                    {
                        currentToy = toysQueue.First.Next.Value;
                        //toysQueue.Remove(TypeOfToy);
                    }

                }
                else
                {
                    currentToy = null;
                }
                if (!toysQueue.Remove(TypeOfToy))
                    Debug.LogWarning("Tried to remove none existing toy (" + TypeOfToy.name + ")");
                toysCount--;
            }
        }
    }

    void UpdateCurrentToy()
    {
        if (toysQueue.Count > 0)
        {
            if (toysQueue.First.Value == null)
            {
                toysQueue.RemoveFirst();
            }
            if(toysQueue.Count > 0)
                currentToy = toysQueue.First.Value;
            //toysQueue.RemoveFirst();
        }
        //else
        //{
        //    currentToy = null;
        //}
        toysCount--;
    }

    void SetDyingState()
    {
        dying = true;
    }

    void StopShootingAnimation()
    {
        anim.SetBool("shoot", false);
    }

    void IncreaseHealth()
    {
        HealthBar health = GetComponentInChildren<HealthBar>();
        health.HealToMax();
    }
}
