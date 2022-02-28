using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
  //move to the training mode scene once button is pressed
    public void OnGetStartedClick(){
      SceneManager.LoadScene("TrainMode");
    }

    public void OnBackClick(){
      if (SceneManager.GetActiveScene().name == "DeployMode"){
        SceneManager.LoadScene("TrainMode");
      }
    }
}
