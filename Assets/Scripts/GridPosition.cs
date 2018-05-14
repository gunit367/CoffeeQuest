using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    const int UP = 0;
    const int RIGHT = 1;
    const int DOWN = 2;
    const int LEFT = 3;

    public int x;
    public int y;
    public int[] walls; // UP, RIGHT, DOWN, LEFT
    public GridPosition[] neighbors;
    public int group;

    public GridPosition(int x, int y, int group)
    {
        this.x = x;
        this.y = y;
        this.walls = new int[4];
        for (int i = 0; i < 4; i++)
        {
            this.walls[i] = 1;
        }

        this.neighbors = new GridPosition[4];
        for (int i = 0; i < 4; i++)
        {
            this.neighbors[i] = null;
        }
        this.group = group;
    }

    int Opposite(int dir)
    {
        switch (dir)
        {
            case UP:
                return DOWN;
            case DOWN:
                return UP;
            case RIGHT:
                return LEFT;
            case LEFT:
                return RIGHT;
        }
        return 0;
    }

    int Min(int x, int y)
    {
        if (x < y)
        {
            return x;
        }
        return y;
    }

    public void RemoveWall(int dir)
    {
        this.walls[dir] = 0;
        GridPosition n = this.neighbors[dir];
        n.walls[Opposite(dir)] = 0;

        int g = Min(this.group, n.group);
        n.SetGroup(g);
        this.SetGroup(g);
    }

    public int SeparateNeighbor()
    {
        for (int i = 0; i < 4; i++)
        {
            if (this.neighbors[i] != null)
            {
                if (this.neighbors[i].group != this.group)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public void SetGroup(int n)
    {
        if (this.group == n)
        {
            return;
        }
        this.group = n;
        for (int i = 0; i < 4; i++)
        {
            if (this.walls[i] == 0)
            {
                this.neighbors[i].SetGroup(n);
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
