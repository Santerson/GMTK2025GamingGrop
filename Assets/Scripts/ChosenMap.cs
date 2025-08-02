using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenMap : MonoBehaviour
{
    public List<Vector2> points = new List<Vector2>();
    public int map = 1;

    public void changeMap(List<Vector2> points, int map)
    {
        this.points = points;
        this.map = map;
    }
}
