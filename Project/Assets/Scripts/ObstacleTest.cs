using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5;

    public GameObject effect;
    public GameObject sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            //other.GetComponent<PlayerTest>().camAnim.SetTrigger("shake");
            Instantiate(sound, transform.position, Quaternion.identity);
            other.GetComponent<PlayerTest>().health -= damage;
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
