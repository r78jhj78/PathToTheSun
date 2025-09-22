using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    public float rayLength = 1f;
    public LayerMask playerLayer;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); 
            return;
        }

        transform.Translate(Vector2.down * moveDistance);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
}
