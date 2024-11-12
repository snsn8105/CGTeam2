using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    Animator _animator;
    Camera _camera;
    CharacterController _controller;

    public float speed = 5f;
    public float runspeed = 8f;
    public float finalSpeed;
    public bool toggleCameraRotation;
    public bool run;
    public float smoothness = 10f;

    // 중력 변수 추가
    public float gravity = -9.81f;  // 중력 값
    private float verticalVelocity;  // 수직 속도

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _camera = Camera.main;
        _controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        InputMovement();
    }

    void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            if (_camera == null) return;
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

    void InputMovement()
    {
        finalSpeed = (run) ? runspeed : speed;

        // 입력 벡터 계산 (정규화된 벡터 사용)
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDirection = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")).normalized;

        // 이동 벡터에 속도 적용
        Vector3 velocity = moveDirection * finalSpeed;

        // 중력 적용
        if (_controller.isGrounded)
        {
            verticalVelocity = -1f;  // 바닥에 닿아 있을 때 수직 속도 초기화 (-1로 약간의 중력 유지)
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // 중력을 지속적으로 적용
        }

        velocity.y = verticalVelocity;  // 수직 속도를 이동 벡터에 포함

        // 최종 이동
        _controller.Move(velocity * Time.deltaTime);

        // 애니메이션 Blend 값 설정
        if (moveDirection.magnitude > 0) // 움직임이 있을 때만 값 설정
        {
            float targetBlend = run ? 1f : 0.5f;
            _animator.SetFloat("Blend", targetBlend, 0.1f, Time.deltaTime);
        }
        else
        {
            // 움직임이 없을 때는 Idle 상태로 전환
            _animator.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);
        }
    }
}
