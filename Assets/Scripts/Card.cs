using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card_data data;

    public string card_name;
    public string description;
    public int defense;
    //public int cost;
    public int damage;
    //public Sprite sprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI defenseText;
    //public TextMeshProUGUI costText;
    public TextMeshProUGUI damageText;
    //public Image spriteImage;
        

    // Start is called before the first frame update
    void Start()
    {
        card_name = data.card_name;
        description = data.description;
        defense = data.defense;
        damage = data.damage;
        //sprite = data.sprite;
        nameText.text = card_name;
        descriptionText.text = description;
        defenseText.text = defense.ToString();
        //costText.text = cost.ToString();
        damageText.text = damage.ToString();
        //spriteImage.sprite = sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
