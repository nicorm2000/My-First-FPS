using System.Collections.Generic;
using UnityEngine;

public class EnemeyRandomMovement : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] float timeBetweenPoints;

    private float aux = 0;

    private Transform initial;
    private Transform target;

    private void Awake()
    {
        initial = points[0];

        target = initial;

        UpdateTarget();
    }

    private void Update()
    {
        aux += Time.deltaTime;
        
        transform.position = Vector3.Lerp(initial.position, target.position, aux / timeBetweenPoints);

        if (aux > timeBetweenPoints)
        {
            aux = 0;

            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        List<Transform> newList = new List<Transform>(points);
        newList.Remove(target);

        int index = Random.Range(0, newList.Count);

        initial = target;

        target = newList[index];
    }
}
