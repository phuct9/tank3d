using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static int STATUS = 100;
    //bien trang thai cua game
    //100: Game dang o man hinh top
    //0: Game is playing
    //-1: Gameover
    //1: Game win

    public GameObject player;
    public GameObject cFirst;//UI top
    public GameObject cGame;//UI game play
    public Text txtScore;
    //public GameObject enemyPrefab;

    private float timeCreateEnemy = 10;
    private Quaternion rotateCam;//goc quay cua camera
    private Vector3 positionCam;//vi tri cua camera
   
    void Start()
    {
        DataSaver.SCORE = 0;
        rotateCam = transform.rotation;
        positionCam = transform.position;
    }
   
    void Update()
    {
        timeCreateEnemy -= Time.deltaTime;
        if (timeCreateEnemy < 0)
        {
            timeCreateEnemy = 10;
            float zz = Camera.main.transform.position.z + 50;
            for (int xx = -4; xx < 5; xx++)
            {
                //GameObject obj = Instantiate(enemyPrefab, new Vector3(xx, 0, zz), Quaternion.identity);
                GameObject obj = DestroyEffectPooling.Instance.spawnFromPool("enemy", new Vector3(xx, 0, zz), Quaternion.identity);
            }
        }
        txtScore.text = "" + DataSaver.SCORE;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateCam, 2 *Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, positionCam, 2*Time.deltaTime);
        if (GameLogic.STATUS == 0)
        {
            positionCam = player.transform.position + new Vector3(0, 12, -8);
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    /// <summary>
    /// khi click start play game button
    /// </summary>
    public void playStart()
    {
        cFirst.SetActive(false);
        cGame.SetActive(true);
        rotateCam = Quaternion.AngleAxis(45, Vector3.right);
        STATUS = 0;
    }



}
