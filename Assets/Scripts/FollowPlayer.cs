using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerOrientation;
    public float speed = 3f;

    [Header("Ground Check")]
    public float objectHeight = 1f;
    public LayerMask whatIsGround;

    [Header("Physics Settings")]
    public float groundedDrag = 5f; // Drag when grounded
    public float airDrag = 0f; // Drag when in the air

    private bool grounded;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, objectHeight * 0.5f + 0.3f, whatIsGround);

        // Handle drag
        rb.drag = grounded ? groundedDrag : airDrag;

        if (grounded && playerOrientation != null)
        {
            Vector3 direction = (playerOrientation.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }
}
