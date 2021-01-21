using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoapyController : MonoBehaviour
{
    public Text score;
    public AudioSource BackgroundMusic;
    public AudioSource VictoryMusic;
    public AudioSource DefeatMusic;
    public float speed = 6.0f;
    private int scoreValue = 0;
    public Text winText;
    public Text loseText;
    public AudioClip Collectable; 
    AudioSource audioSource;
    public GameObject pickupPrefab;
    Rigidbody2D rigidbody2d;
    public float currentTime = 0f;
    public float startingTime = 12f;
    public Text timer;
    bool gameOver;
    public Text Ending;
    
    // Start is called before the first frame update
    void Start()
    {
        DefeatMusic.enabled = false;
        BackgroundMusic.enabled = false;
        VictoryMusic.enabled = false;
        currentTime = startingTime;
      score.text = scoreValue.ToString();
      winText.text = "";
      loseText.text = "";
      Ending.text = "";
      audioSource = GetComponent<AudioSource>();
      rigidbody2d = GetComponent<Rigidbody2D>();
   }
   // Update is called once per frame
   void Update()
   {
       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");
       Vector2 position = transform.position;
       position.x = position.x + speed * horizontal * Time.deltaTime;
       position.y = position.y + speed * vertical * Time.deltaTime;
       transform.position = position;
       currentTime -= 1 * Time.deltaTime;
       timer.text = currentTime.ToString("0");
       if (currentTime <= 10)
        {
            speed = 6.0f;
            BackgroundMusic.enabled = true;
            if (currentTime <=0)
       {
           currentTime = 0f;
           speed = 0.0f;
           BackgroundMusic.enabled = false;
           DefeatMusic.enabled = true;
           loseText.text = "Time's ran out! You got infected!";
           Destroy(loseText, 2);
           gameOver = true;
           Ending.text = "Press R to Restart or Esc to quit!";
       }
        }
        if (currentTime >= 11)
        {
            speed = 0.0f;
        }
    
       if (scoreValue == 6)
            {
                VictoryMusic.enabled = true;
                BackgroundMusic.enabled = false;
                winText.text = "Congratulations you win! Game made by Jacob Brady!";
                Destroy(winText, 2);
                speed = 0.0f;
                gameOver = true;
                Ending.text = "Press R to Restart or Esc to quit!";
            if (currentTime <=12)
            {
                currentTime = 12f;
            }
            }
            if (Input.GetKey(KeyCode.R))

        {

            if (gameOver == true)

            {

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }

        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
  
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Germ")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            PlaySound(Collectable);
            GameObject pickupObject = Instantiate(pickupPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        
    }
     public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
