using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Generater : MonoBehaviour,IPause
{ 
    [SerializeField] GameObject[] _rice;
    [SerializeField] GameObject[] _items;
    [SerializeField] GameObject[] _rareitems;
    [SerializeField] float _riceinterbal;
    [SerializeField] float _iteminterbal;
    [SerializeField] float _rareinterbal;
    [SerializeField] float _maxx;
    [SerializeField] float _lowerx;
    [SerializeField] float _maxz;
    [SerializeField] float _lowerz;
    [SerializeField] int _firstGenerateRice;
    float ricetime;
    float itemtime;
    float raretime;
    bool flag = true;
    void Start()
    {
        //ゲーム開始時の米の生成をしています
         for(var i =0; i <= _firstGenerateRice; i++)
        {
            Vector3 pos = new Vector3(RandomGenerateSpotX(), 2, RandomGenerateSpotZ());
            Instantiate(RiceRandom()).transform.position = pos;
        }       

    }
    void Update()
    {
        if (flag)
        {
            //それぞれのインターバルが経過すると生成します
            ricetime += Time.deltaTime;
            itemtime += Time.deltaTime;
            raretime += Time.deltaTime;
            if (ricetime >= _riceinterbal)
            {
                Vector3 pos = new Vector3(RandomGenerateSpotX(), 50, RandomGenerateSpotZ());
                Instantiate(RiceRandom()).transform.position = pos;
                ricetime = 0;
            }
            if (itemtime >= _iteminterbal)
            {
                Vector3 pos = new Vector3(RandomGenerateSpotX(), 50, RandomGenerateSpotZ());
                Instantiate(ItemRandom()).transform.position = pos;
                itemtime = 0;
            }
            if (raretime >= _rareinterbal)
            {
                Vector3 pos = new Vector3(RandomGenerateSpotX(), 50, RandomGenerateSpotZ());
                Instantiate(RareItems()).transform.position = pos;
                raretime = 0;
            }
        } 
    }
    //これらはランダムなゲームオブジェクト型の戻り値を返す関数です
    GameObject RiceRandom()
    {
        return _rice[RandomGenerater(_rice.Length)];
    }
    GameObject ItemRandom()
    {
        return _items[RandomGenerater(_items.Length)];
    }
    GameObject RareItems()
    {
        return _rareitems[RandomGenerater(_rareitems.Length)];
    }
    int RandomGenerater(int length)
    {
        int randomInt = UnityEngine.Random.Range(0, length);
        return randomInt;
    }
    //座標のランダム生成関数
    float RandomGenerateSpotX()
    {
        float randomspot = UnityEngine.Random.Range(_lowerx , _maxx);
        return randomspot;
    }
    float RandomGenerateSpotZ()
    {
        float randomspot  = UnityEngine.Random .Range(_lowerz , _maxz);
        return randomspot;
    }
    void IPause.Pause()
    {
        flag = false;
    }

    void IPause.Resume()
    {
        flag = true;
    }
}
