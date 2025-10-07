using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed;
    public Transform target;

    void Start()
    {
        
    }


    void Update()
    {
        Vector3 directionToTarget = target.position - transform.position;
        Vector3 changeInPosition = (directionToTarget).normalized * speed * Time.deltaTime;
        if (directionToTarget.magnitude > changeInPosition.magnitude)
        {
            transform.position += changeInPosition;
        }
        else
        {
            transform.position = target.position;
        }
        
    }
}
