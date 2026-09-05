using UnityEngine;

[System.Serializable]
public class AnatomyLayer
{
    public string layerName; // just a label for your own reference in the Inspector
    public GameObject layerObject; // the Skull or Brain GameObject itself
    public Renderer targetRenderer; // that same object's Renderer component
    public Material transparencyMaterial;
    public Material clippingMaterial;
    public float maxAlpha = 0.3f; // Skull = 0.2, Brain = 0 (leave default)

    private Material activeInstance;

    public void SetMode(bool isClipMode)
    {
        Material source = isClipMode ? clippingMaterial : transparencyMaterial;
        activeInstance = new Material(source);
        targetRenderer.material = activeInstance;
    }

    public void SetVisible(bool visible)
    {
        layerObject.SetActive(visible);
    }

    public void SetTransparency(float sliderValue)
    {
        if (activeInstance == null) return;
        float alpha = Mathf.Lerp(0f, maxAlpha, sliderValue);
        activeInstance.SetFloat("_Transparency", alpha);
    }
}

public class AnatomyLayerManager : MonoBehaviour
{
    [SerializeField] private AnatomyLayer skull;
    [SerializeField] private AnatomyLayer brain;

    void Start()
    {
        skull.SetMode(false); // start in Transparency mode
        brain.SetMode(false);
    }

    public void SetMode(bool isClipMode)
    {
        skull.SetMode(isClipMode);
        brain.SetMode(isClipMode);
    }

    public void SetSkullVisible(bool visible) => skull.SetVisible(visible);
    public void SetBrainVisible(bool visible) => brain.SetVisible(visible);

    public void SetSkullTransparency(float value) => skull.SetTransparency(value);
    public void SetBrainTransparency(float value) => brain.SetTransparency(value);
}