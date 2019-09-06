using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    int life, maxLife;
    int stamina, maxStamina;
    int hungryDegree, maxHungreeDegree;

    [SerializeField]
    public float[] timeOut;
    private float[] timeElapsed;

    public Status playerStatus;

    private Slider[] sliders;

    private AnimatorStateInfo tempAnimator;
    public enum Status {
        non,
        sick,
        Stan
    }

    // Start is called before the first frame update
    void Start() {
        maxLife = 100;
        life = maxLife;

        maxHungreeDegree = 100;
        hungryDegree = maxHungreeDegree;

        maxStamina = 500;
        stamina = maxStamina;

        sliders = new Slider[3];
        sliders[0] = GameObject.Find("Life_Slider").GetComponent<Slider>();
        sliders[1] = GameObject.Find("Stamina_Slider").GetComponent<Slider>();
        sliders[2] = GameObject.Find("Hungry_Slider").GetComponent<Slider>();

        timeElapsed = new float[3];
    }

    private void Update() {
        HungrySystem();
    }

    private void FixedUpdate() {
        StaminaSystem();
    }

    void HungrySystem() {
        timeElapsed[0] += Time.deltaTime;

        if (timeElapsed[0] >= timeOut[0] && this.playerStatus == Status.non) {
            // Do anything
            hungryDegree -= 1; 
            timeElapsed[0] -= timeOut[0];
        }

        sliders[2].value = (float)hungryDegree / (float)maxHungreeDegree;
    }

    void StaminaSystem() {
        //timeElapsed[1] += Time.deltaTime;

        //if (timeElapsed[1] >= timeOut[1] && this.playerStatus == Status.non) {
        //    // Do anything   
        //    timeElapsed[1] -= timeOut[1];
        //}

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WL_Run")) {
            stamina -= 2;
        }

        if (!tempAnimator.IsName("WL_Attack_Bite_Left") && this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WL_Attack_Bite_Left")) {
            stamina -= 100;
        }

        if (this.stamina < 0) {
            this.playerStatus = Status.Stan;
        }
        if(this.stamina > 50) {
            this.playerStatus = Status.non;
        }

        stamina += 1;

        sliders[1].value = (float)stamina / (float)maxStamina;

        tempAnimator = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }
}
