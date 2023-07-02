using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot: ScriptableObject
{
    /**
     * Scitable object representing a loop object
     * 
     * This is used to hold diffrent lootitems which are then coined to a prefab if they get istantiated
     */
    public GameObject lootModel; // The model of the loot
    public string lootName; // The name of the loot
    public string attributAffected; // The attribute affected by the loot
    public float attributeChange; // The amount of attribute change caused by the loot

    public int dropChance; // The chance of the loot to drop

    public Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
