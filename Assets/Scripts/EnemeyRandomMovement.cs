using System.Collections.Generic;
using UnityEngine;

public class EnemeyRandomMovement : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] float timeBetweenPoints;

    private bool isMovingToPointSelected = true;

    private float aux = 0;

    private void Update()
    {
        if (isMovingToPointSelected)
        {
            aux += 1 / timeBetweenPoints * Time.deltaTime;
        }
        else
        {
            aux -= 1 / timeBetweenPoints * Time.deltaTime;
        }

        transform.position = Vector3.Lerp(points[0].position, points[1].position, aux);

        if (transform.position == points[1].position || transform.position == points[2].position)
        {
            isMovingToPointSelected = !isMovingToPointSelected;
        }
        if (transform.position == points[3].position || transform.position == points[4].position)
        {
            isMovingToPointSelected = !isMovingToPointSelected;
        }
        if (transform.position == points[5].position || transform.position == points[6].position)
        {
            isMovingToPointSelected = !isMovingToPointSelected;
        }
    }

    //private int UpdatePoint(List<Transform> list)
    //{
    //    int index = 0;
    //
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        index = Random.Range(0, list.Count);
    //    }
    //
    //    return index;
    //}
}
