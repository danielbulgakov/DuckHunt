using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameBehaviourScript : MonoBehaviour
{
    public float spawnSpeed = 3;
    private int BulletCount = 3;

    public GameObject Duck;
    public GameObject Table;
    public GameObject Bullets;
    public GameObject GameOverScreen;
    public GameObject Roof;

    public TMP_Text ScoreTXT; 


    public Texture2D CrosshaitCursor;

    public int ObjCount = 0;
    public int Score = 0;

    private bool GameOver = false;
    private bool MouseClicked = false;
    private Vector2 clickedPos;
    public float DuckDelete = 5f;

    float position = 9.2f;
    float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.gameObject.SetActive(false);
        Cursor.SetCursor(CrosshaitCursor, new Vector2(CrosshaitCursor.width / 2, CrosshaitCursor.height / 2), CursorMode.Auto);
        ScoreTXT.text = "0";
    }

    public void SpawnDuck()
    {

        if (ObjCount != 0) return;
        else ObjCount++;
        float newScale = Random.Range(0.7f, 1.8f);
        Vector3 RandPosRight = new Vector3(position, Random.Range(-0.3f, 0.6f));
        GameObject NewDuck = Instantiate(Duck, RandPosRight, Quaternion.identity) as GameObject;
        NewDuck.transform.localScale = new Vector3(newScale, newScale, 1f);
        NewDuck.SendMessage("SetDirection", direction, SendMessageOptions.DontRequireReceiver);
        direction *= -1;
        position *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTXT.text = Score.ToString();
        if (!GameOver)
        {
            SpawnDuck();

            MouseClicked = Input.GetMouseButtonDown(0);

            if (MouseClicked)
            {
                clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(clickedPos, clickedPos, 0f);

                if (hit.transform != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Duck"))
                {
                    hit.collider.SendMessage("DuckDead", SendMessageOptions.DontRequireReceiver);

                    ObjCount--;
                    Bullets.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    Bullets.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    Bullets.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    BulletCount = 3;
                    Score += 20;


                }
                else
                {
                    Bullets.gameObject.transform.GetChild(BulletCount - 1).gameObject.SetActive(false);
                    Bullets.gameObject.transform.GetChild(BulletCount - 1).gameObject.SetActive(false);
                    Bullets.gameObject.transform.GetChild(BulletCount - 1).gameObject.SetActive(false);
                    BulletCount--;
                    Score -= 5;
                }
                

            }

            if (BulletCount == 0) GameOver = true;
            


        }
        else
        {
            GameOverScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (Score > 9999) Score = 0;
        
       


    }
    
   

}
