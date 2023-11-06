using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameObject gm = null;
    private float timer;
    public GameObject explote;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        timer = 0.0f;
        gm = GameObject.FindGameObjectWithTag("GameManager");
        if (gm == null){
            Debug.Log("No hi ha cap GameObject amb el tag GameManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        this.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.green, Color.red, timer / 4.0f);
    
        if (timer >= 4.0f){
            DestroyBomb();
        }
    }

    void OnMouseDown()
    {
        gm.GetComponent<GameManager>().AddScore();
        gm.GetComponent<GameManager>().BombDesactivate();
        Destroy(this.gameObject);
    }

    public void DestroyBomb()
    {
        gm.GetComponent<GameManager>().ExplotionsSounds();
        GameObject.Instantiate(explote, new Vector3(0, 0, 0), Quaternion.identity);
        gm.GetComponent<GameManager>().TakeDamage();
        Destroy(this.gameObject);
    }

}
