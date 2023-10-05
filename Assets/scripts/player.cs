using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    GameObject currentFloor;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] GameObject replayButton;
    [SerializeField] Text scoreText;

    int score;
    float scoreTime;
    Animator anim;
    SpriteRenderer render;
    AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0;
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            render.flipX = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            render.flipX = false;
            
        }
        if ( Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        
        UpdateScore();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Normal")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("碰到了Normal!");
                currentFloor = other.gameObject;
                ModifyHp(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
           
        }
        if (other.gameObject.tag == "Nails")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("碰到了Nails!");
                currentFloor = other.gameObject;
                ModifyHp(-1);
                anim.SetTrigger("hurt");
                other.gameObject.GetComponent<AudioSource>().Play();
            }
           
        }
        if(other.gameObject.tag == "ceiling")
        {
            Debug.Log("碰到了ceiling!");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
             anim.SetTrigger("hurt");
            other.gameObject.GetComponent<AudioSource>().Play();
        }
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "DeathLine")
        {
            Debug.Log("你輸了");
            Die();
        }
    }
    void ModifyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        if (Hp <= 0)
        {
            Hp = 0;
            Die();
           
        }
        UpdateHpBar();
    }
    void UpdateHpBar()
    {
        for(int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = "地下" + score.ToString() + "層";
        }

    }
    void Die()
    {
        deathSound.Play();
        Time.timeScale = 0f;
        replayButton.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    public void s()
    {

    }
}
