using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string targetTag = "Enemy"; // The tag of the target objects that can be attacked

    private float attackcooldown = 0; // Cooldown time between attacks

    public AttributeManager carryer; // The attribute manager of the character holding the weapon

    public Animator ani; // The animator component for playing attack animations

    private bool isCheckingCollision = false; // Flag to indicate if collision checking is active

    private bool isAllowedToAttack = true; // Flag to indicate if the weapon is allowed to attack

    public bool player = false; // Flag to indicate if the weapon belongs to a player character

    public bool doubleAxeUser; // Flag to indicate if the weapon is a double axe user

    private bool axeHit = false; // Flag to indicate if the axe hit a target

    private void Start()
    {
        if (player)
        {
            attackcooldown = carryer.attackcooldown; // Set the attack cooldown time for the player character
        }
    }

    public void Attack()
    {
        if (!doubleAxeUser)
        {
            isCheckingCollision = true; // Start checking for collisions with targets
        }

        if (axeHit)
        {
            dealDamageToPlayer(); // Deal damage to the player character if axe hit a target
            Invoke("ResetAxeHit", 1f); // Reset the axe hit flag after a delay
        }

        ani.SetTrigger("Attack"); // Trigger the attack animation
        Invoke("EndCollisionCheck", 1f); // Stop collision checking after a delay
    }

    private void Update()
    {
        if (player && Input.GetMouseButtonDown(1))
        {
            if (isAllowedToAttack)
            {
                isAllowedToAttack = false;
                isCheckingCollision = true; // Start checking for collisions with targets
                ani.SetTrigger("Attack"); // Trigger the attack animation
                Invoke("EndCollisionCheck", 1f); // Stop collision checking after a delay
                Invoke("ResetAttack", attackcooldown); // Reset the attack flag after the attack cooldown
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCheckingCollision)
        {
            Debug.Log(other.gameObject.tag);

            if (targetTag.Equals(other.gameObject.tag))
            {
                if (!player)
                {
                    dealDamageToPlayer(); // Deal damage to the player character if collided with an enemy
                }
                else
                {
                    carryer.DealDamage(other.gameObject); // Deal damage to the target enemy
                    isCheckingCollision = false; // Stop collision checking
                }
            }
            else
            {
                Debug.Log(other.gameObject.tag); // Log the tag of the collided object if it does not match the target tag
            }
        }
    }

    private void EndCollisionCheck()
    {
        isCheckingCollision = false; // Stop collision checking
    }

    private void ResetAttack()
    {
        isAllowedToAttack = true; // Reset the attack flag
    }

    private void ResetAxeHit()
    {
        axeHit = false; // Reset the axe hit flag
    }

    private void dealDamageToPlayer()
    {
        carryer.DealDamage(GameObject.Find("Player")); // Deal damage to the player character
    }

    public void AxeHit()
    {
        axeHit = true; // Set the axe hit flag
    }
}