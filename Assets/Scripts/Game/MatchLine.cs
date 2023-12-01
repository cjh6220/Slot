using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLine : MonoBehaviour
{
    public Mkey.LineCreator lineCreator;
    public Mkey.RayCaster[] rayCasters;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Texture2D lineTexture;
    [SerializeField]
    private Texture2D dotTexture;
    [SerializeField]
    private bool useBehColor = true;
    [SerializeField]
    private int sortingOrder = 0;
    private int sortingLayerID = 0; //next updates

    protected static Texture2D procLineTexture; // procedural texture from gradient

    #region temp vars
    private bool burnCancel = false;
    private WaitForEndOfFrame wfef;
    private List<SpriteRenderer> rend;
    private SpriteRenderer[] lSRs;
    private List<SpriteRenderer> dSRs;
    private Sprite dotSprite;
    private Sprite[] lineSprites;
    private List<Color> colors;
    #endregion temp vars
    private void Start() 
    {
        Create()    ;
    }
    
    public void Create()
    {
        wfef = new WaitForEndOfFrame();
        Material mat = (!material) ? new Material(Shader.Find("Sprites/Default")) : material;

        // create texture from gradient
        if (!lineTexture && !procLineTexture)
        {
            CreateLinearGradientTexture(gradient, ref procLineTexture);
        }
        if (procLineTexture)
        {
            lineTexture = procLineTexture;
        }

        if (!dotTexture) CreateRadialGradientTexture(gradient, ref dotTexture);

        List<Vector3> positions = new List<Vector3>(); // world pos
        List<Vector3> hP = new List<Vector3>(); // local pos
        bool dotStart_0 = true; // start dots from 0 handle or from 1

        if (lineCreator && lineCreator.enabled && lineCreator.handlesPositions != null && lineCreator.handlesPositions.Count > 1)
        {
            foreach (var item in lineCreator.handlesPositions)
            {
                positions.Add(transform.TransformPoint(item));
                hP.Add(item);
            }
        }
        else
        {
            dotStart_0 = true;
            // create line using raycasters
            foreach (var item in rayCasters)
            {
                if (item)
                {
                    positions.Add(item.transform.position);
                    hP.Add(transform.InverseTransformPoint(item.transform.position));
                }
            }
        }

        //create lines
        float[] lengths = new float[hP.Count - 1];
        lineSprites = new Sprite[hP.Count - 1];
        lSRs = new SpriteRenderer[hP.Count - 1];
        Vector3 dirInit = new Vector3(1, 0, 0);
        Vector2 pivot = new Vector2(0, 0.5f);

        for (int i = 0; i < hP.Count - 1; i++)
        {
            Vector3 dir = (positions[i + 1] - positions[i]);
            lengths[i] = dir.magnitude;
            lineSprites[i] = Sprite.Create(lineTexture, new Rect(0, 0, (int)(lengths[i] * 100f), lineTexture.height), pivot, 100);
            lSRs[i] = Mkey.Creator.CreateSprite(null, lineSprites[i], mat, positions[i], sortingLayerID, sortingOrder);

            Quaternion lQuaternion = new Quaternion();
            lQuaternion.SetFromToRotation(dirInit, dir);
            lSRs[i].transform.localScale = new Vector3(1, transform.lossyScale.y, 1);
            lSRs[i].transform.rotation = lQuaternion;
            lSRs[i].transform.parent = transform;
        }

        // create dots
        dSRs = new List<SpriteRenderer>();
        dotSprite = Sprite.Create(dotTexture, new Rect(0, 0, dotTexture.width, dotTexture.height), new Vector2(0.5f, 0.5f), 100);
        int i0 = (dotStart_0) ? 0 : 1;
        int dLength = (dotStart_0) ? hP.Count : hP.Count - 1;
        for (int i = i0; i < dLength; i++)
        {
            SpriteRenderer sr = Mkey.Creator.CreateSprite(null, dotSprite, mat, positions[i], sortingLayerID, sortingOrder + 1);
            sr.transform.parent = transform;
            sr.transform.localEulerAngles = Vector3.zero;
            dSRs.Add(sr);
        }

        //2) cache colors
        if (dSRs != null && lSRs != null)
        {
            rend = new List<SpriteRenderer>(dSRs.Count + lSRs.Length);
            rend.AddRange(dSRs);
            rend.AddRange(lSRs);
            colors = new List<Color>(rend.Count);
            for (int i = 0; i < rend.Count; i++)
            {
                colors.Add(rend[i].color);
            }
        }
        SetLineVisible(false);
    }

    internal void SetLineVisible(bool visible)
        {
            if (dSRs != null)
            {
                foreach (var item in dSRs)
                {
                  if(item)  item.gameObject.SetActive(visible);
                }
            }

            if (lSRs != null)
            {
                foreach (var item in lSRs)
                    if (item) item.gameObject.SetActive(visible);
            }
        }

    private void CreateLinearGradientTexture(Gradient g, ref Texture2D output)
    {
        Debug.Log("Create procedural texture");
        int gradientWidth = 4096;
        int gradientHeight = 16;

        if (output != null) DestroyImmediate(output);
        output = new Texture2D(gradientWidth, gradientHeight, TextureFormat.ARGB32, false);
        output.wrapMode = TextureWrapMode.Clamp;
        output.filterMode = FilterMode.Bilinear;

        float k = 1.0f / ((float)gradientHeight - 1.0f);

        Color c;
        for (int i = 0; i < gradientHeight; i++)
        {
            c = g.Evaluate((float)i * k);
            for (int j = 0; j < gradientWidth; j++)
            {
                output.SetPixel(j, i, c);
            }
        }
        output.Apply();
        Debug.Log("line procedural texture created");
    }

    private void CreateRadialGradientTexture(Gradient g, ref Texture2D output)
        {
            int gradientWidth = 32;
            int gradientHeight = 32;

            if (output != null) DestroyImmediate(output);
            output = new Texture2D(gradientWidth, gradientHeight, TextureFormat.ARGB32, false);
            output.wrapMode = TextureWrapMode.Clamp;
            output.filterMode = FilterMode.Bilinear;
            float maxDist = 16;

            float k = 1.0f / (maxDist - 1.0f);

            Color c;
            Color transp = new Color(1, 1, 1, 0);
            float di;
            float dj;
            float dist;
            for (int i = 0; i < gradientHeight; i++)
            {
                di = i - maxDist;
                for (int j = 0; j < gradientWidth; j++)
                {
                    dj = j - maxDist;
                    dist = Mathf.Sqrt(di * di + dj * dj);
                    if (dist <= maxDist)
                    {
                        c = g.Evaluate(dist * k);
                        output.SetPixel(j, i, c);
                    }
                    else
                        output.SetPixel(j, i, transp);
                }
            }
            output.Apply();
        }
}
