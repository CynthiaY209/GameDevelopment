using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    private Vector2 targetPos;
    public float Ystep = 5;
    public float Xstep = 5;

    public int moveSpeed = 50;
    public float maxHeight = 5;
    public float minHeight = -5;

    public float health = 5;
    public Animator camAnim;

    public Text healthDisplay;

    public GameObject moveEffect;

    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = health.ToString();

        if(health<=0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameOver.SetActive(true);
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if ((Input.GetKeyDown(KeyCode.UpArrow ) || Input.GetKeyDown(KeyCode.W)) && transform.position.y < maxHeight)
        {
            camAnim.SetTrigger("shake");
            Instantiate(moveEffect, transform.position, Quaternion.identity);
            targetPos = new Vector2(transform.position.x, transform.position.y + Ystep);
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))  && transform.position.y > minHeight)
        {
            camAnim.SetTrigger("shake");
            Instantiate(moveEffect, transform.position, Quaternion.identity);
            targetPos = new Vector2(transform.position.x, transform.position.y - Ystep);
        }
    }
}
