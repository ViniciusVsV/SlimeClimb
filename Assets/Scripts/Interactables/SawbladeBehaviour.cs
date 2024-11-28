using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    private Transform targetPoint;

    void Start()
    {
        targetPoint = point1;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if(transform.position == point1.position)
            targetPoint = point2;
        else if(transform.position == point2.position)
            targetPoint = point1;

        RotateSawblade();
    }

    void RotateSawblade()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}