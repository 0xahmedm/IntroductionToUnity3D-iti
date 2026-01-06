using UnityEngine;

public class MoveCharController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    CharacterController controller;

    MyInputSystem inputSystem;
    Vector2 inputVector;

    [SerializeField]Camera TPCamera;
    [SerializeField]Camera FPCamera;
    public float mouseSens = 1f;


    Animator anim;

    void Awake()
    {
        inputSystem = new MyInputSystem();
        moveWithInputSystem();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y);
        controller.Move(move * speed * Time.deltaTime);
        if (move != Vector3.zero)
        {
            anim.SetBool("running", true);
            transform.forward = move;
        }
        else
        {
            anim.SetBool("running", false);
        }
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
        inputSystem.Player.ToggleCam.performed += ctx =>
        {
            TPCamera.enabled = !TPCamera.enabled;
            FPCamera.enabled = !FPCamera.enabled;
        };
        inputSystem.Player.Look.performed += ctx =>
        {
            Vector2 lookVector = ctx.ReadValue<Vector2>();
            transform.Rotate(0, lookVector.x * mouseSens, 0);
            if (TPCamera.enabled)
            {
                TPCamera.transform.Rotate(-lookVector.y * mouseSens, 0, 0);
            }
            else if (FPCamera.enabled)
            {
                FPCamera.transform.Rotate(-lookVector.y * mouseSens, 0, 0);
            }
        };
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        inputSystem.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }
}