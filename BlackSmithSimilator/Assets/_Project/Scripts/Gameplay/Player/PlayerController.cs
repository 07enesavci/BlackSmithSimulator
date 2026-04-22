using UnityEngine;
using UnityEngine.InputSystem;

namespace BlacksmithSimulator.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Hareket Ayarları")]
        [SerializeField] private float _walkSpeed = 2.5f;
        [SerializeField] private float _runSpeed = 5f;
        [SerializeField] private float _speedTransitionSmoothing = 10f;
        [SerializeField] private float _jumpHeight = 1.5f;
        [SerializeField] private float _gravity = -19.62f;

        [Header("Kamera Ayarları")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _mouseSensitivity = 15f;
        [SerializeField] private float _maxLookAngle = 80f;

        [Header("Zemin Kontrolü")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundMask;

        private CharacterController _controller;
        private InputSystem_Actions _inputActions;
        
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private Vector3 _velocity; 
        private float _currentSpeed;
        
        private float _xRotation = 0f;
        private bool _isGrounded;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputActions = new InputSystem_Actions();

            QualitySettings.vSyncCount = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _inputActions.Player.Jump.performed += OnJumpPerformed;
        }

        private void OnEnable() => _inputActions.Player.Enable();
        private void OnDisable() 
        {
            _inputActions.Player.Disable();
            _inputActions.Player.Jump.performed -= OnJumpPerformed;
        }

        private void Update()
        {
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            _lookInput = _inputActions.Player.Look.ReadValue<Vector2>();

            RotateBody(); 
            CalculateGravity();
            MovePlayer();
        }

        private void LateUpdate()
        {
            RotateHead();
        }

        private void CalculateGravity()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
        }

        private void MovePlayer()
        {
            bool isMovingForward = _moveInput.y > 0;
            bool hasMovementInput = _moveInput != Vector2.zero;
            bool isSprinting = _inputActions.Player.Sprint.IsPressed() && isMovingForward;

            if (hasMovementInput)
            {
                float targetSpeed = isSprinting ? _runSpeed : _walkSpeed;
                _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * _speedTransitionSmoothing);
            }
            else
            {
                // Elini tuştan çektiği an yumuşatma yapma, dur.
                _currentSpeed = 0f;
            }

            Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
            
            if (moveDirection.magnitude > 1f)
            {
                moveDirection.Normalize();
            }

            Vector3 finalMovement = moveDirection * _currentSpeed;
            finalMovement.y = _velocity.y; 

            _controller.Move(finalMovement * Time.deltaTime);
        }

        private void RotateBody()
        {
            float mouseX = _lookInput.x * _mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }

        private void RotateHead()
        {
            if (_cameraTransform == null) return;

            float mouseY = _lookInput.y * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }
    }
}