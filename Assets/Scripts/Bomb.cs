using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damageAmount = 10;
    public float radius = 5f;
    public GameObject explosionEffect;

    private bool exploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !exploded)
        {
            // Получаем все коллайдеры объектов в радиусе поражения
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                // Если объект имеет компонент PlayerHealth, наносим ему урон
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }

            // Воспроизводим анимацию взрыва
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Уничтожаем анимацию взрыва через заданное время
            Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            // Уничтожаем бомбу
            Destroy(gameObject);

            exploded = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Рисуем сферу для отображения радиуса поражения в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}