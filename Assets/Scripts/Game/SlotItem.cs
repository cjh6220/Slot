using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mkey;

public class SlotItem : MonoBehaviour
{
    public SpriteRenderer sR;
    public Sprite OriginImg;
    public Sprite SpinImg;
    [SerializeField]
    private IconSpriteDeformerMesh deformer;
    public List<Sprite> OriginImgList;
    public List<Sprite> SpinImgList;

    private Vector3 pos;
    private Vector3 oldPos;
    private float speed = 0;

    private void Start()
    {
        SetIcon(Random.Range(0, OriginImgList.Count));
    }

    private void Update()
    {
        pos = transform.position;
        speed = (pos - oldPos).magnitude / Time.deltaTime;
        oldPos = pos;
        SetIcon(speed > 10);
    }

    public void SetIcon(int num)
    {
        OriginImg = OriginImgList[num];
        SpinImg = SpinImgList[num];
        sR.sprite = OriginImg;
    }

    private void SetIcon(bool blur)
    {
        if (SpinImg == null) return;
        if (sR)
        {
            sR.sprite = (!blur) ? OriginImg : SpinImg;
        }

        // else if (deformer)
        // {
        //     deformer.SetTexture((!blur) ? OriginImg.texture : SpinImg.texture);
        // }
    }
}
