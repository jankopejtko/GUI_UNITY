using UnityEngine;

//todo level 4 die when player y is less than -10
using UnityEngine.SceneManagement;
//todo level 4 die when player y is less than -10

public class player_movement_example : MonoBehaviour
{
    [Header("Reference")]
    public CharacterController controller;
    public Transform playerCamera;

    [Header("Nastavení pohybu")]
    public float walkSpeed = 5f;
    public float mouseSensitivity = 500f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;

    //double jump
    private int jumpCount = 0;
    //double jump

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // hide cursor and lock it to the center of the screen
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // keybord
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //todo for students: make it so that the player can move faster when holding down left shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = 10f;
        }
        else
        {
            walkSpeed = 5f;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * walkSpeed * Time.deltaTime);

        // jump
        //if (Input.GetButtonDown("Jump") && jumpCount < 2 && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}
        //todo for students: make it so that the player can double jump by allowing them to jump again while in the air, but only once before landing again
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;
        }
        if (isGrounded && velocity.y < 0)
        {
            jumpCount = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        //todo level 4 die when player y is less than -10
        if (this.transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //todo level 4 die when player y is less than -10
    }
}
