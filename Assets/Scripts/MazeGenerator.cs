using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    public int MAZE_HEIGHT;
    public int MAZE_WIDTH;

    public GameObject wall;
    public GameObject ground;

    public float scale = 2f;

    public void generate()
	{
		Maze maze = new Maze(MAZE_WIDTH, MAZE_HEIGHT);
		maze.CreateMaze();
		GridPosition[,] gridPositions = maze.GetGrid();

        print("Generating Maze");
        

        GameObject groundObject =
                        Instantiate(ground, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;
        groundObject.transform.localScale = new Vector3(scale, 1, scale);
        wall.transform.localScale = new Vector3(.1f, 2f, scale);

		float x;
		float z;
        
        for (int index_x = 0; index_x < MAZE_WIDTH; index_x++)
		{
            for (int index_y = 0; index_y < MAZE_HEIGHT; index_y++)
			{
				x = scale * (-5f + index_x);
				z = scale * (4f - index_y);

				if (gridPositions[index_x, index_y].GetWalls()[GridPosition.LEFT] == 1)
                //if (true)
				{
					print("Created Left Wall");
                    // Instantiate the left wall
					GameObject lftWall = Instantiate(wall, new Vector3(x + 0.05f, 1f, z + (scale * 0.5f)), Quaternion.identity) as GameObject;
                }

				if (gridPositions[index_x, index_y].GetWalls()[GridPosition.UP] == 1)
				//if (true)
                {
					print("Created Top Wall");
                
                    // Instantiate the top wall
                    GameObject topWall = Instantiate(wall, new Vector3(x + (scale * 0.5f), 1f, z + scale - 0.05f), Quaternion.Euler(0, 90, 0)) as GameObject;
                }
                

				if (gridPositions[index_x, index_y].GetWalls()[GridPosition.RIGHT] == 1)
				//if (true)
                {
					print("Created Right Wall");
                    
                    // Instantiate the right wall
                    GameObject rhtWall = Instantiate(wall, new Vector3(x+scale-0.05f, 1f, z+(scale * 0.5f)), Quaternion.identity) as GameObject;
                }

                
				if (gridPositions[index_x, index_y].GetWalls()[GridPosition.DOWN] == 1)
				//if (true)
				{
					print("Created Bottom Wall");

					// Instantiate the bottom wall
					GameObject btmWall = Instantiate(wall, new Vector3(x+ (scale * 0.5f), 1f, z + 0.05f), Quaternion.Euler(0, 90, 0)) as GameObject;
				}
            }
        }

    }


}
