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

        [Tooltip("Mouse Delta için (dt ile çarpılmaz). 0.05 - 0.25 aralığı genelde iyi başlar.")]
        [SerializeField] private float _mouseSensitivity = 0.12f;

        [Tooltip("0 = smoothing kapalı. 10-25 arası yumuşak his verir.")]
        [SerializeField] private float _lookSmoothing = 25f;

        [SerializeField] private float _maxLookAngle = 80f;

        [Header("Zemin Kontrolü")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundMask;

        private CharacterController _controller;
        private InputSystem_Actions _inputActions;

        private Vector2 _moveInput;
        private Vector2 _lookInputRaw;
        private Vector2 _lookInputSmoothed;

        private Vector3 _velocity;
        private float _currentSpeed;

        private float _xRotation;
        private bool _isGrounded;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
            _inputActions.Player.Jump.performed += OnJumpPerformed;
        }

        private void OnDisable()
        {
            _inputActions.Player.Jump.performed -= OnJumpPerformed;
            _inputActions.Player.Disable();
        }

        private void Update()
        {
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

            // Bu değer Mouse Delta: dt ile tekrar çarpılmaz.
            _lookInputRaw = _inputActions.Player.Look.ReadValue<Vector2>();

            SmoothLookInput();

            RotateBody();
            CalculateGravity();
            MovePlayer();
        }

        private void LateUpdate()
        {
            RotateHead();
        }

        private void SmoothLookInput()
        {
            if (_lookSmoothing <= 0f)
            {
                _lookInputSmoothed = _lookInputRaw;
                return;
            }

            // frame-rate bağımsız smoothing
            float t = 1f - Mathf.Exp(-_lookSmoothing * Time.deltaTime);
            _lookInputSmoothed = Vector2.Lerp(_lookInputSmoothed, _lookInputRaw, t);
        }

        private void CalculateGravity()
        {
            if (_groundCheck != null)
                _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask, QueryTriggerInteraction.Ignore);
            else
                _isGrounded = _controller.isGrounded;

            if (_isGrounded && _velocity.y < 0f)
                _velocity.y = -2f;

            _velocity.y += _gravity * Time.deltaTime;
        }

        private void MovePlayer()
        {
            bool isMovingForward = _moveInput.y > 0f;
            bool hasMovementInput = _moveInput.sqrMagnitude > 0.0001f;
            bool isSprinting = _inputActions.Player.Sprint.IsPressed() && isMovingForward;

            if (hasMovementInput)
            {
                float targetSpeed = isSprinting ? _runSpeed : _walkSpeed;
                _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * _speedTransitionSmoothing);
            }
            else
            {
                _currentSpeed = 0f;
            }

            Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;

            if (moveDirection.sqrMagnitude > 1f)
                moveDirection.Normalize();

            Vector3 finalMovement = moveDirection * _currentSpeed;
            finalMovement.y = _velocity.y;

            _controller.Move(finalMovement * Time.deltaTime);
        }

        private void RotateBody()
        {
            float mouseX = _lookInputSmoothed.x * _mouseSensitivity;
            transform.Rotate(Vector3.up * mouseX, Space.Self);
        }

        private void RotateHead()
        {
            if (_cameraTransform == null) return;

            float mouseY = _lookInputSmoothed.y * _mouseSensitivity;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -_maxLookAngle, _maxLookAngle);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (_isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
}