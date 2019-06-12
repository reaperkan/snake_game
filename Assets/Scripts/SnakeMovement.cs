using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private float gravity=-9.81f;
    public Transform circularWorld;
    
    public List<Transform> bodyParts=new List<Transform>();
    public Transform bodyHolder;
    public GameObject bodyPrefab;
    public float minDistance=1;
    public float speed=3;
    public int startSize;
    void Start(){
        _rigidBody=GetComponent<Rigidbody>();
        bodyParts.Add(this.transform);
        for(int i=0;i<startSize-1;i++){
            AddBody(false);
        }
    }
    void Update(){
        if(Input.GetAxis("Horizontal") != 0)
            this.transform.Rotate(Vector3.up*100*Time.deltaTime*Input.GetAxis("Horizontal"));
    }
    void FixedUpdate(){
        AddGravity();
        MoveMySelf();
        MoveBody();
    }
    void AddGravity(){
        Vector3 center=(transform.position-circularWorld.position).normalized;
        _rigidBody.AddForce(center*gravity);
         Quaternion target=Quaternion.FromToRotation(transform.up,center)*transform.rotation;
        transform.rotation=Quaternion.Slerp(transform.rotation,target,Time.deltaTime*50);
    }
    void MoveMySelf(){
        transform.Translate(
            Vector3.forward*Time.deltaTime*speed
        );
    }
    void MoveBody(){
        for(int i=1;i<bodyParts.Count;i++){
            Transform curr=bodyParts[i];
            Transform prev=bodyParts[i-1];
            float distance=Vector3.Distance(prev.position,curr.position);
            float time=distance/minDistance*speed*Time.deltaTime;
            Vector3 newPos=prev.position;
            curr.position=Vector3.Slerp(curr.position,newPos,time);
            curr.rotation=Quaternion.Slerp(curr.rotation,prev.rotation,time);
        }
    }
    void AddBody(bool isTrigger){
        Transform p=Instantiate(bodyPrefab,bodyParts[bodyParts.Count-1].position,bodyParts[bodyParts.Count-1].rotation).transform;
        p.SetParent(bodyHolder);
        p.GetComponentInChildren<BoxCollider>().isTrigger=isTrigger;
        bodyParts.Add(p);
    }
    void Dead(){
        Camera.main.transform.SetParent(null);
        bodyHolder.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
