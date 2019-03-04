using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeRedScript : MonoBehaviour
{
    public GameObject player;
    
    private float timeLeft = 5f;
    
#region Unused Cube Timer UI Variables
    //public GameObject cubeTimer;
    //public TextMeshProUGUI cubeTimerText;
    //public float xOffset = 81f;
    //public float yOffset = 10f;
    //private int wholeTime;
    //public Camera mainCam;
    //public RectTransform cubeTimerRect;
    //public Canvas canvas;
#endregion
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerRed");
        //ConvertUIPos();
    }

    // Update is called once per frame
    void Update()
    {
        CubeCountdown();
        CubeFail();
    }

    private void OnTriggerEnter(Collider other) //if hit by another collider
    { 
        //Debug.Log("hit");
        if (other.gameObject == player) //and that collider belongs to the correct player
        {
            Destroy(gameObject); //destroy this cube and
            GameManager.instance.Health += 5;
            GameManager.instance.Score++;
            //Destroy(cubeTimer); //destroy this timer
        }
    }

#region Unused ConvertUIPos Method
    /*private void ConvertUIPos()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>(); //initialize RectTransform variable

        Vector2 viewportPos = mainCam.WorldToViewportPoint(transform.position); //convert camera pos to world pos
        Vector2 cubeScreenPos = new Vector2(
            (((viewportPos.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*0.5f)) + xOffset), //convert world pos to canvas pos plus slight offset
            (((viewportPos.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f)) + yOffset)); //not sure why the offset is necessary, but it is
        cubeTimerRect.anchoredPosition = cubeScreenPos; //position cube timer on canvas according to world pos of cube
    }*/
#endregion

    private void CubeCountdown() //countdown timer for cube
    {
        timeLeft -= Time.deltaTime; //countdown in seconds starting from timeLeft
        //wholeTime = (int) timeLeft; //convert to whole number
        //cubeTimerText.text = " " + wholeTime; //display time left in UI text
    }
    
    private void CubeFail()
    {
        if (timeLeft <= 0) //if timeLeft runs out
        {
            Destroy(gameObject); //destroy the cube and
            GameManager.instance.Health -= 5;
            //Destroy(cubeTimer); //destroy cube timer
        }
    }
}
