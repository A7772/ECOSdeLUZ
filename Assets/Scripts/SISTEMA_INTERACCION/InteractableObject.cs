using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractiveObject : MonoBehaviour
{
    [Header("Configuración del Objeto")]
    public string objectName;
    [TextArea(3, 10)] // Esto hace que el cuadro de texto en Unity sea más cómodo
    public string interactionMessage = "Has interactuado con el objeto";

    [Header("Feedback Visual")]
    [SerializeField] private Color hoverColor = Color.yellow;

    private Color _originalColor;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
        }
    }

    // Detecta cuando el ratón pasa por encima
    private void OnMouseEnter()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = hoverColor;
        }
    }

    // Detecta cuando el ratón sale
    private void OnMouseExit()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _originalColor;
        }
    }

    // Detecta el clic
    private void OnMouseDown()
    {
        // Verificación de seguridad: ¿está el ratón sobre un elemento de UI?
        if (UnityEngine.EventSystems.EventSystem.current != null &&
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return; // Si clicamos en un botón del menú, no interactuamos con el objeto
        }

        DoInteraction();
    }

    private void DoInteraction()
    {
        Debug.Log($"[Interacción] {objectName}: {interactionMessage}");
        // Aquí es donde en el futuro llamarás a tu InteractionManager
    }
}

