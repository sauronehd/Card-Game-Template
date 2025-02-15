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
using UnityEditor;

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
    private Vector3 og_pos;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        og_pos = transform.position;
    }

    void Update()
    {
        data = gm.player_hand[ID - 1];
        if (data != null)
        {
            card_name = data.card_name;
            description = data.description;
            defense =+data.defense;
            damage =data.damage;
            nameText.text = card_name;
            descriptionText.text = description;
            defenseText.text ="D "+ defense.ToString();
            damageText.text ="A:" +damage.ToString();
            MakeVisible(gameObject);
        }
        else
        {
            MakeInvisible(gameObject);
            //print("im invisible");
        }
    }

    void MakeInvisible(GameObject obj)
    {
        transform.position = new Vector3(999,999,999);
        //print("there i go");
        //Idea - simply zap the card very far away
    }

    void MakeVisible(GameObject obj)
    {
        transform.position = og_pos;
         //print("im back");
        //Save og coords then return from abyss here
    }

    public void playCard(Card parent)
    {

        if(gm.player_card_deployed==null&&gm.priority==1)
        {
            if(parent.data!=null)
            {
                gm.player_card_deployed = parent.data;
                gm.player_hand[parent.ID - 1] = null;
                parent.data = null;
                //print("Card played:"+ parent.ID);
                gm.priority = 2;
            }
        }
    }

}
