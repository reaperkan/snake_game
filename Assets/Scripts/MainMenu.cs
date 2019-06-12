using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button topScoreButton;
    private bool show;
    void Start()
    {
        playButton.onClick.AddListener(()=>{PlayGame();});   
        topScoreButton.onClick.AddListener(()=>{ShowScore();});
        if(!PlayerPrefs.HasKey("Score")){
            PlayerPrefs.SetFloat("Score",-1);
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Delete)){
            PlayerPrefs.DeleteAll();
        }
    }
    void PlayGame(){
        SceneManager.LoadScene("GameScene");
    }
    void ShowScore(){
        show=!show;
        if(show){
            float score=PlayerPrefs.GetFloat("Score");
            topScoreButton.transform.GetComponentInChildren<Text>().text=score==-1?"Sorry No High Score":"HS :"+score;
        }else{
            topScoreButton.transform.GetComponentInChildren<Text>().text="Score";
        }
    }
}
