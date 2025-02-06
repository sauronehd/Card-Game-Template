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
    public int ID;

    [SerializeField]private GameManager gm;
    //public Image spriteImage;
        

    // Start is called before the first frame update
    void Start()
    {

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        data = gm.player_hand[ID-1];
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

        if (data == null)
        {
            MakeInvisible(gameObject);
        }
        else
        {
            MakeVisible(gameObject);
        }
    }

    void MakeInvisible(GameObject obj)
    {
        obj = this.gameObject;
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

    void MakeVisible(GameObject obj)
    {
        obj = this.gameObject;
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    void OnMouseDown()
    {
        
        gm.player_hand[ID-1] = null;
        data = null;
        print("Card played");
    }
}
