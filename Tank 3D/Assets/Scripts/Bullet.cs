using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float angle;
    public float speed;
    public Text txtScore;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, 0, speed*Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        if (transform.position.z > Camera.main.transform.position.z + 100)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Bullet va cham voi Enemy
    /// Bullet va cham voi Le Trai Le Phai
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.layer) {
            case 7:
                Destroy(gameObject);
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.destroy();
                for (int i = 0; i < 3; i++)
                    DestroyEffectPooling.Instance.spawnFromPool("BulletDestroyItem", transform.position, Quaternion.identity);
                DataSaver.SCORE++;
                break;
            case 8:
                Bullet bullet = gameObject.GetComponent<Bullet>();
                bullet.angle = -bullet.angle;
                break;
        }
    }

}
