using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float speed = 12f;
    public float gravity = -19.81f;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 🎯 INPUT (FIXED - NO DRIFT)
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // 🎯 BLEND TREE FIX
        Vector2 input = new Vector2(x, z);
        float moveAmount = Mathf.Clamp01(input.magnitude);

        animator.SetFloat("moveAmount", moveAmount, 0.1f, Time.deltaTime);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}