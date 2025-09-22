using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAtttack : MonoBehaviour
{
    public Animator animator;
    public float distanceRaycast = 0.1f;
    private float cooldownAttack = 0.1f;
    private float actualCooldownAttack;
    public GameObject beeBullet;
    public LayerMask playerLayer;
    public int damage = 1;
    void Start()
    {
        actualCooldownAttack = -1f;
    }

    void Update()
    {
        actualCooldownAttack -= Time.deltaTime;
        Debug.DrawRay(transform.position,Vector2.down,Color.red,distanceRaycast);
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, distanceRaycast, playerLayer);

        if (hit2D.collider != null && hit2D.collider.CompareTag("Player") && actualCooldownAttack <= 0f)
        {
            animator.Play("AttackBee");
            Invoke("LaunchBullet", 0.5f);
            actualCooldownAttack = cooldownAttack;
        }
    }
    void LaunchBullet()
    {
        GameObject newBullet;
        newBullet = Instantiate(beeBullet,transform.position,transform.rotation);
    }
}
