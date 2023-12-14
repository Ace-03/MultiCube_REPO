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

/*
   void Update()
    {
        scoreText.text = (PhotonNetwork.Time).ToString("F2"); ;
    }

    [PunRPC]
    public void Begin()
    {
        startTime = PhotonNetwork.Time;
    }
}
*/