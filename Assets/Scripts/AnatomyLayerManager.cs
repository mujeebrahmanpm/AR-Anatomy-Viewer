using UnityEngine;

[System.Serializable]
public class AnatomyLayer
{
    public string layerName;
    public GameObject layerObject;
    public Renderer targetRenderer;
    public Material transparencyMaterial;
    public Material clippingMaterial;
    public float maxAlpha = 0.3f;

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

    public void SetClipMinX(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMinX", value); }
    public void SetClipMaxX(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMaxX", value); }
    public void SetClipMinY(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMinY", value); }
    public void SetClipMaxY(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMaxY", value); }
    public void SetClipMinZ(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMinZ", value); }
    public void SetClipMaxZ(float value) { if (activeInstance != null) activeInstance.SetFloat("_ClipMaxZ", value); }

    public Bounds GetWorldBounds() => targetRenderer.bounds;
}

public class AnatomyLayerManager : MonoBehaviour
{
    [SerializeField] private AnatomyLayer skull;
    [SerializeField] private AnatomyLayer brain;

    private Vector3 boundsMin;
    private Vector3 boundsMax;

    void Start()
    {
        skull.SetMode(false);
        brain.SetMode(false);

        Bounds combined = skull.GetWorldBounds();
        combined.Encapsulate(brain.GetWorldBounds());
        boundsMin = combined.min;
        boundsMax = combined.max;
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

    public void SetClipRangeX(float minVal, float maxVal) => SetClipRange(minVal, maxVal, Vector3.right);
    public void SetClipRangeY(float minVal, float maxVal) => SetClipRange(minVal, maxVal, Vector3.up);
    public void SetClipRangeZ(float minVal, float maxVal) => SetClipRange(minVal, maxVal, Vector3.forward);

    void SetClipRange(float minVal, float maxVal, Vector3 axis)
    {
        float axisMin = Vector3.Dot(boundsMin, axis);
        float axisMax = Vector3.Dot(boundsMax, axis);

        float worldMin = Mathf.Lerp(axisMin, axisMax, (minVal + 1f) / 2f);
        float worldMax = Mathf.Lerp(axisMin, axisMax, (maxVal + 1f) / 2f);

        if (axis == Vector3.right)
        {
            skull.SetClipMinX(worldMin); skull.SetClipMaxX(worldMax);
            brain.SetClipMinX(worldMin); brain.SetClipMaxX(worldMax);
        }
        else if (axis == Vector3.up)
        {
            skull.SetClipMinY(worldMin); skull.SetClipMaxY(worldMax);
            brain.SetClipMinY(worldMin); brain.SetClipMaxY(worldMax);
        }
        else
        {
            skull.SetClipMinZ(worldMin); skull.SetClipMaxZ(worldMax);
            brain.SetClipMinZ(worldMin); brain.SetClipMaxZ(worldMax);
        }
    }
}