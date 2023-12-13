using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    //bool gameHasEnded = false;
    //public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public PlayerController playerController;
    //public Player photonPlayer;

    [Header("Players")]
    public string playerPreFabLocation;
    public string[] playerPreFabLocations;
    public PlayerController[] players;
    public Transform[] spawnPoints;
    public int alivePlayers;


    private int playersInGame;

    public float postGameTime;

    // instance
    public static GameManager instance;

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    /*
    [PunRPC]
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }
    */

    /*
    [PunRPC]
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    */

    // Multiplayer
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        alivePlayers = players.Length;

        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;

        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
            photonView.RPC("SpawnPlayer", RpcTarget.All);
    }

    [PunRPC]
    void SpawnPlayer()
    {
        Debug.Log("Player id in Game manager = " + PhotonNetwork.LocalPlayer.ActorNumber);
        GameObject playerObj = PhotonNetwork.Instantiate(playerPreFabLocations[(PhotonNetwork.LocalPlayer.ActorNumber-1)], spawnPoints[(PhotonNetwork.LocalPlayer.ActorNumber - 1)].position, Quaternion.identity);/*Random.Range(0, spawnPoints.Length)].position, Quaternion.identity)*/

        // initialze the player for all players
        playerObj.GetComponent<PlayerController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    public PlayerController GetPlayer(int playerId)
    {
        foreach (PlayerController player in players)
        {
            if (player != null && player.id == playerId)
                return player;
        }

        return null;
    }

    public PlayerController GetPlayer(GameObject playerObject)
    {
        foreach (PlayerController player in players)
        {
            if (player != null && player.gameObject == playerObject)
                return player;
        }

        return null;
    }

    /*
    public void CheckWinCondition()
    {
        if (alivePlayers == 1)
            photonView.RPC("WinGame", RpcTarget.All, players.First(x => !x.dead).id);
    }
    */

    [PunRPC]
    void WinGame(int winningPlayer)
    {
        // set the UI win text
        //GameUI.instance.SetWinText(GetPlayer(winningPlayer).photonPlayer.NickName);

        Invoke("GoBackToMenu", postGameTime);
    }

    void GoBackToMenu()
    {
        NetworkManager.instance.ChangeScene("Menu");
    }
}
