using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public GameObject bulletPrefab;//bullet
    public float moveSpeed;//toc do tien ve phia finish

    public GameObject panalGameOver;//UI Game over
    public GameObject panalGameWin;//UI Game win
    public GameObject firePrefab;//fire when player is destroied

    public float timeCreateBulletMax;//time moi khi create a new bullet
    private float timeCreateBullet;//time cowndown 

    private bool isMouseDown;
    private Vector3 positionEnd;
    private Quaternion rotationEnd;
    private Plane plane = new Plane(Vector3.up, 0);//phuc vu cho ray casting

    void Start()
    {
        rotationEnd = transform.rotation;
        positionEnd = transform.position;
    }

    void Update()
    {
        if (GameLogic.STATUS != 0) return;
        
        shoot();
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
        if (isMouseDown)
        {
            mouseUpdate();
        }
        else
        {
            rotationEnd = Quaternion.identity;
        }
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        transform.position = Vector3.Slerp(transform.position, new Vector3(positionEnd.x,positionEnd.y,transform.position.z),10*Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationEnd, 10 * Time.deltaTime);
    }

    private void shoot()
    {
        timeCreateBullet -= Time.deltaTime;
        if (timeCreateBullet < 0)
        {
            timeCreateBullet = timeCreateBulletMax;
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
            GameObject bulletRight = Instantiate(bulletPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            Bullet bulletRightScript = bulletRight.GetComponent<Bullet>();
            bulletRightScript.angle = 30f;
            GameObject bulletLeft = Instantiate(bulletPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
            Bullet bulletLeftScript = bulletLeft.GetComponent<Bullet>();
            bulletLeftScript.angle = -30f;
        }
    }

    private void mouseUpdate()
    {
        float z = transform.position.z;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray,out float distance))
        {
            positionEnd = ray.GetPoint(distance);
        }
        positionEnd.z = z;
        if (positionEnd.x < -4)
            positionEnd.x = -4;
        else if (positionEnd.x > 4)
            positionEnd.x = 4;
        if (transform.position.x < positionEnd.x)
        {
            rotationEnd = Quaternion.AngleAxis(30, Vector3.up);
        }
        else if (transform.position.x > positionEnd.x)
        {
            rotationEnd = Quaternion.AngleAxis(-30, Vector3.up);
        }
        else
        {
            rotationEnd = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    /// <summary>
    /// Player va cham voi Enemy
    /// Player va cham voi vach dich Finish
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.layer)
        {
            case 7:
                GameLogic.STATUS = -1;
                Destroy(gameObject);
                Instantiate(firePrefab, transform.position, Quaternion.identity);
                for (int i = 0; i < 3; i++)
                    DestroyEffectPooling.Instance.spawnFromPool("PlayerDestroyItem", transform.position, Quaternion.identity);
                panalGameOver.SetActive(true);
                break;
            case 9:
                GameLogic.STATUS = 1;
                panalGameWin.SetActive(true);
                break;
        }
    }

}
