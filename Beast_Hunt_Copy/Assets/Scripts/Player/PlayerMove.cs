using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ito
//    プレイヤーの動きを制御するメソッド
//    f,gで回転
//    矢印で平行移動
//ジャンプメソッド追加予定

public class PlayerMove : MonoBehaviour {
    public static bool moveFlag = false;

    private float z;
    private float x;

    Vector3 cameraHolizontal;
    Vector3 moveVector;

    private GameObject pointer;

    private Rigidbody player;
    private GameObject cg;
    private List<GameObject> cgAll;
    

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

    float rotationCos;

    public enum Action {
        non,
        Run,
        Walk,
        Jump,
        Attack,
        FallDown
    }

    private void Start() {
        player = this.GetComponent<Rigidbody>();
        pointer = GameObject.Find("Pointer");
        key = new bool[keyCount];
        ps = this.GetComponent<PlayerStatus>();
        runParticle = GameObject.Find("Run_Particle");
        cg = GameObject.Find("CG");
        cgAll = new List<GameObject>();
        GameObject temp = GameObject.Find("CG");
        //cgAll.Add(temp);
        GetAllCGObj(temp);
    }

    private void Update() {

        //Debug.Log(currentAction);

        z = Input.GetAxis("MoveHorizontal");
        x = Input.GetAxis("MoveVertical");

        rotationCos = Vector3.Dot(new Vector3(moveVector.x, 0, moveVector.z), new Vector3(player.transform.right.x, 0, player.transform.right.z));

        if (currentAction != Action.FallDown) {
            Jump();
            RunEffect();
        }


        if (Input.GetKeyDown(KeyCode.F)) {
            this.GetComponent<Animator>().SetTrigger("Attack");
            currentAction = Action.Attack;
        }
    }

    void FixedUpdate() {
        cameraHolizontal = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveVector = (cameraHolizontal * x + Camera.main.transform.right * z).normalized;

        moveVector = Vector3.Lerp(tempVector, moveVector, acceleration);

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WL_Attack_Bite_Left") || ps.playerStatus == PlayerStatus.Status.Stan) {
            Stop();
            return;
        }

        player.velocity = new Vector3(moveVector.x * maxSpeed, player.velocity.y, moveVector.z * maxSpeed);

        if (currentAction != Action.FallDown) {
            PlayerRotation();
            PlayerUpdatePostion();
        }

        RecoveryRotation();

        tempVector = moveVector;
    }

    private void LateUpdate() {
        //center.transform.eulerAngles = new Vector3(0,center.transform.eulerAngles.y,0);
        //cg.transform.Rotate(90, 0, 0);
        RunRotation();
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
            if (this.GetComponent<Rigidbody>().velocity.magnitude / 10 > 0.6f) {
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
            runParticle.SetActive(true);
            //runParticle.GetComponent<ParticleSystem>().Play();
            runParticle.GetComponent<ParticleSystem>().maxParticles = 10;
            //runParticle.GetComponent<ParticleSystem>().playbackSpeed = 10;
            //runParticle.GetComponent<ParticleSystem>().emissionRate = 1;
        } else {

            //runParticle.SetActive(false);
            runParticle.GetComponent<ParticleSystem>().maxParticles = 0;
        }
    }

    //float tempRotation;

    void RunRotation() {
        Debug.Log(rotationCos * 3600);
        //cg.transform.Rotate(45, 0,0);
        //cg.transform.Rotate(1,0,0);
        if (currentAction == Action.Run) {
            //if ((cg.transform.eulerAngles.z > 30 && cg.transform.eulerAngles.z < 180 && rotationCos > 0) ||
            //    (cg.transform.eulerAngles.z > 180 && cg.transform.eulerAngles.z < 330 && rotationCos < 0) ||
            //    (cg.transform.eulerAngles.z >= 0 && cg.transform.eulerAngles.z <= 30) || (cg.transform.eulerAngles.z >= 330 && player.transform.eulerAngles.z <= 360)) {
            //cg.transform.eulerAngles = new Vector3(cg.transform.eulerAngles.x , cg.transform.eulerAngles.y + 30, cg.transform.eulerAngles.z);
            if (rotationCos < 30 && rotationCos > -30) {
                cg.transform.Rotate(rotationCos * 3600, 0, 0);
            } else if(rotationCos > 30) {
                cg.transform.Rotate(30, 0, 0);
            }else if (rotationCos < -30) {
                cg.transform.Rotate(-30, 0, 0);
            }

            //}
            //cg.transform.eulerAngles = new Vector3(-90, cg.transform.eulerAngles.y , 90);
        } else {
            //if (cg.transform.eulerAngles.z >= 0 && cg.transform.eulerAngles.z< 90) {
            //    cg.transform.Rotate(0, 0, -0.01f);
            //} else if (cg.transform.eulerAngles.z > 270 && cg.transform.eulerAngles.z< 360) {
            //    cg.transform.Rotate(0, 0, 0.01f);
            //}
        }
        //tempRotation = rotationCos;
    }

    void RecoveryRotation() {
        //Debug.Log(player.transform.eulerAngles);
        if ((player.transform.eulerAngles.x > 90 && player.transform.eulerAngles.x < 270) || (player.transform.eulerAngles.z > 90 && player.transform.eulerAngles.z < 270)) {
            currentAction = Action.FallDown;
        }
        if (currentAction == Action.FallDown) {
            //if () {

            //}
        }
    }

    void GetAllCGObj(GameObject obj) {
        for (int i = 0; i < obj.transform.childCount; i++) {
            cgAll.Add(obj.transform.GetChild(i).gameObject);
            GetAllCGObj(obj.transform.GetChild(i).gameObject);
        }

    }
}
