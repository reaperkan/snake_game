using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
   void OnTriggerEnter(Collider col){
       if(col.gameObject.tag=="Food"){
           Food f = col.GetComponent<Food>();
           GameManager.gm.AddScore(f.getColor,f.getScore);
           this.gameObject.SendMessage("AddBody",true);
           Destroy(col.gameObject);
       }else if(col.gameObject.tag=="Body"){
           GameManager.gm.Dead();
            this.gameObject.SendMessage("Dead");
       }
   }
}
