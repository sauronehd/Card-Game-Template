using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //when using vairibalt int player, player is one, Ai is 2
    public static GameManager gm;
    //public List<Card> deck = new List<Card>();
    public List<Card_data> player_deck = new List<Card_data>();
    public List<Card_data> ai_deck = new List<Card_data>();
    public Card[] player_hand;
    public Card[] ai_hand;
    public List<Card> discard_pile = new List<Card>();

    public Card_data punch_card;
    public Card_data power_fist_card;
    public Card_data pressure_point_card;
    public Card_data stun_card;
    
    
    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        asignDeck(1);
       // asignDeck(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reload(int player)
    {

    }

    void asignDeck(int player)
    {
        if(player == 1)
        {
            int punch = 30;
            int stun = 13;
            int powerFist = 13;
            int pressurePoint = 5;

            while(player_deck.Count < 60)
            {
                int selection = Random.Range(1, 5);
                if(selection == 1 && punch > 0)
                {
                    player_deck.Add(punch_card);
                    punch--;
                    print("punch");
                }
                else if(selection == 2 && stun > 0)
                {
                    player_deck.Add(stun_card);
                    stun--;
                    print("stun");
                }
                else if(selection == 3 && powerFist > 0)
                {
                    player_deck.Add(power_fist_card);
                    powerFist--;
                    print("power fist");
                }
                else if(selection == 4 && pressurePoint > 0)
                {
                    player_deck.Add(pressure_point_card);
                    pressurePoint--;
                    print("pressure point");
                }

                if(player_deck.Count == 60)
                {
                    break;
                }

            }
            
        }
        else
        {
            /*
            int punch = 30;
            int stun = 13;
            int powerFist = 13;
            int pressurePoint = 5;

            while(ai_deck.Count < 60)
            {
                int selection = Random.Range(1, 4);
                if(selection == 1 && punch > 0)
                {
                    ai_deck.Add(punch_card);
                    punch--;
                }
                else if(selection == 2 && stun > 0)
                {
                    ai_deck.Add(stun_card);
                    stun--;
                }
                else if(selection == 3 && powerFist > 0)
                {
                    ai_deck.Add(power_fist_card);
                    powerFist--;
                }
                else if(selection == 4 && pressurePoint > 0)
                {
                    ai_deck.Add(pressure_point_card);
                    pressurePoint--;
                }

                if(ai_deck.Count == 60)
                {
                    break;
                }

            }
            */

        }
    }

    void AI_Turn()
    {

    }

    



    
}
