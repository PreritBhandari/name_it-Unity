using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchName : MonoBehaviour {
  
  public string word;
  private Image Fruit;
  public Button[] numberButtons;
  public Text nameDisplay;

  public AudioSource fruitaudio;
  private int a=1;

  public GameObject starwindow;
  public GameObject[] stars;
  public GameObject retry;
  public GameObject resume;


  public AudioSource levelclear;
  public AudioSource levelloose;
  public AudioSource wrong;


 
  

  void Start() {

    foreach (var button in numberButtons)
          {
              // For each one of my buttons add a event for when it's pressed so I can add the right button
              button.onClick.AddListener(() => NameStore(button.name));
          }

    Fruit=GetComponent<Image>();
    // levelclear=GetComponent<AudioSource>();
    // levelloose=GetComponent<AudioSource>();


    
  }
  

  
  
  public void NameStore(string ButtonName)
  {     
     
        
          word += ButtonName;
          nameDisplay.text=word;
          if (nameDisplay.text.Length>Fruit.sprite.name.Length)
          {
            nameDisplay.text="";  
            word=""; 
            a+=1;
            if(a>3)
            {
              Debug.Log("You lose");
              starwindow.SetActive(true);
              retry.SetActive(true);
              levelloose.Play();
              
            } 
            else
            {
                Debug.Log("Next Try");
                wrong.Play();
                

            }
             
          }

          
          if (word.ToLower()==Fruit.sprite.name)
          {
              Debug.Log("Correct Answer");
              Invoke("Audio",1); // call Audio() after 4 second
              

              // nameDisplay.text="CORRECT !!!";
          }      
         
  }

  public void Audio()
  {
     fruitaudio.Play();
     Invoke("Starwindow",1);//then call star window

  }

  public void Starwindow()
  { 
    
    levelclear.Play();
    starwindow.SetActive(true);
    resume.SetActive(true);
    if (a==1)
    {
        stars[0].SetActive(true);
        stars[1].SetActive(true);
        stars[2].SetActive(true);
    }
    else if (a==2)
     {
        stars[0].SetActive(true);
        stars[1].SetActive(true);
        
    }

    else if (a==3)
     {
        stars[0].SetActive(true);
    }

    
  }

  public void Retry()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void Quit()
  {
    SceneManager.LoadScene("MainScene");
  }
  



}
