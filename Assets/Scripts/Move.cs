using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody rb;
    private float moveX, moveY;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        move();
    }

    void move()
    {
        Vector3 movement = new Vector3(moveX, 0, moveY);
        rb.linearVelocity = movement * speed;
    }
}
