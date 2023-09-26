using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;
    public Vector3 targetVector;
    
    public float xBorderLimit = 10f;
    public float yBorderLimit = 6f;

    public float fragDisplace = 10f;
    public float fragVelocity = 50f;
    
    public GameObject fragPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
        {
            gameObject.SetActive(false);
        }
        else if (newPos.x < -xBorderLimit)
        {
            gameObject.SetActive(false);
        }
        else if (newPos.y > yBorderLimit)
        {
            gameObject.SetActive(false);
        }
        else if (newPos.y < -yBorderLimit)
        {
            gameObject.SetActive(false);
        }
        
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            gameObject.SetActive(false);
            Destroy(other.gameObject);

            Fragments(other.gameObject.transform.position);
        }

        if (other.gameObject.CompareTag("Fragment"))
        {
            IncreaseScore();
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Score");
        go.GetComponent<Text>().text = "Score : " + Player.SCORE;
    }

    void Fragments(Vector3 spawnPosition)
    {
        GameObject frag1 = Instantiate(fragPrefab, new Vector3(spawnPosition.x-fragDisplace,spawnPosition.y,spawnPosition.z), Quaternion.Euler(0,0,-45)*transform.rotation);
        frag1.GetComponent<Rigidbody2D>().velocity = new Vector2(-fragVelocity*Time.deltaTime,-fragVelocity*Time.deltaTime);
        GameObject frag2 = Instantiate(fragPrefab, new Vector3(spawnPosition.x+fragDisplace,spawnPosition.y,spawnPosition.z), Quaternion.Euler(0,0,45)*transform.rotation);
        frag2.GetComponent<Rigidbody2D>().velocity = new Vector2(fragVelocity*Time.deltaTime,-fragVelocity*Time.deltaTime);
    }
}
