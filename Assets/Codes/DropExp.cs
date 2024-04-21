using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class DropExp : MonoBehaviour
{
   [System.Serializable]
   public class Drops
   {
      public string name;
      public GameObject itemPrefab;
      public float dropRate;
   }

   public List<Drops> drops;

   private void Awake()
   {
    
   }

   public void dropExp()
   {
      float randomNumber = UnityEngine.Random.Range(0f, 100f);
      List<Drops> possibleDrops = new List<Drops>();
      foreach (Drops rate in drops)
      {
         if (randomNumber <= rate.dropRate)
         {
            possibleDrops.Add(rate);
         }

         if (possibleDrops.Count > 0)
         {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
         }
      }
   }
}
