using UnityEngine;

public class EnemyForwardBackwardMovement : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float timeBetweenPoints;
    private bool isMovingToPointA = true;
    
    private float aux = 0;

    private void Update()
    {
        if (isMovingToPointA)
        {
            aux += 1 / timeBetweenPoints * Time.deltaTime;
        }
        else
        {
            aux -= 1 / timeBetweenPoints * Time.deltaTime;
        }

        transform.position = Vector3.Lerp(pointA.position, pointB.position, aux);

        if (transform.position == pointA.position || transform.position == pointB.position)
        {
            isMovingToPointA = !isMovingToPointA;
        }
    }
}
