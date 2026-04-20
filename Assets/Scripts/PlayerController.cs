using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions inputs;
    private CharacterController controller;

    public TextMeshProUGUI healthText;

    private Vector2 moveInput;

    public float health = 100f;
    public int attack = 20;

    public float moveSpeed = 10f;
    public float rotationSpeed = 200f;

    public bool canMove = true;

    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputs.Disable();
    }

    void Update()
    {
        if(!canMove)
        {
            return;
        }

        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);
        Vector3 moveDir = (transform.forward * moveInput.y) * moveSpeed;

        if (!controller.isGrounded) moveDir.y = -9.81f;

        controller.Move(moveDir * Time.deltaTime);
        ActualizarUI();
    }

    public void MoreAttack()
    {
        attack = attack + 5;
    }

    public void LessAttack()
    {
        attack = attack - 5;
    }

    public void MoreSpeed()
    {
        moveSpeed = moveSpeed + 5;
    }

    public void LessSpeed()
    {
        moveSpeed = moveSpeed - 5;
    }


    public void ActualizarUI()
    {
        if (healthText != null)
        {
            healthText.text = "Vida: " + health + " | Atk: " + attack + " | Vel: " + moveSpeed;
        }
    }
}