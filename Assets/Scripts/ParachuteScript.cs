using UnityEngine;
using System.Collections;

public class ParachuteScript : MonoBehaviour {

    public Package package;

    Fader faderScript;
    World world = World.WorldInstance;

	// Use this for initialization
	void Start () {
        int random = Random.Range(0, 12);

        if (random >= 11) // 11
        {
            package = Package.ATOM_BOMB;
        }
        else if (random >= 9)
        {
            package = Package.HEALTH;
        }
        else if (random >= 6) 
        {
            package = Package.GRENADE_SOLDIER;
        }
        else if (random >= 3)
        {
            package = Package.RPG_SOLDIER;
        }
        else
        {
            package = Package.SHOTGUN_SOLDIER;
        }
        faderScript = GetComponent<Fader>();
	}

    void OnMouseDown()
    {
        world.OnPackageCollected(package);
        faderScript.Fade(GetComponent<SpriteRenderer>());
    }
}
