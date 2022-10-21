
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private GameObject[] points;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].transform.position);
            points[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        }
    }

}
