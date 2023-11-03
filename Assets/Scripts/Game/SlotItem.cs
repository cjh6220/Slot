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
    private SpriteToMesh STM;
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

    public void SetIconOrder(int num)
    {
        //sR.sortingOrder = num;
    }

    private void SetIcon(bool blur)
    {
        if (SpinImg == null) return;
        if (sR)
        {
            sR.sprite = (!blur) ? OriginImg : SpinImg;
        }
        else if (STM)
        {
            STM.SetTexture((!blur) ? OriginImg.texture : SpinImg.texture);
        }
        // else if (deformer)
        // {
        //     deformer.SetTexture((!blur) ? OriginImg.texture : SpinImg.texture);
        // }
    }
}
