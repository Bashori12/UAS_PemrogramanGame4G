using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text Score;
    public Text timeGame;
    private int textScore = 0;
    public float countdown = 90f;
    public GameObject gameOver;
    public GameObject paused;
    public Text HighScore;
    bool canHit;

    private void Start()
    {
        canHit = true;
        Score = GameObject.Find("Score").GetComponent<Text>();
        Score.text = "Score : 0";
        timeGame = GameObject.Find("Time").GetComponent<Text>();
        timeGame.text = "Time Left : 0:00";
    }

    void Update()
    {
        float minutes = Mathf.FloorToInt(countdown / 60);
        float seconds = Mathf.FloorToInt(countdown % 60);
        timeGame.text = string.Format("Time Left : {0:00}:{1:00}", minutes, seconds);
        if(countdown > 0){
            countdown -= Time.deltaTime;
            if(countdown <= 0){
                Over();
                HighScore.text = "HighScore : " + Score.text;
            }
        }


        // if(canHit){
        //     if((Input.GetTouch(0).phase == TouchPhase.Stationary) || (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.magnitude < 1.2f)){
        //     Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //     RaycastHit hit;
        //     if(Physics.Raycast(ray, out hit)){
        //         Score.text = "Score : " + textScore;
        //         CapsuleCollider cc = hit.collider as CapsuleCollider;
        //             if(cc != null){
        //                 textScore += 10;
        //             }
        //         }
        //     }
        // }

        if(canHit){
            if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                Score.text = "Score : " + textScore;
                CapsuleCollider cc = hit.collider as CapsuleCollider;
                    if(cc != null){
                        textScore += 10;
                    }
                }
            }
        }

    }

    public void PausedGame(){
        paused.SetActive(true);
        Time.timeScale = 0;
        canHit = false;
    }

    public void ResumeGame(){
        paused.SetActive(false);
        Time.timeScale = 1;
        canHit = true;
    }

    public void Over(){
        gameOver.SetActive(true);
        Time.timeScale = 0;
        canHit = false;
    }
}
