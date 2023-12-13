using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Score : MonoBehaviourPun
{

    //public Transform player;
    public TextMeshProUGUI scoreText;
    private float startTime;


    // Update is called once per frame
    [PunRPC]
    void Update()
    {
        scoreText.text = (Time.time).ToString("F2"); ;
    }

    [PunRPC]
    public void Begin()
    {
        startTime = Time.time;
    }
}
