using UnityEngine;

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
    
    private void Start()
    {
        player = this.GetComponent<Rigidbody>();
        pointer = GameObject.Find("Pointer");
        key = new bool[keyCount];
        ps = this.GetComponent<PlayerStatus>();
    }

    private void Update() {
        z = Input.GetAxis("MoveHorizontal");
        x = Input.GetAxis("MoveVertical");

        if (Input.GetKeyDown(KeyCode.Space)) {
            this.GetComponent<Animator>().SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            this.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        cameraHolizontal = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveVector = (cameraHolizontal * x + Camera.main.transform.right * z).normalized;

        moveVector = Vector3.Lerp(tempVector, moveVector, acceleration);
        
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WL_Attack_Bite_Left") || ps.playerStatus == PlayerStatus.Status.Stan) {
            moveVector = Vector3.zero;
            tempVector = Vector3.zero;
            this.GetComponent<Animator>().SetFloat("Speed",0);
            return;
        }

        player.velocity = new Vector3(moveVector.x  * maxSpeed, player.velocity.y, moveVector.z * maxSpeed);

        float rotationCos = Vector3.Dot(new Vector3(moveVector.x, 0, moveVector.z), new Vector3(player.transform.right.x, 0, player.transform.right.z));

        if (rotationCos != 0) {
            player.transform.Rotate(0, rotationCos * rotationSpeed, 0);
        }

        if (rotationCos > 0.08f)
        {
            this.GetComponent<Animator>().SetFloat("Turn", 1);
        }else if (rotationCos < -0.08f)
        {
            this.GetComponent<Animator>().SetFloat("Turn", -1);
        }else
        {
            this.GetComponent<Animator>().SetFloat("Turn", 0);
        }

        if (this.GetComponent<Rigidbody>().velocity.magnitude > 0.2f)
        {
            this.GetComponent<Animator>().SetFloat("Speed", this.GetComponent<Rigidbody>().velocity.magnitude / 10);
        }
        else
        {
            this.GetComponent<Animator>().SetFloat("Speed", 0);
        }

        tempVector = moveVector;
    }
}
