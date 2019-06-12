using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
public class FoodType{
    public Color color;
    public float score;
}
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject spawnPrefab;
    public Transform world;
    public float radius;
    public float roundScore=0;
    private Color previousColor=Color.black;
    private int streak=0;
    public List<FoodType> allfoodtypes=new List<FoodType>();
    public int startFood=10;

    [Header("UI")]
    public Text scoreText;
    public Text streakText;
    public GameObject gameOverHolder;
    public Text scoreHolder;
    public Text newHighScore;
    public Button doneButton;
    void Start(){
        gm=this;
        radius=world.localScale.x*world.GetComponent<SphereCollider>().radius;
        doneButton.onClick.AddListener(()=>{ReturnToMenu();});
        for(int i=0;i<startFood;i++)
            SpawnFood();
    }
    void Update(){
        scoreText.text="Score: "+roundScore;
        streakText.text="Streak: "+streak;
    }
    void SpawnFood(){
        Vector3 pos=GenerateRandomPos();
        GameObject g=Instantiate(spawnPrefab,pos,Quaternion.identity);
        int randomIndex=Random.Range(0,allfoodtypes.Count);
        g.GetComponent<Food>().SetValues(allfoodtypes[randomIndex].color,allfoodtypes[randomIndex].score,world.position);
    }
    Vector3 GenerateRandomPos(){
        Vector3 pos=Vector3.zero;
        do{
            pos=Random.insideUnitSphere*radius*2;
        }while(insideWorld(pos));
        return pos;
    }
    bool insideWorld(Vector3 pos){
        return Vector3.Distance(world.position,pos)<radius;
    }
    public void AddScore(Color color,float score){
        if(color==previousColor){
            streak++;
        }else{
            streak=1;
            previousColor=color;            
        }
        roundScore+=score*streak;
        SpawnFood();
    }
    public void Dead(){
        streak=0;
        gameOverHolder.SetActive(true);
        scoreHolder.text="Score :"+roundScore;
        if(PlayerPrefs.GetFloat("Score")<roundScore){
            newHighScore.text="Yay New highscore!";
            PlayerPrefs.SetFloat("Score",roundScore);
        }
    }
    void ReturnToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
