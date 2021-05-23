using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofBehaviour : MonoBehaviour
{
    public GameObject GameOverScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
   
}
