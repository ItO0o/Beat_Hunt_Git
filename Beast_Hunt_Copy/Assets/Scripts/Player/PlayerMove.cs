﻿using UnityEngine;

//ito
//    プレイヤーの動きを制御するメソッド
//    f,gで回転
//    矢印で平行移動
//ジャンプメソッド追加予定

public class PlayerMove : MonoBehaviour
{
    public static bool moveFlag = false;

    private float z;
    private float x;

    Vector3 cameraHolizontal;
    Vector3 moveVector;

    private GameObject pointer;

    private Rigidbody player;

    [SerializeField]
    public float maxSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float acceleration;

    private Vector3 tempVector;

    [SerializeField]
    private int keyCount;
    private bool[] key;

    PlayerStatus ps;
    public Action currentAction;

    GameObject runParticle;

    public enum Action {
        non,
        Run,
        Walk,
        Jump,
        Attack
    }
    
    private void Start()
    {
        player = this.GetComponent<Rigidbody>();
        pointer = GameObject.Find("Pointer");
        key = new bool[keyCount];
        ps = this.GetComponent<PlayerStatus>();
        runParticle = GameObject.Find("Run_Particle");
    }

    private void Update() {
        z = Input.GetAxis("MoveHorizontal");
        x = Input.GetAxis("MoveVertical");

        Jump();
        RunEffect();

        if (Input.GetKeyDown(KeyCode.F)) {
            this.GetComponent<Animator>().SetTrigger("Attack");
            currentAction = Action.Attack;
        }
    }

    void FixedUpdate()
    {
        cameraHolizontal = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveVector = (cameraHolizontal * x + Camera.main.transform.right * z).normalized;

        moveVector = Vector3.Lerp(tempVector, moveVector, acceleration);
        
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WL_Attack_Bite_Left") || ps.playerStatus == PlayerStatus.Status.Stan) {
            Stop();
            return;
        }

        player.velocity = new Vector3(moveVector.x  * maxSpeed, player.velocity.y, moveVector.z * maxSpeed);

        PlayerRotation();
        PlayerUpdatePostion();

        tempVector = moveVector;
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && currentAction != Action.Jump) {
            this.GetComponent<Animator>().SetTrigger("Jump");
            this.GetComponent<PlayerStatus>().stamina -= 50;
            currentAction = Action.Jump;
        }
    }

    void Stop() {
        moveVector = Vector3.zero;
        tempVector = Vector3.zero;
        this.GetComponent<Animator>().SetFloat("Speed", 0);
    }

    void PlayerRotation() {
        float rotationCos = Vector3.Dot(new Vector3(moveVector.x, 0, moveVector.z), new Vector3(player.transform.right.x, 0, player.transform.right.z));
        if (rotationCos != 0) {
            player.transform.Rotate(0, rotationCos * rotationSpeed, 0);
        }
        if (rotationCos > 0.08f) {
            this.GetComponent<Animator>().SetFloat("Turn", 1);
        } else if (rotationCos < -0.08f) {
            this.GetComponent<Animator>().SetFloat("Turn", -1);
        } else {
            this.GetComponent<Animator>().SetFloat("Turn", 0);
        }
    }

    void PlayerUpdatePostion() {
        if (this.GetComponent<Rigidbody>().velocity.magnitude > 0.4f) {
            if (this.GetComponent<Rigidbody>().velocity.magnitude / 10 > 0.3f) {
                currentAction = Action.Run;
            } else {
                currentAction = Action.Walk;
            }
            this.GetComponent<Animator>().SetFloat("Speed", this.GetComponent<Rigidbody>().velocity.magnitude / 10);
        } else {
            this.GetComponent<Animator>().SetFloat("Speed", 0);
        }
    }

    void RunEffect() {
        if (currentAction == Action.Run) {
            //runParticle.SetActive(true);
            runParticle.GetComponent<ParticleSystem>().emissionRate = 5;
        } else {
            //runParticle.SetActive(false);
            runParticle.GetComponent<ParticleSystem>().emissionRate = 0;

        }
    }
}
