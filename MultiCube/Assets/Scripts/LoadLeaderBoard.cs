using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLeaderBoard : MonoBehaviour
{
    public Leaderboard leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Tried to Call LeaderBoard EndGame Function");
        leaderboard.OnGameEnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
