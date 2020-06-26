using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

static class Constants
{
    public const int xValue = 8;
    public const int yValue = 8;
}
public class SceneLoadTest : MonoBehaviour
{
    public int[,] map = new int[Constants.xValue + 2,Constants.yValue + 2 ];
    public int[,] wheigths = new int [Constants.xValue + 2, Constants.yValue + 2];
    private Vector2[] knots = new Vector2[5];


    class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }




    void Start()
    {
        for (int x = 0; x < Constants.xValue + 2; x++)
        {
            for (int y = 0; y < Constants.yValue + 2; y++)
            {
                map[x, y] = 0;
                wheigths[x, y] = 1;
            }
        }



        knots[0] = new Vector2(3, 1);
        for (int i = 1; i < 4; i++)
        {
            SetKnot(i);
        }
        SetRandomWeigths();

        Debug.Log(map[0, 5] + " " + map[1, 5] + " " + map[2, 5] + " " + map[3, 5] + " " + map[4, 5] + " " + map[5, 5] + " " + map[6, 5] + "\n"
                        + map[0, 4] + " " + map[1, 4] + " " + map[2, 4] + " " + map[3, 4] + " " + map[4, 4] + " " + map[5, 4] + " " + map[6, 4] + "\n"
                         + map[0, 3] + " " + map[1, 3] + " " + map[2, 3] + " " + map[3, 3] + " " + map[4, 3] + " " + map[5, 3] + " " + map[6, 3] + "\n"
                          + map[0, 2] + " " + map[1, 2] + " " + map[2, 2] + " " + map[3, 2] + " " + map[4, 2] + " " + map[5, 2] + " " + map[6, 2] + "\n"
                           + map[0, 1] + " " + map[1, 1] + " " + map[2, 1] + " " + map[3, 1] + " " + map[4, 1] + " " + map[5, 1] + " " + map[6, 1] + "\n"
                            + map[0, 0] + " " + map[1, 0] + " " + map[2, 0] + " " + map[3, 0] + " " + map[4, 0] + " " + map[5, 0] + " " + map[6, 0] + "\n");

        Debug.Log("Start Algorithm");
        AStartAlgorithm(0, 1);
        AStartAlgorithm(0, 2);
        AStartAlgorithm(0, 3);
        AStartAlgorithm(1, 3);
        AStartAlgorithm(2, 3);
        AStartAlgorithm(1, 2);

        ResetWeigths();
        CreateDeadEnd();
        AStartAlgorithm(0, 4);





        Debug.Log(map[0, 5] + " "  + map[1, 5] + " " + map[2, 5] + " " + map[3, 5] + " " + map[4, 5] + " " + map[5, 5] + " " + map[6, 5] + "\n" 
                        + map[0, 4] + " " + map[1, 4] + " " + map[2, 4] + " " + map[3, 4] + " " + map[4, 4] + " " + map[5, 4] + " " + map[6, 4] + "\n"
                         + map[0, 3] + " " + map[1, 3] + " " + map[2, 3] + " " + map[3, 3] + " " + map[4, 3] + " " + map[5, 3] + " " + map[6, 3] + "\n"
                          + map[0, 2] + " " + map[1, 2] + " " + map[2, 2] + " " + map[3, 2] + " " + map[4, 2] + " " + map[5, 2] + " " + map[6, 2] + "\n"
                           + map[0, 1] + " " + map[1, 1] + " " + map[2, 1] + " " + map[3, 1] + " " + map[4, 1] + " " + map[5, 1] + " " + map[6, 1] + "\n"
                            + map[0, 0] + " " + map[1, 0] + " " + map[2, 0] + " " + map[3, 0] + " " + map[4, 0] + " " + map[5, 0] + " " + map[6, 0] + "\n");


    }


    private void ResetWeigths()
    {
        for (int x = 0; x < Constants.xValue + 2; x++)
        {
            for (int y = 0; y < Constants.yValue + 2; y++)
            {
                if (wheigths[x, y] == 2)
                {
                    wheigths[x, y] = 0;
                }
                if (wheigths[x, y] == 3)
                {
                    wheigths[x, y] = 1;
                }

            }
        }
    }

    private void CreateDeadEnd()
    {
        int x, y;

        for (int i = 0; i < 10; i++)
        {
            x = Random.Range(0, 2);
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = Constants.xValue - 1;
            }
            y = Random.Range(3, 6);

            if (map[x - 1, y] == 0 && map[x + 1, y] == 0)
            {
                knots[4] = new Vector2(x, y);
            }
        }


    }

    private void SetRandomWeigths()
    {
        int x, y = 0;

        for(int i = 0; i < 10; i++)
        {
            x = Random.Range(2, Constants.xValue - 2);
            y = Random.Range(2, Constants.yValue - 2);
            wheigths[x, y] = 10;
        }
    }

    private void SetKnot(int index)
    {
        int x = Random.Range(2, Constants.xValue -2);
        int y = Random.Range(2, Constants.yValue - 2);


        for (int i = 0; i < 10; i++)
        {
            if ((map[x, y] == 0) && (map[x + 1, y] == 0) && (map[x - 1, y] == 0) && (map[x, y + 1] == 0) && (map[x, y - 1] == 0))
            {
                map[x, y] = 1;
                knots[index] = new Vector2(x, y);
                return;
            }
            x = Random.Range(2, Constants.xValue - 2);
            y = Random.Range(2, Constants.yValue - 2);
        }
        knots[index] = new Vector2(3, 1);
        return;

    }

    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Mathf.Abs(targetX - x) + Mathf.Abs(targetY - y);
    }


    private void AStartAlgorithm(int startIndex, int endIndex)
    {
        Debug.Log("Start: " + knots[0] + "End: " + knots[endIndex]);
        Location current = null;
        var start = new Location {X = (int)knots[startIndex].x, Y = (int)knots[startIndex].y, G = 0};
        var target = new Location { X = (int)knots[endIndex].x, Y = (int)knots[endIndex].y };
        var openList = new List<Location>();
        var closedList = new List<Location>();
        int g = 0;

        openList.Add(start);

        while (openList.Count > 0)
        {
            // algorithm's logic goes here

            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y);

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                {

                    // compute its score, set the parent
                    

                    if(adjacentSquare.X < 0 || adjacentSquare.X > Constants.xValue-1 || adjacentSquare.Y < 0 || adjacentSquare.Y > Constants.yValue - 1)
                    {
                        break;
                    }
                    Debug.Log("X adjancent Square: " + adjacentSquare.X + "Y: " + adjacentSquare.Y);

                    adjacentSquare.G = current.G + wheigths[adjacentSquare.X, adjacentSquare.Y];
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X,
                        adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }


        }

        while(current != null)
        {
            //Debug.Log("Currentx: " + current.X + " Y: " + current.Y);
            if (map[current.X, current.Y] == 0)
            map[current.X, current.Y] = 2;
            SetWeigth(current.X, current.Y);
            current = current.Parent;
        }



        //Debug.Log(map);
    }



    private void SetWeigth(int x, int y)
    {
        wheigths[x, y] = 2;

        if (wheigths[x+1, y] == 1)
        {
            wheigths[x + 1, y] = 4;
        }
        if (wheigths[x - 1, y] == 1)
        {
            wheigths[x - 1, y] = 4;
        }
        if (wheigths[x, y + 1] == 1)
        {
            wheigths[x, y + 1] = 4;
        }
        if (wheigths[x, y - 1] == 1)
        {
            wheigths[x, y - 1] = 4;
        }

    }

    static List<Location> GetWalkableAdjacentSquares(int x, int y)
    {

        var proposedLocations = new List<Location>()
    {
        new Location { X = x, Y = y - 1 },
        new Location { X = x, Y = y + 1 },
        new Location { X = x - 1, Y = y },
        new Location { X = x + 1, Y = y },
    };

        return proposedLocations;

    }

}
