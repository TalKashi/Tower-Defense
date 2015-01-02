using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScript : MonoBehaviour {

    public GameObject GirlToy;
    public GameObject Teadybear;
    public GameObject Duck;
    public GameObject PotatoHead;
    public GameObject Package;
    public Image NukeImageFlash;
    public float flashSpeed = 5f;
    public float TimeBetweenPackages = 10;

    World world = World.WorldInstance;
    Color flashColour = new Color(1, 1, 1, 0.9f);

    private float top = 1.8f;
    private float mid = -0.5f;
    private float bottom = -2.5f;
    private float lastPackage = 0;
    private bool isNukeActivated = false;
    private bool hasBeenCalled = true;

    private const float X_POS = -15;
    private const float RIGHT_MOST_SCREEN = 7.5f;
    private const float LEFT_MOST_SCREEN = -RIGHT_MOST_SCREEN;

	// Use this for initialization
	void Start () {
        OneToyInEachRow();
        //CreateDuckBottom();
        lastPackage = Time.time;
        world.NukeActivated += OnNukeActivatedListener;
        NukeImageFlash.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastPackage >= TimeBetweenPackages)
        {
            CreatePackage();
            lastPackage = Time.time;
        }
        if (Time.time > 10 && hasBeenCalled)
        {
            world.OnNukeActivated();
            hasBeenCalled = false;
        }

        if (isNukeActivated)
        {
            NukeImageFlash.color = flashColour;
        }
        else
        {
            NukeImageFlash.color = Color.Lerp(NukeImageFlash.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isNukeActivated = false;
	}

    void OnNukeActivatedListener()
    {
        isNukeActivated = true;
    }

    void CreatePackage()
    {
        Instantiate(Package, new Vector3(Random.Range(LEFT_MOST_SCREEN, RIGHT_MOST_SCREEN), 9, 0), Quaternion.identity);
    }

    void OneToyInEachRow()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                CreateDuckTop();
                CreateTeadybearMid();
                CreatePotatoHeadBottom();
                break;
            case 1:
                CreateDuckMid();
                CreateTeadybearTop();
                CreateGirlToyBottom();
                break;
            case 2:
                CreatePotatoHeadBottom();
                CreateTeadybearTop();
                CreateGirlToyMid();
                break;
        }

    }

    void CreateGirlToy(float y, int row)
    {
        GameObject clone = (GameObject) Instantiate(GirlToy, new Vector3(X_POS, y, 0), Quaternion.identity);
        clone.GetComponent<Toy>().MyRow = row;
        clone.GetComponentInChildren<HealthBar>().isToy = true;
    }

    void CreateGirlToyTop()
    {
        CreateGirlToy(top, 1);
    }

    void CreateGirlToyMid()
    {
        CreateGirlToy(mid, 2);
    }

    void CreateGirlToyBottom()
    {
        CreateGirlToy(bottom, 3);
    }

    void CreateTeadybear(float y, int row)
    {
        GameObject clone = (GameObject) Instantiate(Teadybear, new Vector3(X_POS, y, 0), Quaternion.identity);
        clone.GetComponent<Toy>().MyRow = row;
        clone.GetComponentInChildren<HealthBar>().isToy = true;
    }

    void CreateTeadybearTop()
    {
        CreateTeadybear(top, 1);
    }

    void CreateTeadybearMid()
    {
        CreateTeadybear(mid, 2);
    }

    void CreateTeadybearBottom()
    {
        CreateTeadybear(bottom, 3);
    }

    void CreateDuck(float y, int row)
    {
        GameObject clone = (GameObject) Instantiate(Duck, new Vector3(X_POS, y, 0), Quaternion.identity);
        clone.GetComponent<Toy>().MyRow = row;
        clone.GetComponentInChildren<HealthBar>().isToy = true;
    }

    void CreateDuckTop()
    {
        CreateDuck(top, 1);
    }

    void CreateDuckMid()
    {
        CreateDuck(mid, 2);
    }

    void CreateDuckBottom()
    {
        CreateDuck(bottom, 3);
    }

    void CreatePotatoHead(float y, int row)
    {
        GameObject clone = (GameObject) Instantiate(PotatoHead, new Vector3(X_POS, y, 0), Quaternion.identity);
        clone.GetComponent<Toy>().MyRow = row;
        clone.GetComponentInChildren<HealthBar>().isToy = true;
    }

    void CreatePotatoHeadTop()
    {
        CreatePotatoHead(top, 1);
    }

    void CreatePotatoHeadMid()
    {
        CreatePotatoHead(mid, 2);
    }

    void CreatePotatoHeadBottom()
    {
        CreatePotatoHead(bottom, 3);
    }

    void CreateAllInSameRowRandom()
    {
        CreateAllInSameRow(Random.Range(1, 4));
    }

    void CreateAllInSameRow(int row)
    {
        if (row == 1)
        {
            CreateDuckTop();
            CreateGirlToyTop();
            CreatePotatoHeadTop();
            CreateTeadybearTop();
        }
        else if (row == 2)
        {
            CreateDuckMid();
            CreateGirlToyMid();
            CreatePotatoHeadMid();
            CreateTeadybearMid();
        }
        else
        {
            CreateDuckBottom();
            CreateGirlToyBottom();
            CreatePotatoHeadBottom();
            CreateTeadybearBottom();
        }
    }
}
