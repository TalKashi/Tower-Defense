using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class RifleSoldier : MonoBehaviour
{
    //Game specs.
    public float HP;
    public float DistanceOfShot;
    public float ShotsInOneSecond;

    //Variables for shooting mechanism.
    private float TimeOfLastShot;
    private bool EnemyAtRange;
    public LayerMask ToHit;
    //private Mario Enemy;

    //Objects that the soldier holds.
    private Transform BurstPoint;
    public GameObject RifleShotPrefab;

    //Variables for positionning mechanism (dragging or double-clicking).
    private bool isMoving = false;
    private bool alreadyMoved = false;
    private Vector3 screenPoint;
    private Vector3 offset;

    private int numOfEnemies;

    private Vector3 mousePos;
    
	void Awake ()
    {
        //Creating the box collider which will detect that a toy enters the soldiers's distance of shot.
        BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(DistanceOfShot, 1);

        //Initiating the point from where the bullets will fly.
        BurstPoint = transform.FindChild("RifleBurstPoint");

        //World.WorldInstance.NewEnemy += NewEnemy;
        //World.WorldInstance.EnemyDied += DeadEnemy;
	}

    void NewEnemy(string stamString, int stamInt)
    {
        numOfEnemies++;
    }

    void DeadEnemy(string stamString, int stamInt)
    {
        numOfEnemies--;
    }
	
	void Update ()
    {
        //Check if there is an enemy at range and if its time to shoot (cause of fire rate and the sprite is not moving).      
        if (EnemyAtRange && numOfEnemies > 0)
        {
            bool readyToShoot = (Time.time >= TimeOfLastShot + 1 / ShotsInOneSecond) && ! isMoving;

            if (readyToShoot)
            {
                Shoot();
            }
        }
	}

    void Shoot()
    {
        TimeOfLastShot = Time.time;

        GameObject clone;
        clone = (Instantiate(RifleShotPrefab, new Vector3(BurstPoint.position.x, BurstPoint.position.y, 0), Quaternion.identity)) as GameObject;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyAtRange = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyAtRange = false;
        }
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));   
    }

    void OnMouseDrag()
    {
        if (!alreadyMoved)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            isMoving = true;
        }
    }

    void OnMouseUp()
    {
        isMoving = false;
        alreadyMoved = true;
    }
}
