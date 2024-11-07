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

    // �߷� ���� �߰�
    public float gravity = -9.81f;  // �߷� ��
    private float verticalVelocity;  // ���� �ӵ�

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

        // �Է� ���� ��� (����ȭ�� ���� ���)
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDirection = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")).normalized;

        // �̵� ���Ϳ� �ӵ� ����
        Vector3 velocity = moveDirection * finalSpeed;

        // �߷� ����
        if (_controller.isGrounded)
        {
            verticalVelocity = -1f;  // �ٴڿ� ��� ���� �� ���� �ӵ� �ʱ�ȭ (-1�� �ణ�� �߷� ����)
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // �߷��� ���������� ����
        }

        velocity.y = verticalVelocity;  // ���� �ӵ��� �̵� ���Ϳ� ����

        // ���� �̵�
        _controller.Move(velocity * Time.deltaTime);

        // �ִϸ��̼� Blend �� ����
        if (moveDirection.magnitude > 0) // �������� ���� ���� �� ����
        {
            float targetBlend = run ? 1f : 0.5f;
            _animator.SetFloat("Blend", targetBlend, 0.1f, Time.deltaTime);
        }
        else
        {
            // �������� ���� ���� Idle ���·� ��ȯ
            _animator.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);
        }
    }
}
