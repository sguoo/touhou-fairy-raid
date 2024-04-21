using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCaracter : MonoBehaviour
{
   public void onClick()
   {
      SceneManager.LoadScene("CharacterSelector");
   }
}
