using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Score : MonoBehaviourPun, IPunObservable
{

    //public Transform player;
    public TextMeshProUGUI scoreText;
    private float startTime;
    private float timeTaken;

    [PunRPC]
    public void Start()
    {
        startTime = Time.time;
    }

 
    [PunRPC]
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            scoreText.text = (Time.time - startTime).ToString("F2"); ;
        }
        
        
    }

    public void End()
    {
        timeTaken = Time.time - startTime;

        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        //Leaderboard.instance.SetLeaderboardEntry(scoreText.text);
    }
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(scoreText.text);
        }
        else if (stream.IsReading)
        {
            scoreText.text = (string)stream.ReceiveNext();
        }
    }
    
}
