using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    //public Transform player;
    public TextMeshProUGUI scoreText;
    private float startTime;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = (Time.time).ToString("F2"); ;
    }

    public void Begin()
    {
        startTime = Time.time;
    }
}
