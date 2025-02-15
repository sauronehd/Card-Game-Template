using UnityEngine;
using TMPro;
public class combatRepCard : MonoBehaviour
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
    //private Vector3 og_pos;

    public string who;  

    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //og_pos = transform.position;
    }

    void Update()
    {
        if(who == "player")
        {
            data = gm.player_card_deployed;
        }
        else
        {
            data = gm.ai_card_deployed;
        }
        if (data != null)  {
            card_name = data.card_name;
            description = data.description;
            defense = data.defense;
            damage = data.damage;
            nameText.text = card_name;
            descriptionText.text = description;
            defenseText.text ="D: "+ defense.ToString();
            damageText.text ="A: "+ damage.ToString();
        }
        else
        {
            damageText.text = "";
            defenseText.text = "";
            descriptionText.text = "";
            nameText.text = "";
        }
    
    }



}
