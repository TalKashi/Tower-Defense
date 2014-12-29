using UnityEngine;
using System.Collections.Generic;

public class Soldier : MonoBehaviour {

    public GameObject Bullet;
    public int MyRow;
    public float TimeBetweenShots;

    GameObject currentToy;
    Transform burstPoint;
    World world = World.WorldInstance;
    Queue<GameObject> toysQueue;
    int toysCount;
    float timeOfLastShot = 0;
    bool dying;

    void Start()
    {
        toysCount = 0;
        burstPoint = transform.FindChild("BurstPoint");
        world.ToyDied += ToyDiedListener;
        toysQueue = new Queue<GameObject>();
        dying = false;
    }

    void Update()
    {
        if (dying)
        {
            return;
        }
        if(toysQueue.Count > 0)
        {
            shoot();
        }
    }

    void shoot()
    {
        if (Time.time - timeOfLastShot >= TimeBetweenShots)
        {
            GameObject clone = (GameObject) Instantiate(Bullet, new Vector3(burstPoint.position.x, burstPoint.position.y, 0), Quaternion.identity);
            if(gameObject.name == "Shotgun_Soldier")
                clone.GetComponent<RifleShot>().MyRow = MyRow;
            else
                clone.GetComponent<AreaDeamageShot>().MyRow = MyRow;
            timeOfLastShot = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Toy")
        {
            if (toysCount == 0)
            {
                currentToy = other.gameObject;
            }
            toysCount++;
            toysQueue.Enqueue(other.gameObject);
        }
    }

    void ToyDiedListener(string TypeOfToy, int row)
    {
        if (MyRow == row)
        {
            if (toysQueue.Count > 0)
            {
                currentToy = toysQueue.Dequeue();
            }
            else
            {
                currentToy = null;
            }
            toysCount--;
        }
    }

    void SetDyingState()
    {
        dying = true;
    }
}
