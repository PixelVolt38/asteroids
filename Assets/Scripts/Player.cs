using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float rotationSpeed = 5f;
    public float movementSpeed = 5f;
    public GameObject bulletPrefab;
    public GameObject bulletSpawner;


    public float xBorderLimit = 10f;
    public float yBorderLimit = 6f;

    public static int SCORE = 0;

    private Rigidbody2D _rigid;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
        {
            newPos.x = -xBorderLimit;
        }
        else if (newPos.x < -xBorderLimit)
        {
            newPos.x = xBorderLimit;
        }
        else if (newPos.y > yBorderLimit)
        {
            newPos.y = -yBorderLimit;
        }
        else if (newPos.y < -yBorderLimit)
        {
            newPos.y = yBorderLimit;
        }

        transform.position = newPos;
        
        float thrust = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        Vector2 movementDirection = transform.right;
        _rigid.AddForce(thrust * movementDirection);

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, -rotation);

        if(Input.GetKeyDown(KeyCode.Space)){
            /*GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, Quaternion.identity);

            Bullet bala = bullet.GetComponent<Bullet>();

            bala.targetVector = transform.right;*/
            if (Input.GetKeyDown(KeyCode.Space)){
                //GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, Quaternion.identity);//giro ninguno
                GameObject bullet = BulletPooling.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = bulletSpawner.transform.position;//sale desde la posicion del bulletSpawner
                    bullet.SetActive(true);//activo una bala
                    bullet.GetComponent<Bullet>().targetVector = transform.right;//sale con movimiento
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Fragment"))
        {
            Reload();
        }
    }

    public void Reload()
    {
        SCORE = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
