using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    Material white;
    [SerializeField]
    Material black;

    GameOfLife game;
    MeshRenderer meshRenderer;
    int x,y;
    public void setup(int x, int y, GameOfLife game)
    {
        this.x = x;
        this.y = y;
        this.game = game;
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnMouseDown()
    {
        game.setCell(x,y, this);
    }

    public void setColor(bool isWhite)
    {
        meshRenderer.material = isWhite ? white : black;
    }
}
