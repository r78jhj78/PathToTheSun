using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public Vector2 direction = Vector2.down;
    public float distance = 0.5f;
    public LayerMask targetLayer;
    public int damage = 1;
    public float attackCoolDown = 1.5f;
    public float currentColDown;
    // Start is called before the first frame update
    void Start()
    {
        currentColDown = 0f;   
    }

    // Update is called once per frame
    void Update()
    {
        currentColDown -= Time.deltaTime;
        Debug.DrawRay(transform.position,direction.normalized*distance,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction.normalized,distance,targetLayer); 
        if (hit.collider != null && currentColDown <= 0f)
        {
            Player heald = hit.collider.GetComponent<Player>();
            if (heald != null)
            {
                heald.TakeDamage(damage);
                currentColDown = attackCoolDown;
            }
        }
    }
}
