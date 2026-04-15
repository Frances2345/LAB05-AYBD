using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    private InputSystem_Actions inputs;
    private CharacterController controller;

    private Vector2 moveInput;

    public float moveSpeed = 10f;
    public float rotationSpeed = 200f;

    public int str;
    public int dtx;
    public int spd;

    private void Awake()
    {
        inputs = new();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void Start()
    {

    }

    void Update()
    {
        OnMove();
    }

    public void OnMove()
    {

    }
}