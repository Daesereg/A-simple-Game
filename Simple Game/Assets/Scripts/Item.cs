using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public float stat = 5;
    [SerializeField] public string item_name = "obj_name";
    [SerializeField] public bool isSelected = false;
    [SerializeField] public float spawnRate = 0.5f;
    [SerializeField] public string baseType = "something";
    [SerializeField] public string rarity = "rairty";
    [SerializeField] public string category = "category";
    Transform pos;
    BoxCollider2D boxCol;

    void Start(){
        pos = GetComponent<Transform>();
        boxCol = GetComponent<BoxCollider2D>();
        }
}
