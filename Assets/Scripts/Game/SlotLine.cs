using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mkey;


public class SlotLine : MonoBehaviour
{
    public Mkey.RayCaster[] rayCasters;
    public int tileCount = 30;
    public GameObject tilePrefab;
    [SerializeField]
    private int topSector = 0;
    private float tileSizeY = 2.4f;
    private float gapY = 0f;
    private Transform TilesGroup;
    private float anglePerTileRad = 0;
    private float anglePerTileDeg = 0;
    private int windowSize;
    private bool baseLink = false;
    private TweenSeq tS;

    public int NextOrderPosition { get; private set; }
    public int CurrOrderPosition { get; private set; }

    public void CreateSlotCylinder()
    {
        // create Reel transform
        TilesGroup = (new GameObject()).transform;
        TilesGroup.localScale = transform.lossyScale;
        TilesGroup.parent = transform;
        TilesGroup.localPosition = Vector3.zero;
        TilesGroup.name = "Reel(" + name + ")";

        // calculate reel geometry
        float distTileY = tileSizeY + gapY; //old float distTileY = 3.48f;

        anglePerTileDeg = 360.0f / (float)tileCount;

        anglePerTileRad = anglePerTileDeg * Mathf.Deg2Rad;
        float radius = (distTileY / 2f) / Mathf.Tan(anglePerTileRad / 2.0f); //old float radius = ((tileCount + 1) * distTileY) / (2.0f * Mathf.PI);

        windowSize = rayCasters.Length;

        bool isEvenRayCastersCount = (windowSize % 2 == 0);
        int dCount = (isEvenRayCastersCount) ? windowSize / 2 - 1 : windowSize / 2;
        float addAnglePerTileDeg = (isEvenRayCastersCount) ? -anglePerTileDeg * dCount - anglePerTileDeg / 2f : -anglePerTileDeg;
        float addAnglePerTileRad = (isEvenRayCastersCount) ? -anglePerTileRad * dCount - anglePerTileRad / 2f : -anglePerTileRad;
        topSector = windowSize - 1;

        TilesGroup.localPosition = new Vector3(TilesGroup.localPosition.x, TilesGroup.localPosition.y, radius); // offset reel position by z-coordinat

        // orient to base rc
        RayCaster baseRC = rayCasters[rayCasters.Length - 1]; // bottom raycaster
        float brcY = baseRC.transform.localPosition.y;
        float dArad = 0f;
        if (brcY > -radius && brcY < radius && baseLink)
        {
            float dY = brcY - TilesGroup.localPosition.y;
            dArad = Mathf.Asin(dY / radius);
            //    Debug.Log("dY: "+ dY + " ;dArad: " + dArad  + " ;deg: " + dArad* Mathf.Rad2Deg);
            addAnglePerTileRad = dArad;
            addAnglePerTileDeg = dArad * Mathf.Rad2Deg;
        }
        else if (baseLink)
        {
            Debug.Log("Base Rc position out of reel radius");
        }

        //create reel tiles
        for (int i = 0; i < tileCount; i++)
        {
            float n = (float)i;
            float tileAngleRad = n * anglePerTileRad + addAnglePerTileRad; // '- anglePerTileRad' -  symborder corresponds to visible symbols on reel before first spin 
            float tileAngleDeg = n * anglePerTileDeg + addAnglePerTileDeg;

            GameObject obj = Instantiate(tilePrefab, transform.position, Quaternion.identity);
            obj.transform.parent = TilesGroup;
            obj.transform.localPosition = new Vector3(0, radius * Mathf.Sin(tileAngleRad), -radius * Mathf.Cos(tileAngleRad));
            obj.transform.localScale = Vector3.one;
            obj.transform.localEulerAngles = new Vector3(tileAngleDeg, 0, 0);
            obj.name = "SlotSymbol: " + String.Format("{0:00}", i);

            
            //slotSymbols[i] = Instantiate(tilePrefab, transform.position, Quaternion.identity).GetComponent<SlotSymbol>();
            //slotSymbols[i].transform.parent = TilesGroup;
            //slotSymbols[i].transform.localPosition = new Vector3(0, radius * Mathf.Sin(tileAngleRad), -radius * Mathf.Cos(tileAngleRad));
            //slotSymbols[i].transform.localScale = Vector3.one;
            //slotSymbols[i].transform.localEulerAngles = new Vector3(tileAngleDeg, 0, 0);
            //slotSymbols[i].name = "SlotSymbol: " + String.Format("{0:00}", i);
        }
    }

