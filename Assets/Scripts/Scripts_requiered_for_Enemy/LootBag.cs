using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject dropedIteamPrefab; // Prefab for the dropped item

    public List<Loot> lootlist = new List<Loot>(); // List of loot items

    // Method to get a dropped item based on its drop chance
    private Loot GetDroppedIteam()
    {
        int randomNumber = Random.Range(1, 101); // Generate a random number between 1 and 100

        List<Loot> possiblIteam = new List<Loot>();

        // Iterate through the loot list and add items with drop chance less than or equal to the random number
        foreach (Loot item in lootlist)
        {
            if (randomNumber <= item.dropChance)
            {
                possiblIteam.Add(item);
            }
        }

        if (possiblIteam.Count > 0)
        {
            // If there are possible items to drop, select a random item from the list
            Loot dropIteam = possiblIteam[Random.Range(0, possiblIteam.Count)];
            return dropIteam;
        }

        Debug.Log("No loot dropped");
        return null;
    }

    // Method to instantiate the dropped loot at a specified spawn point
    public void InstatiateLoot(Vector3 spawnpoint)
    {
        Loot dropedIteam = GetDroppedIteam();

        if (dropedIteam != null)
        {
            // Instantiate the loot game object and set its mesh and materials based on the dropped item
            GameObject lootGameObject = Instantiate(dropedIteamPrefab, spawnpoint, Quaternion.identity);
            lootGameObject.GetComponent<MeshFilter>().sharedMesh = dropedIteam.lootModel.GetComponent<MeshFilter>().sharedMesh;
            lootGameObject.GetComponent<MeshRenderer>().sharedMaterials = dropedIteam.lootModel.GetComponent<MeshRenderer>().sharedMaterials;

            // Set the values of the collision detector component on the loot game object
            lootGameObject.GetComponent<CollisionDetector>().setValues(dropedIteam.attributAffected, dropedIteam.attributeChange);
        }
    }
}
