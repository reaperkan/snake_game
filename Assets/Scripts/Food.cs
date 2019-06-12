using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Vector3 center;
    private float score;
    private Color color;
    bool initialized=false;
    bool grounded=false;    
    public float getScore{
        get{
            return score;
        }
    }
    public Color getColor{
        get{
            return color;
        }
    }

    void Start(){
        _rigidBody=GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag=="Ground"){
            _rigidBody.isKinematic=true;
            GetComponent<Collider>().isTrigger=true;           
            grounded=true;
        }
    }
    void FixedUpdate(){
        if(initialized && !grounded){
            Vector3 c=(transform.position-center).normalized;
            Quaternion target=Quaternion.FromToRotation(transform.up,c)*transform.rotation;
            transform.rotation=Quaternion.Slerp(transform.rotation,target,Time.deltaTime*50);
            _rigidBody.AddForce(c*(-9.81f));
        }
    }
    public void SetValues(Color color,float score,Vector3 center){
        this.center=center;
        this.color=color;
        this.score=score;
        GetComponent<MeshRenderer>().material.color=color;
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",color);
        initialized=true;
    }
}
