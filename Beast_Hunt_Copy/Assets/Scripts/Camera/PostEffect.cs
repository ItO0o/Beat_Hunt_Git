using UnityEngine;

public class PostEffect : MonoBehaviour {
    public Material mosaic;
    public float monoLevel;
    public bool mode;
    public float transitionSpeed = 0.1f;
    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, mosaic);
    }

    private void Update() {
        mosaic.SetFloat("_MonoLevel",monoLevel);
        if (Input.GetKeyDown(KeyCode.V)) {
            mode = true;
        }else if (Input.GetKeyUp(KeyCode.V)) {
            mode = false;
        }

        if (mode == true && monoLevel > 0) {
            monoLevel -= transitionSpeed;
        } else if (mode == false && monoLevel < 1){
            monoLevel += transitionSpeed;
        }
    }
    //public GameObject renderTarget;

    //protected MeshRenderer _rendererComponents;

    //void Start() {
    //    _rendererComponents = renderTarget.GetComponent<MeshRenderer>();
    //}

    //void OnPreRender() {
    //    //foreach (var renderer in _rendererComponents) {
    //    _rendererComponents.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    //    //}
    //}
    //void OnPostRender() {
    //    //foreach (var renderer in _rendererComponents) {
    //    _rendererComponents.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //    //}
    //}
}