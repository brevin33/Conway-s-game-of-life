using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField]
    float updateRate = 1;

    [SerializeField]
    int gridSizeX = 10;
    [SerializeField]
    int gridSizeY = 10;
    [SerializeField]
    float cellSize = .1f;
    [SerializeField]
    GameObject Cube;
    [SerializeField]
    GameObject StartButton;
    public bool runGame = true;



    bool[,] grid;
    Cell[,] cells;

    float updateTimer = 0;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        grid = new bool[gridSizeX,gridSizeY];
        cells = new Cell[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                grid[x,y] = false;
                GameObject cell = Instantiate(Cube, new Vector3(x * cellSize, y * cellSize), Quaternion.identity, transform);
                Cell script = cell.GetComponent<Cell>();
                script.setup(x, y, this);
                cells[x,y] = script;
            }
        }
        GameObject startButton = Instantiate(StartButton, new Vector3((gridSizeX + 1.5f) * cellSize, .5f), Quaternion.identity, transform);
        startButton.GetComponent<StartGameOfLife>().game = this;
        nextStep();
    }

    public void setCell(int x, int y, Cell cell)
    {
        grid[x,y] = !grid[x,y];
        cell.setColor(grid[x, y]);
    }


    void Update()
    {

        if (!runGame)
            return;
        updateTimer += Time.deltaTime;
        if (updateTimer > updateRate)
        {
            nextStep();
            updateTimer = 0;
        }
    }

    void nextStep()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (grid[x, y])
                    aliveCellEvaluation(x,y);
                else
                    deadCellEvaluation(x, y);
                cells[x, y].setColor(grid[x,y]);
            }
        }
    }



    void deadCellEvaluation(int x, int y)
    {
        Vector2Int[] kernal = new Vector2Int[] { new Vector2Int(x-1,y-1), new Vector2Int(x, y - 1) , new Vector2Int(x + 1, y - 1),
                                                    new Vector2Int(x-1,y),                           new Vector2Int(x + 1, y),
                                                    new Vector2Int(x-1,y+1), new Vector2Int(x, y + 1) , new Vector2Int(x + 1, y + 1)};
        int aliveNeighbors = 0;
        for (int i = 0; i < kernal.Length; i++)
        {
            int kx = kernal[i].x;
            int ky = kernal[i].y;
            if(bound(kx,0,gridSizeX-1) && bound(ky,0,gridSizeY-1) && grid[kx,ky])
                aliveNeighbors++;
        }

        grid[x, y] = aliveNeighbors == 3;
    }

    void aliveCellEvaluation(int x, int y)
    {
        Vector2Int[] kernal = new Vector2Int[] { new Vector2Int(x-1,y-1), new Vector2Int(x, y - 1) , new Vector2Int(x + 1, y - 1),
                                                    new Vector2Int(x-1,y),                           new Vector2Int(x + 1, y),
                                                    new Vector2Int(x-1,y+1), new Vector2Int(x, y + 1) , new Vector2Int(x + 1, y + 1)};
        int aliveNeighbors = 0;
        for (int i = 0; i < kernal.Length; i++)
        {
            int kx = kernal[i].x;
            int ky = kernal[i].y;
            if (bound(kx, 0, gridSizeX - 1) && bound(ky, 0, gridSizeY-1) && grid[kx, ky])
                aliveNeighbors++;
        }

        grid[x, y] = aliveNeighbors == 3 || aliveNeighbors == 2;
    }

    bool bound(int x, int lowerLimit, int uperLimit) {
        return lowerLimit <= x && x <= uperLimit;
    }
}
