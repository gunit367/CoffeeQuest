using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Maze
{
    int GRID_WIDTH;
    int GRID_HEIGHT;
    int TRIES_MAX;

    const int UP = 0;
    const int RIGHT = 1;
    const int DOWN = 2;
    const int LEFT = 3;

    GridPosition[,] grid;
    private int separate_components;
    private int tmp;

    public Maze(int width, int height)
    {
        this.GRID_WIDTH = width;
        this.GRID_HEIGHT = height;
        this.TRIES_MAX = 2;
        InitializeMaze();
    }

	public GridPosition[,] GetGrid() {
		return grid;
	}

    public void InitializeMaze()
    {
        this.grid = new GridPosition[GRID_WIDTH, GRID_HEIGHT];
        for (int i = 0; i < GRID_WIDTH; i++)
        {
            for (int j = 0; j < GRID_HEIGHT; j++)
            {
                int a0;
                int a1;
                int b;

                if (GRID_WIDTH > GRID_HEIGHT)
                {
                    b = GRID_WIDTH;
                    a0 = i;
                    a1 = j;
                }
                else
                {
                    b = GRID_HEIGHT;
                    a0 = j;
                    a1 = i;
                }

                int group = b * a0 + a1;

                this.grid[i, j] = new GridPosition(i, j, group);
            }
        }
        this.CreateGraph();
        this.separate_components = GRID_WIDTH * GRID_HEIGHT;
        this.tmp = 1;
    }

    public void CreateMaze()
    {
        while (this.separate_components > 1)
        {
            this.JoinRandomComponents();
        }
    }

    private void CreateGraphRecursive(int x, int y)
    {
        GridPosition pos = this.grid[x, y];
        GridPosition left = null;
        GridPosition up = null;

        if (pos.neighbors[LEFT] != null || pos.neighbors[UP] != null)
        {
            return; // Already visited
        }

        if (x > 0)
        {
            left = this.grid[x - 1, y];
            pos.neighbors[LEFT] = left;
            left.neighbors[RIGHT] = pos;
            CreateGraphRecursive(x - 1, y);
        }

        if (y > 0)
        {
            up = this.grid[x, y - 1];
            pos.neighbors[UP] = up;
            up.neighbors[DOWN] = pos;
            CreateGraphRecursive(x, y - 1);
        }
    }

    private void CreateGraph()
    {
        CreateGraphRecursive(GRID_WIDTH - 1, GRID_HEIGHT - 1);
    }

    private Pair GetRandomPosition()
    {
        int x = 0;
        int y = 0;
        int t;
        Pair res;

        if (this.tmp == 1)
        {
            int tries = 0;
            res = new Pair(Random.Range(0, GRID_WIDTH), Random.Range(0, GRID_HEIGHT));
            while (grid[res.x, res.y].SeparateNeighbor() == -1)
            {
                res = new Pair(Random.Range(0, GRID_WIDTH), Random.Range(0, GRID_HEIGHT));
                tries += 1;

                if (tries == TRIES_MAX)
                {
                    this.tmp = 0;
                    return res;
                }
            }
            return res;
        }

        t = this.grid[x, y].SeparateNeighbor();
        while (t == -1)
        {
            x += 1;
            if (x == GRID_WIDTH)
            {
                x = 0;
                y += 1;
            }
            t = this.grid[x, y].SeparateNeighbor();
        }
        return new Pair(x, y);
    }

    private void JoinRandomComponents()
    {
        Pair coords = GetRandomPosition();
        GridPosition p = this.grid[coords.x, coords.y];
        for (int i = 3; i >= 0; i--)
        {
            if (p.neighbors[i] != null)
            {
                if (p.neighbors[i].group != p.group)
                {
                    p.RemoveWall(i);
                    this.separate_components -= 1;
                    return;
                }
            }
        }
        this.tmp = 0;
    }

    public void NextMaze()
    {
        GRID_HEIGHT += 1;
        GRID_WIDTH += 1;
        this.InitializeMaze();
    }


    private class Pair
    {
        public int x;
        public int y;

        public Pair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    
}
