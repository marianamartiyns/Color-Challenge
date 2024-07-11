using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variáveis de PlayerMoviment
    public float MoveSmoothTime;
    public float GravityStrength;
    public float JumpStrength;
    public float WalkSpeed;
    public float RunSpeed;

    // Variáveis de PlayerController
    private CharacterController controller;
    private Animator anim;
    public float speed;
    public float gravity;
    public float rotSpeed;
    private float rot;
    private Vector3 moveDirection;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;
    private Vector3 currentForceVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleMovement();
    }

    // Movimentação do PlayerMoviment
    private void HandleMovement()
    {
        Vector3 playerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if (playerInput.magnitude > 1f)
        {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

        currentMoveVelocity = Vector3.SmoothDamp(
            currentMoveVelocity,
            moveVector * currentSpeed,
            ref moveDampVelocity,
            MoveSmoothTime
        );

        controller.Move(currentMoveVelocity * Time.deltaTime);

        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(groundCheckRay, 1.1f))
        {
            currentForceVelocity.y = -2f;

            if (Input.GetKey(KeyCode.Space))
            {
                currentForceVelocity.y = JumpStrength;
            }
            else
            {
                currentForceVelocity.y -= GravityStrength * Time.deltaTime;
            }

            controller.Move(currentForceVelocity * Time.deltaTime);
        }
    }

    // Movimentação do PlayerController
    private void Move()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = Vector3.forward * speed;
                anim.SetInteger("transition", 1);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                moveDirection = Vector3.zero;
                anim.SetInteger("transition", 0);
            }
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}

