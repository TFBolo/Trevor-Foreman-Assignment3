using UnityEngine;

public class Car : MonoBehaviour
{

    public Rigidbody2D rb;

    public float minSpeed = 8f;
    public float maxSpeed = 12f;

    float speed = 1f;
    float timeAlive = 3;

    private void Start()
    {
        timeAlive = -(GameManager.speed / 4) + 5;
        minSpeed = GameManager.speed;
        maxSpeed = GameManager.speed + 4;
        speed = Random.Range(minSpeed, maxSpeed);
        Destroy(gameObject, timeAlive);
    }

    void FixedUpdate()
    {
        Vector2 forward = new Vector2(transform.right.x, transform.right.y);
        rb.MovePosition(rb.position + forward * Time.fixedDeltaTime * speed);
    }
}
