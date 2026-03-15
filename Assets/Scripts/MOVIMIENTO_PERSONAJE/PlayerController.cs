
using UnityEngine;

// Asegúrate de que el archivo se llame exactamente PlayerController.cs
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.1f;

    private Vector2 _targetPosition;
    private Rigidbody2D _rb;
    private bool _isMoving = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void SetTargetPosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        _isMoving = true;
        FlipCharacter();
    }

    private void MovePlayer()
    {
        if (!_isMoving) return;

        float distanceToTarget = Vector2.Distance(_rb.position, _targetPosition);

        if (distanceToTarget > stopDistance)
        {
            Vector2 direction = (_targetPosition - _rb.position).normalized;

            // Usamos linearVelocity para versiones modernas de Unity (evita el aviso CS0618)
            _rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            _isMoving = false;
        }
    }

    private void FlipCharacter()
    {
        if (_targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
