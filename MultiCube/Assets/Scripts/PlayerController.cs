using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public PlayerController player;
    public GameManager gameManager;
    [Header("Info")]
    public int id;

    [Header("Components")]
    public Rigidbody rig;
    public Player photonPlayer;
    //public PlayerWeapon weapon;

    // CubeGame Code 

    public Rigidbody rb;


    public float fowardforce = 2000f;
    public float sidewaysforce = 100f;

    [PunRPC]
    public void Initialize(Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;

        // is this not our local player?
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            rig.isKinematic = true;
        }
        else 
        {
            //GameUI.instance.Initialize(this);
        }
    }

    public int GetPlayerID()
    {
        Debug.Log("Player id in player controller = " + id);
        return id;
    }

    void Update()
    {
        Move();
    }

    [PunRPC]
    void Move() 
    {
       
        if (id % 2 == 0) 
        {
            rb.AddForce(fowardforce * Time.deltaTime, 0, 0);

            if (Input.GetKey("d"))
            {
                rb.AddForce(0, 0, sidewaysforce * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(0, 0, -sidewaysforce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        else
        {
            rb.AddForce(0, 0, fowardforce * Time.deltaTime);

            if (Input.GetKey("d"))
            {
                rb.AddForce(sidewaysforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(-sidewaysforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }

        if (rb.position.y < -2f && photonView.IsMine)
        {
            Debug.Log("Player #" + id + " fell off the map");
            EndGame();
        }
    }

    [PunRPC]
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");

            if (photonView.IsMine)
            {
                Debug.Log("Try to restart game for Player #" + id);
                Invoke("Restart", restartDelay);
            }
            
        }
    }

    [PunRPC]
    void Restart()
    {
        Debug.Log("Restart game for Player #" + id);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player = GameManager.instance.GetPlayer(id);
        player.transform.position = GameManager.instance.spawnPoints[id-1].position;
        player.transform.rotation = Quaternion.identity;
        player.enabled = true;
        gameHasEnded = false;
    }
}
