using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Generater : MonoBehaviour
{
    // Start is called before the first frame update

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
    float ricetime;
    float itemtime;
    float raretime;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        ricetime += Time.deltaTime;
        itemtime += Time.deltaTime;
        raretime += Time.deltaTime;
    }
    //�����̓����_���ȃQ�[���I�u�W�F�N�g�^�̖߂�l��Ԃ��֐��ł�
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
        int randomInt = UnityEngine.Random.Range(0, length + 1);
        return randomInt;
    }
    //���W�̃����_�������֐�
    float RandomGenerateSpotX()
    {
        float randomspot = UnityEngine.Random.Range(_lowerx , _maxx +1);
        return randomspot;
    }
    float RandomGenerateSpotZ()
    {
        float randomspot  = UnityEngine.Random .Range(_lowerz , _maxz +1);
        return randomspot;
    }
}
