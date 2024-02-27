using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Rigidbody rigid;
    Animator animator;

    public bool getItem = false;

    public Action speedUp;


    /// <summary>
    /// �̵� ����(1 : ����, -1 : ����, 0 : ����)
    /// </summary>
    float moveDirectionFB = 0.0f;

    /// <summary>
    /// �̵� ����(1 : ����, -1 : ����, 0 : ����)
    /// </summary>
    float moveDirectionRL = 0.0f;

    /// <summary>
    /// �յ� �̵� �ӵ�
    /// </summary>
    public float moveFBSpeed = 9.0f;

    /// <summary>
    /// �¿� �̵� �ӵ�
    /// </summary>
    public float moveRLSpeed = 5.0f;

    /// <summary>
    /// �ڷ� �и��� �ӵ�
    /// </summary>
    public float moveBack = 5.0f;

    /// <summary>
    /// ������
    /// </summary>
    public float jumpPower = 6.0f;

    /// <summary>
    /// ���� ������ �ƴ��� ��Ÿ���� ����
    /// </summary>
    bool isJumping = false;

    /// <summary>
    /// ���� �� Ÿ��
    /// </summary>
    public float jumpCoolTime = 5.0f;

    /// <summary>
    /// �����ִ� ��Ÿ��
    /// </summary>
    float jumpCoolRemains = -1.0f;

    /// <summary>
    /// ������ �������� Ȯ���ϴ� ������Ƽ(�������� �ƴϰ� ��Ÿ���� �� ������.)
    /// </summary>
    bool IsJumpAvailable => !isJumping && (jumpCoolRemains < 0.0f);

    readonly int IsMoveHash = Animator.StringToHash("IsMove");
    readonly int IsJump = Animator.StringToHash("Jump");
    readonly int IsDash = Animator.StringToHash("Dash");
    readonly int DieHash = Animator.StringToHash("Die");

    /// <summary>
    /// �÷��̾��� ���� ����
    /// </summary>
    bool isAlive = true;

    public Action onDie;

    /// <summary>
    /// ������ �� Ÿ��
    /// </summary>
    public float itemCoolTime = 5.0f;

    /// <summary>
    /// ������ �����ִ� ��Ÿ��
    /// </summary>
    float itemCoolRemains = -1.0f;

    private void Awake()
    {
        inputActions = new();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.onSpeedUp += OnSpeedUp;
        GameManager.Instance.onSpeedUpEnd += OnSpeedUpEnd;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Jump.performed += OnJumpInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= OnJumpInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        SetInput(context.ReadValue<Vector2>(), !context.canceled);
    }

    private void OnJumpInput(InputAction.CallbackContext _)
    {
        Jump();
    }

    private void Update()
    {
        jumpCoolRemains -= Time.deltaTime;
        itemCoolRemains -= Time.deltaTime;

        if (itemCoolRemains > 0 && getItem)
        {
            rigid.MovePosition(rigid.position + Time.deltaTime * moveFBSpeed * transform.forward);
        }

        if(itemCoolRemains < 0 && getItem)
        {
            getItem = false;
            animator.SetBool(IsDash, getItem);
            DashEnd();
        }
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveBack * -transform.forward);
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            getItem = true;
            Dash();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnDie();
        }

    }

    /// <summary>
    /// �̵� �Է� ó���� �Լ�
    /// </summary>
    /// <param name="input">�Էµ� ����</param>
    /// <param name="isMove">�̵� ���̸� true, �̵� ���� �ƴϸ� false</param>
    void SetInput(Vector2 input, bool isMove)
    {
        moveDirectionRL = input.x;
        moveDirectionFB = input.y;
        animator.SetBool(IsMoveHash, isMove);
        //animator.SetBool(IsMoveHash, isMove);
    }

    /// <summary>
    /// ���� �̵� ó�� �Լ�(FixedUpdate���� ���)
    /// </summary>
    void Move()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveFBSpeed * moveDirectionFB * transform.forward);

        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveRLSpeed * moveDirectionRL * transform.right);
    }

    /// <summary>
    /// ���� ���� ó���� �ϴ� �Լ�
    /// </summary>
    void Jump()
    {
        if (IsJumpAvailable) // ������ ������ ���� ����
        {
            animator.SetTrigger(IsJump);
            rigid.AddForce(jumpPower * Vector3.up, ForceMode.Impulse);  // �������� jumpPower��ŭ ���� ���ϱ�
            jumpCoolRemains = jumpCoolTime; // ��Ÿ�� �ʱ�ȭ
            isJumping = true;               // �����ߴٰ� ǥ��
        }
    }

    private void Dash()
    {
        animator.SetBool(IsDash, getItem);
        speedUp?.Invoke();
        GameManager.Instance.SpeedUp();
        itemCoolRemains = itemCoolTime; // ��Ÿ�� �ʱ�ȭ
        Debug.Log("�÷��̾� Dash");
    }

    private void DashEnd()
    {
        GameManager.Instance.SpeedUpEnd();
        Debug.Log("�÷��̾� DashEnd");
    }


    void OnDie()
    {
        if (isAlive)
        {
            Debug.Log("�׾���");

            // �״� �ִϸ��̼��� ���´�.
            animator.SetTrigger(DieHash);

            // �� �̻� ������ �ȵǾ�� �Ѵ�.
            inputActions.Player.Disable();

            // �뱼�뱼 ������.(�ڷ� �Ѿ�鼭 y������ ������ �Դ´�.)
            rigid.constraints = RigidbodyConstraints.None;  // ���� ����� ���� �����ϱ�
            Transform head = transform.GetChild(0);
            rigid.AddForceAtPosition(-transform.forward, head.position, ForceMode.Impulse);
            rigid.AddTorque(transform.up * 1.5f, ForceMode.Impulse);

            // �׾��ٰ� ��ȣ������(onDie ��������Ʈ ����)
            onDie?.Invoke();

            isAlive = false;
        }
    }

    private void OnSpeedUp()
    {
        moveFBSpeed = 7.0f;
        inputActions.Player.Disable();
    }

    private void OnSpeedUpEnd()
    {
        moveFBSpeed = 9.0f;
        inputActions.Player.Enable();
    }
}
