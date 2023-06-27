using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    [Header("Current Values")]
    public Dictionary<string, float> variables = new Dictionary<string, float>();

    [Header("Attribute Options")]
    public bool enableHealthregen = false;
    public bool eneableLifesteal = false;

    [Header("Attributes")]
    public float health;
    public float attack;
    public float armor;
    public float criticalDamageMultiplyer;
    public float criticalChance;
    public float percentageLifesteal;
    public float lifeRegenRate;
    public float jumps;
    public float attackcooldown;

    [Header("References")]
    public Healthbar_Script healthbarScript;

    [Header("Status")]
    public bool dead = false;
    private bool healthregenActive = false;
    private float maxHealth;


    // Start is called before the first frame update
    public void Start()
    {
        // Set initial health value for the Healthbar_Script component
        healthbarScript.SetHealth(health);

        // Store the maximum health value
        maxHealth = health;

        try
        {
            // Get the number of jumps allowed from the PlayerMovment component and store it in the 'jumps' variable
            jumps = gameObject.GetComponent<PlayerMovment>().getJumpsAllowed();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString() + "Error in setting Jumps");
            // If an exception occurs (e.g., PlayerMovment component not found), set the 'jumps' variable to 1
            jumps = 1;
        }

        // Add attribute variables to the dictionary
        variables.Add("health", health);
        variables.Add("attack", attack);
        variables.Add("armor", armor);
        variables.Add("criticalDamageMultiplyer", criticalDamageMultiplyer);
        variables.Add("criticalChance", criticalChance);
        variables.Add("percentageLifesteal", percentageLifesteal);
        variables.Add("lifeRegenRate", lifeRegenRate);
        variables.Add("jumps", jumps);
        variables.Add("attackcooldown", attackcooldown);
    }

    // Update attribute variables from the dictionary
    public void UpdateVariables()
    {
        float temp;

        // Retrieve the value of each attribute from the dictionary and update the corresponding variable
        variables.TryGetValue("health", out temp);
        health = temp;
        variables.TryGetValue("attack", out temp);
        attack = temp;
        variables.TryGetValue("armor", out temp);
        armor = temp;
        variables.TryGetValue("criticalDamageMultiplyer", out temp);
        criticalDamageMultiplyer = temp;
        variables.TryGetValue("criticalChance", out temp);
        criticalChance = temp;
        variables.TryGetValue("percentageLifesteal", out temp);
        percentageLifesteal = temp;
        variables.TryGetValue("lifeRegenRate", out temp);
        lifeRegenRate = temp;
        variables.TryGetValue("jumps", out temp);
        jumps = temp;
        variables.TryGetValue("attackcooldown", out temp);
        attackcooldown = temp;

        // Set the jumps allowed value in the PlayerMovment component
        gameObject.GetComponent<PlayerMovment>().setJumpsAllowed((int)temp);
    }

    // Update is called once per frame
    public void Update()
    {
        // Start or stop health regeneration based on the enableHealthregen flag
        if (enableHealthregen && !healthregenActive)
        {
            startHealthregen();
        }
        else if (!enableHealthregen && healthregenActive)
        {
            stopHealthregen();
        }
    }

    // Function to apply damage to the character
    public void TakeDamage(float damage)
    {
        // Reduce damage based on armor percentage
        Debug.Log("Get Damaged" + (damage - (damage * armor / 100)));
        health -= damage - (damage * armor / 100);
        healthbarScript.SetHealth(health);

        if (health <= 0)
        {
            // If health drops to or below 0, mark the character as dead, invoke the Despawn method after 5 seconds, and disable the health bar
            dead = true;
            Invoke("Despawn", 5f);
            healthbarScript.gameObject.SetActive(false);
        }
    }

    // Method to handle character despawning
    private void Despawn()
    {
        if (GetComponent<LootBag>() != null)
        {
            // If the character has the LootBag component, instantiate loot at the character's position
            GetComponent<LootBag>().InstatiateLoot(transform.position);
        }
        gameObject.SetActive(false);
    }

    // Coroutine for health regeneration
    private IEnumerator Healthregen()
    {
        float delay = 1f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            // Wait for the specified delay and then call the heal method
            yield return wait;
            heal(lifeRegenRate);
        }
    }

    // Method to heal the character
    private void heal(float amount)
    {
        if (health < maxHealth)
        {
            // If the current health is less than the maximum health, increase the health by the specified amount (limited to the maximum health)
            if (health + amount > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health = health + amount;
            }
        }
    }

    // Method to start health regeneration coroutine
    private void startHealthregen()
    {
        StartCoroutine(Healthregen());
    }

    // Method to stop health regeneration coroutine
    private void stopHealthregen()
    {
        StopCoroutine(Healthregen());
    }

    // Method to deal damage to a target object
    public void DealDamage(GameObject target)
    {
        AttributeManager atm;

        try
        {
            // Get the AttributeManager component from the target object
            atm = target.GetComponent<AttributeManager>();
        }
        catch (Exception e)
        {
            // If an exception occurs, log the error message and set atm to null
            Debug.Log(e);
            atm = null;
        }

        if (atm != null)
        {
            float totalDamage = attack;

            // Apply critical damage if the random value is less than the criticalChance
            if (UnityEngine.Random.Range(0f, 1f) < criticalChance)
            {
                totalDamage = totalDamage * criticalDamageMultiplyer;
            }

            if (eneableLifesteal)
            {
                // Apply lifesteal to the character
                heal((totalDamage * (100 / percentageLifesteal)));
            }

            // Deal damage to the target object
            atm.TakeDamage(totalDamage);
        }
    }
}