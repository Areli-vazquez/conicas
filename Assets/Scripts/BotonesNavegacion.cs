using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesNavegacion : MonoBehaviour
{
   public void InicioMain(){
       SceneManager.LoadScene("Main");
   }
   public void MainAInformacion(){
       SceneManager.LoadScene("Info");
   }
   public void MainAInicio(){
       SceneManager.LoadScene("Inicial");
   }
}
