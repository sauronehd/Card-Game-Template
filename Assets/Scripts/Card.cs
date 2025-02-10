/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [SerializeField]public GameManager gm;
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

        void OnGUI()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Rect buttonRect = new Rect(screenPos.x - 50, Screen.height - screenPos.y - 25, 100, 50);

        if (GUI.Button(buttonRect, "Click Me"))
        {
            Debug.Log("GameObject"+ID+" clicked!");
            
            gm.player_hand[ID - 1] = null;
            data = null;
            print("Card played");
            
        }
    }


    

    
}
*/


//experimental
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour
{
    public Card_data data;
    public GameManager gm;
    public int ID;
    public string card_name;
    public string description;
    public int defense;
    public int damage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI damageText;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        data = gm.player_hand[ID - 1];
        if (data != null)
        {
            card_name = data.card_name;
            description = data.description;
            defense = data.defense;
            damage = data.damage;
            nameText.text = card_name;
            descriptionText.text = description;
            defenseText.text = defense.ToString();
            damageText.text = damage.ToString();
            MakeVisible(gameObject);
        }
        else
        {
            MakeInvisible(gameObject);
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

    public void playCard(Card parent)
    {


        gm.player_hand[parent.ID - 1] = null;
        parent.data = null;
        
        print("Card played:"+ parent.ID);
    }

}
