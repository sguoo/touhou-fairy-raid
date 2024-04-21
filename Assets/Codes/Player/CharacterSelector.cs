  using System;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.TextCore.Text;

  public class CharacterSelector : MonoBehaviour
   {
    public static CharacterSelector chara;
    public CharacterScriptObject characterData;
 
    private void Awake() 
    { 
     if (chara == null) 
     { 
      chara = this;
      DontDestroyOnLoad(gameObject);
     }
     else
     {
      Debug.LogWarning("EXTRA" + this + "DELETED");
      Destroy(gameObject);
     }
      
    }

    public static CharacterScriptObject GetData()
    {
     return chara.characterData;
    }

    public void SelectCharacter(CharacterScriptObject character)
    {
     characterData = character;
    }

    public void DestroySingleton()
    {
     chara = null;
     Destroy(gameObject);
    }
   }
