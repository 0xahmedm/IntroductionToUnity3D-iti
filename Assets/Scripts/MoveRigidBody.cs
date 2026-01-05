using UnityEngine;

public class MoveRigidBody : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    //private Rigidbody rb;
    //private float moveX, moveY;
    CharacterController controller;

    MyInputSystem inputSystem;
    Vector2 inputVector;

    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        inputSystem = new MyInputSystem();
        moveWithInputSystem();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        controller.Move(new Vector3(inputVector.x, 0, inputVector.y) * speed * Time.deltaTime);
    }


    void moveWithInputSystem()
    {
        inputSystem.Player.Move.performed += ctx =>
        {
            inputVector = ctx.ReadValue<Vector2>();
        };
        inputSystem.Player.Move.canceled += ctx =>
        {
            inputVector = Vector2.zero;
        };
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }

    //void move()
    //{
    //    Vector3 movement = new Vector3(moveX, 0, moveY);
    //    rb.linearVelocity = movement * speed;
    //}
}