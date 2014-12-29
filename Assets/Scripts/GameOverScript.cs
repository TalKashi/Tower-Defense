using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    World world = World.WorldInstance;

    void OnTriggerEnter2D()
    {
        world.OnGameOver();
    }
}
