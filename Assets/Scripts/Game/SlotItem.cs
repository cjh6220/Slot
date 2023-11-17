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

    private Vector3 pos;
    private Vector3 oldPos;
    private float speed = 0;

    private void Start()
    {
        var ranNum = Random.Range(0, GameDataManager.Instance.ItemList.Length);
        SetIcon(ranNum);
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
        var data = GameDataManager.Instance.ItemList[num];
        OriginImg = GameDataManager.Instance.GetItemImg(data.resource, false);
        SpinImg = GameDataManager.Instance.GetItemImg(data.resource, true);
        if (sR != null)
        {
            sR.sprite = OriginImg;    
        }
    }

    public void ChangeItem(int num)
    {
        SetIcon(num);
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
