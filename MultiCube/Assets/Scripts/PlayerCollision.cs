using UnityEngine;
using Photon.Pun;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController controller;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "obstacle") 
        {
            controller.enabled = false;
            FindObjectOfType<PlayerController>().EndGame();
        }
    }
}