    public void StartSpin(float spinStartDelay)
    {
        //NextOrderPosition = UnityEngine.Random.Range(0,tileCount);
        Debug.LogError("스핀 시작");
        tS = new TweenSeq();
        float angleX = 0;
        float inRotAngle = 7f;
        float inRotTime = 0.15f;

        float oldVal = 0f;

        tS.Add((callBack) => // in rotation part
            {
                SimpleTween.Value(gameObject, 0f, inRotAngle, inRotTime)
                                  .SetOnUpdate((float val) =>
                                  {
                                      TilesGroup.Rotate(val - oldVal, 0, 0);
                                      oldVal = val;
                                  })
                                  .AddCompleteCallBack(() =>
                                  {
                                      callBack();
                                  }).SetEase(EaseAnim.EaseLinear).SetDelay(spinStartDelay/5);
            });

        float addRotateTime = 0f;
        float mainRotateTimeRandomize = 0.1f;
        float mainRotTime = 3;
        float spinSpeedMultiplier = 2;
        float outRotAngle = 7f;
        
        tS.Add((callBack) =>  // main rotation part
        {
            oldVal = 0f;
            addRotateTime = Mathf.Max(0, addRotateTime);
            mainRotateTimeRandomize = Mathf.Clamp(mainRotateTimeRandomize, 0f, 0.2f);
            mainRotTime = 3;//addRotateTime + UnityEngine.Random.Range(mainRotTime * (1.0f - mainRotateTimeRandomize), mainRotTime * (1.0f + mainRotateTimeRandomize));

            spinSpeedMultiplier = Mathf.Max(0, spinSpeedMultiplier);
            angleX = GetAngleToNextSymb(NextOrderPosition) + anglePerTileDeg * tileCount * spinSpeedMultiplier;
            //if (debugreel) Debug.Log(name + ", angleX : " + angleX);
            SimpleTween.Value(gameObject, 0, -(angleX + outRotAngle + inRotAngle), mainRotTime)
                              .SetOnUpdate((float val) =>
                              {
                                  // check rotation angle 
                                  TilesGroup.Rotate(val - oldVal, 0, 0);
                                  oldVal = val;
                                  //if (val < -inRotAngle && val >= -(angleX + inRotAngle)) WrapSymbolTape(val + inRotAngle);
                              })
                              .AddCompleteCallBack(() =>
                              {
                                  //WrapSymbolTape(angleX);
                                  topSector += Mathf.Abs(Mathf.RoundToInt(angleX / anglePerTileDeg));
                                  topSector = (int)Mathf.Repeat(topSector, tileCount);
                                  //if (debugreel) SignTopSymbol(topSector);

                                  callBack();
                              }).SetEase(EaseAnim.EaseLinear);
        });


        float outRotTime = 0.15f;
        tS.Add((callBack) =>  // out rotation part
            {
                oldVal = 0f;
                SimpleTween.Value(gameObject, 0, outRotAngle, outRotTime)
                                  .SetOnUpdate((float val) =>
                                  {
                                      TilesGroup.Rotate(val - oldVal, 0, 0);
                                      oldVal = val;
                                  })
                                  .AddCompleteCallBack(() =>
                                  {
                                      CurrOrderPosition = NextOrderPosition;
                                      //rotCallBack?.Invoke();
                                      callBack();
                                  }).SetEase(EaseAnim.EaseLinear);
            });

        tS.Start();
    }

    private float GetAngleToNextSymb(int nextOrderPosition)
    {
        if (CurrOrderPosition < nextOrderPosition)
        {
            return (nextOrderPosition - CurrOrderPosition) * anglePerTileDeg;
        }
        return (tileCount - CurrOrderPosition + nextOrderPosition) * anglePerTileDeg;
    }

    // private void WrapSymbolTape(float dA)
    // {
    //     int sectors = Mathf.Abs(Mathf.RoundToInt(dA / anglePerTileDeg));
    //     //  if (sectors < tileCount-windowSize-2) return;

    //     bool found = false;

    //     for (int i = topSector + tempSectors; i < topSector + sectors + 3; i++)
    //     {
    //         int ip = (int)Mathf.Repeat(i, tileCount);
    //         tempSectors = i - topSector; // if (debugreel) Debug.Log("search sectors: " + sectors + ";  i: " + i);

    //         if (!found)
    //         {
    //             found = (ip == lastChanged);
    //         }
    //         else // wrap tape at last changed
    //         {
    //             //if (debugreel) Debug.Log("found: " + found);
    //             int symNumber = symbOrder[GetNextSymb()];
    //             slotSymbols[ip].SetIcon(sprites[symNumber], symNumber);
    //             lastChanged = ip; // if (debugreel) Debug.Log("set symbol in: " + ip + "; tempsectors: " + tempSectors);
    //         }
    //     }
    // }
}

