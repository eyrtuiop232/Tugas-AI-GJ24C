using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Point", menuName = "Waypoint/Point")]
public class Point : ScriptableObject
{
    public List<Vector3> points = new List<Vector3>();
}