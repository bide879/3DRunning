using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Rigidbody rigid;
    Animator animator;

    /// <summary>
    /// 이동 방향(1 : 전진, -1 : 후진, 0 : 정지)
    /// </summary>
    float moveDirection = 0.0f;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 점프력
    /// </summary>
    public float jumpPower = 6.0f;

    /// <summary>
    /// 점프 중인지 아닌지 나타내는 변수
    /// </summary>
    bool isJumping = false;

    /// <summary>
    /// 점프 쿨 타임
    /// </summary>
    public float jumpCoolTime = 5.0f;

    /// <summary>
    /// 남아있는 쿨타임
    /// </summary>
    float jumpCoolRemains = -1.0f;

    /// <summary>
    /// 점프가 가능한지 확인하는 프로퍼티(점프중이 아니고 쿨타임이 다 지났다.)
    /// </summary>
    bool IsJumpAvailable => !isJumping && (jumpCoolRemains < 0.0f);

    readonly int IsMoveHash = Animator.StringToHash("IsMove");
    readonly int IsJump = Animator.StringToHash("Jump");
    readonly int DieHash = Animator.StringToHash("Die");

    /// <summary>
    /// 플레이어의 생존 여부
    /// </summary>
    bool isAlive = true;

    public Action onDie;

    private void Awake()
    {
        inputActions = new();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Jump.performed += OnJumpInput;
        inputActions.Player.Use.performed += OnUseInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Use.performed -= OnUseInput;
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

    private void OnUseInput(InputAction.CallbackContext context)
    {
        //animator.SetTrigger(UseHash);
    }

    private void Update()
    {
        jumpCoolRemains -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnDie();
        }

    }

    /// <summary>
    /// 이동 입력 처리용 함수
    /// </summary>
    /// <param name="input">입력된 방향</param>
    /// <param name="isMove">이동 중이면 true, 이동 중이 아니면 false</param>
    void SetInput(Vector2 input, bool isMove)
    {
        moveDirection = input.x;
        animator.SetBool(IsMoveHash, isMove);
        //animator.SetBool(IsMoveHash, isMove);
    }

    /// <summary>
    /// 실제 이동 처리 함수(FixedUpdate에서 사용)
    /// </summary>
    void Move()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * moveDirection * transform.right);
    }

    /// <summary>
    /// 실제 점프 처리를 하는 함수
    /// </summary>
    void Jump()
    {
        if (IsJumpAvailable) // 점프가 가능할 때만 점프
        {
            animator.SetTrigger(IsJump);
            rigid.AddForce(jumpPower * Vector3.up, ForceMode.Impulse);  // 위쪽으로 jumpPower만큼 힘을 더하기
            jumpCoolRemains = jumpCoolTime; // 쿨타임 초기화
            isJumping = true;               // 점프했다고 표시
        }
    }

    void OnDie()
    {
        if (isAlive)
        {
            Debug.Log("죽었음");

            // 죽는 애니메이션이 나온다.
            animator.SetTrigger(DieHash);

            // 더 이상 조종이 안되어야 한다.
            inputActions.Player.Disable();

            // 대굴대굴 구른다.(뒤로 넘어가면서 y축으로 스핀을 먹는다.)
            rigid.constraints = RigidbodyConstraints.None;  // 물리 잠금을 전부 해제하기
            Transform head = transform.GetChild(0);
            rigid.AddForceAtPosition(-transform.forward, head.position, ForceMode.Impulse);
            rigid.AddTorque(transform.up * 1.5f, ForceMode.Impulse);

            // 죽었다고 신호보내기(onDie 델리게이트 실행)
            onDie?.Invoke();

            isAlive = false;
        }
    }

}
