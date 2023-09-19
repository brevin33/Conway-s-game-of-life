using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameOfLife : MonoBehaviour
{
    public GameOfLife game;

    private void OnMouseDown()
    {
        game.runGame = !game.runGame;
    }
}
