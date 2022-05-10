using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [SerializeField] public float rows;
    [SerializeField] public float columns;
    [SerializeField] public float tileSize;
    [SerializeField] public Text score;
    [SerializeField] public Text movesText;
    //[SerializeField] public Text HighScore;
    [SerializeField] GameObject[] allColors;
    [SerializeField] public int movesLeft;
    

    GameObject[] itemList;
    int clicked = 0;
    GameObject selected;
    string firstItem = "one";
    string secondItem = "second";
    Vector3 selectionDirection;
    Item statOne;
    Item statTwo;
    float totalRates;
    float totalScore;



    void Start(){
        //Creating the item grid
        itemCreation();
        movesLeft = 10;
        selectionDirection = new Vector3(0f, 0f, -2f);
        for(int i = 0; i < allColors.Length; i++){
            Item rate = allColors[i].GetComponent<Item>();;
            totalRates += rate.spawnRate;
        }
        totalScore = 0;
    }
    void Update(){
        itemCombo();
        itemManage();
        if (movesLeft == 0)
        {
            Invoke("GameEnd", 2);
        }
    }

    void GameEnd()
    {
            SceneManager.LoadScene(sceneName: "EndGame");
    }

    //Pseudorandom Item Generator
    private int itemChance(){
        int randomItem = -1;
        while(randomItem == -1){
            randomItem = Random.Range(0,allColors.Length);
            Item weightChance = allColors[randomItem].GetComponent<Item>();

            float itemSpawnChance = weightChance.spawnRate;
            float spawnChance = Random.Range(0.1f, 1f);
            if (itemSpawnChance > spawnChance) {
                return randomItem;
            }
            else{
                Debug.Log("failed spawn");
                randomItem = -1;
            }
        }

        return randomItem;
    }

    private void itemCreation(){
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {

                GameObject tile = Instantiate(allColors[itemChance()], transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;
                tile.transform.position = new Vector2(posX, posY);
            }
        }

        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW/2 + tileSize/2, gridH/1.5f - tileSize/2);
    }

    //Detect/Selecting Items
    bool raySelect(){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, selectionDirection);
        if(hit.collider != null && hit.transform.gameObject.tag == "item"){
            selected = hit.transform.gameObject;
            return true;
        }
        else{
            return false;
        }
    }

    //The item combination for score
    public void itemCombo(){
        if(clicked == 2){
            if(firstItem == secondItem){
                Debug.Log("SIMILAR");
            }
            else{
                movesLeft--;
                movesText.text = movesLeft.ToString();
                if(statOne.baseType != statTwo.baseType)
                {
                    if (statOne.category == statTwo.category)
                    {
                        totalScore = totalScore + 0.5f * (statOne.stat + statTwo.stat);
                    }
                    if (statOne.rarity == statTwo.rarity)
                    {
                        totalScore = totalScore + 0.3f * (statOne.stat + statTwo.stat);
                    }
                    if (statOne.rarity == "Epic" || statTwo.rarity == "Epic")
                    {
                        totalScore = 2f * totalScore;
                    }
                }
                totalScore = statOne.stat + statTwo.stat + totalScore;
                score.text = totalScore.ToString();
            }
            firstItem = "one";
            secondItem = "second";
            clicked = 0;
            statOne.isSelected = false;
            statTwo.isSelected = false;
        }

        if(Input.GetMouseButtonDown(0) && raySelect()){
            if(clicked == 1){
                statTwo = selected.GetComponent<Item>();
                secondItem = statTwo.item_name;
                statTwo.isSelected = true;
                clicked++;
            }
        }
        if(Input.GetMouseButtonDown(0) && raySelect()){
            if(clicked == 0){
                statOne = selected.GetComponent<Item>();
                firstItem = statOne.item_name;
                statOne.isSelected = true;
                clicked++;
                }
            }
    }

    public void itemManage(){
        itemList = GameObject.FindGameObjectsWithTag("item");
        for(int i = 0; i < itemList.Length; i++){
            Item isTrig;
            Transform isDeleted;
            isTrig = itemList[i].GetComponent<Item>();
            if(clicked == 2 && firstItem != secondItem){
                if(isTrig.isSelected){
                        isDeleted = itemList[i].GetComponent<Transform>();
                        Destroy(itemList[i]);
                        //Debug.Log("Item Destroied.");
                        itemList[i] = Instantiate(allColors[itemChance()], transform);
                        itemList[i].transform.position = isDeleted.position;

                }
            }
        }
    }
}
