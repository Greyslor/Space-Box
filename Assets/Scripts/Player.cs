using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento del jugador
    public Transform groundCheck;   // Objeto usado para detectar el suelo
    public LayerMask groundLayer;   // Capa que representa el suelo
    public float groundCheckRadius = 0.2f; // Radio para la detección de suelo
    public Transform ceilingCheck;  // Objeto usado para detectar el techo
    public LayerMask ceilingLayer;  // Capa que representa el techo
    public float minX;              // Límite mínimo en el eje X
    public float maxX;              // Límite máximo en el eje X
    public float floorMinY;         // Límite mínimo en el eje Y (suelo)
    public float floorMaxY;         // Límite máximo en el eje Y (suelo)
    public float ceilingMinY;       // Límite mínimo en el eje Y (techo)
    public float ceilingMaxY;       // Límite máximo en el eje Y (techo)

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingCeiling;
    private bool isOnCeiling; // Flag para rastrear si el jugador está en el techo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Detectar si el jugador está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Detectar si el jugador toca el techo
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, ceilingLayer);

        // Cambiar entre el suelo y el techo al hacer el salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Cambio de techo a suelo y viceversa");
            isOnCeiling = !isOnCeiling;
        }

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Limitar posición dentro del área delimitada
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);

        // Si el jugador está en el techo, invertir la escala vertical
        if (isOnCeiling)
        {
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, ceilingMinY, ceilingMaxY);
            transform.localScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z);
        }
        else
        {
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, floorMinY, floorMaxY);
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }

        transform.position = clampedPosition;
    }
}
