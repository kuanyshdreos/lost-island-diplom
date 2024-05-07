using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Скорость перемещения

    private Rigidbody2D rb; // Ссылка на компонент Rigidbody2D
    private Animator animator; // Ссылка на компонент Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Проверка наличия компонентов
        if (rb == null)
            Debug.LogError("Rigidbody2D component is missing from this GameObject");
        
        if (animator == null)
            Debug.LogError("Animator component is missing from this GameObject");
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Проверяем, происходит ли движение
        if (Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveY) > 0.1f)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);

            // Изменяем направление спрайта в зависимости от направления движения
            transform.localScale = new Vector3(Mathf.Sign(moveX) * -1, 1, 1);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
        }
    }

    void FixedUpdate()
    {
        // Применение движения к Rigidbody2D
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(moveX, moveY) * speed;
        rb.velocity = moveVector;
    }

    void OnCollisionEnter(Collision collision) {
    Debug.Log("Collided with " + collision.gameObject.name);
    }


}
