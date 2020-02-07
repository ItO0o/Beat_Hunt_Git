using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStatus : MonoBehaviour {

    int life, maxLife;
    public int stamina, maxStamina;
    int hungryDegree, maxHungreeDegree;

    [SerializeField]
    public float[] timeOut;
    private float[] timeElapsed;

    public Status playerStatus;

    private Slider[] sliders;

    public float stanStamina,healStan;

    private AnimatorStateInfo tempAnimator;
    public enum Status {
        non,
        Sick,
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
        StanAction();
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

    void StanAction() {
        if (playerStatus == Status.Stan) {
            Palpitations();
        } else if (palLevel > 0) {
            ResetChromaticAberration();
        }
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
            stamina -= 200;
        }

        if (this.stamina < stanStamina) {
            this.playerStatus = Status.Stan;
        }
        if(this.stamina > healStan) {
            this.playerStatus = Status.non;
        }

        if (stamina < maxStamina) {
            stamina += 1;
        }

        sliders[1].value = (float)stamina / (float)maxStamina;

        tempAnimator = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    float palLevel;
    bool palMode;
    float palSpeed = 0.2f;

    void Palpitations() {
        if (palLevel < 0) {
            palMode = true;
        } else if (palLevel > 1) {
            palMode = false;
        }

        if (palMode == true) {
            palLevel += palSpeed;
        } else {
            palLevel -= palSpeed;
        }

        ChromaticAberration c = ScriptableObject.CreateInstance<ChromaticAberration>();
        c.enabled.Override(true);
        c.intensity.Override(palLevel);

        Vignette v = ScriptableObject.CreateInstance<Vignette>();
        v.color.Override(new Color(0.65f,0.37f,0.37f));
        v.enabled.Override(true);
        v.intensity.Override(palLevel);

        PostProcessEffectSettings[] p = { c, v };

        PostProcessVolume postProcessVolume = PostProcessManager.instance.QuickVolume(Camera.main.gameObject.layer, 0f, p);
    }

    void ResetChromaticAberration() {
        palLevel -= palSpeed;
        ChromaticAberration c = ScriptableObject.CreateInstance<ChromaticAberration>();
        c.enabled.Override(true);
        c.intensity.Override(palLevel);

        Vignette v = ScriptableObject.CreateInstance<Vignette>();
        v.color.Override(new Color(0.65f, 0.37f, 0.37f));
        v.intensity.Override(palLevel);
        v.enabled.Override(true);
        PostProcessEffectSettings[] p = {c,v};

        PostProcessVolume postProcessVolume = PostProcessManager.instance.QuickVolume(Camera.main.gameObject.layer, 0f,p);
    }
}
