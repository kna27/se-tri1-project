using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform orientation;
    public bool movementEnabled = true;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDir;
    Rigidbody rb;
    float interactKeyHeldTime;

    [SerializeField] private float sensitivity;
    float xRotation;
    float yRotation;
    float mouseX;
    float mouseY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // raycast from center of screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Interactable.maxInteractRange))
        {
            Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
            if (interactable)
            {
                interactable.ShowInteractText();
                if (Input.GetKey(KeyCode.E))
                {
                    interactKeyHeldTime += Time.deltaTime;
                    if (interactKeyHeldTime >= interactable.interactTime)
                    {
                        interactable.Interact();
                        interactKeyHeldTime = 0;
                    }
                }
                else
                {
                    interactKeyHeldTime = 0;
                }
            }
            else
            {
                //GameObject.Find("Interact Panel").SetActive(false);
            }
        }

        if (movementEnabled)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    void FixedUpdate()
    {
        if (movementEnabled)
        {
            Move();
        }
    }

    void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDir.normalized * speed, ForceMode.Force);
    }
}
