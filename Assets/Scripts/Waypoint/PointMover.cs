using UnityEngine;

public class PointMover : MonoBehaviour
{
    public Point point;
    public float speed = 2f;

    private Vector3 target;
    private int currentIndex = 0;

    void Start()
    {
        transform.position = point.points[0];
        currentIndex = 1;
        target = point.points[currentIndex];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (transform.position == target)
        {
            currentIndex++;
            if (currentIndex < point.points.Count)
            {
                target = point.points[currentIndex];
            }
        }
    }
}