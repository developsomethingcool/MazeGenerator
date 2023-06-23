using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;
    //check for collision
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Trap0" || collisionInfo.collider.tag == "Trap1" || collisionInfo.collider.tag == "Trap2" || collisionInfo.collider.tag == "Trap3" || collisionInfo.collider.tag == "Trap4" || collisionInfo.collider.tag == "Trap5" || collisionInfo.collider.tag == "Trap6" || collisionInfo.collider.tag == "Trap7" || collisionInfo.collider.tag == "Trap8" || collisionInfo.collider.tag == "Trap9")
        {
            FindObjectOfType<GameManage>().EndGame();
            Debug.Log("Hit an obstacle!");
            movement.enabled = false;

        }
    }
}
