using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI player_discard;
    public TMPro.TextMeshProUGUI ai_discard;
    //when using vairibalt int player, player is one, Ai is 2     
    public static GameManager gm;
    //public List<Card> deck = new List<Card>();
    public List<Card_data> player_deck = new List<Card_data>();
    public List<Card_data> ai_deck = new List<Card_data>();
    public Card_data[] player_hand;
    public Card_data[] ai_hand;
    //public List<Card> discard_pile_player = new List<Card>();
    //public List<Card> discard_pile_ai = new List<Card>();
    public Card_data punch_card;
    public Card_data power_fist_card;
    public Card_data pressure_point_card;
    public Card_data stun_card;
    [SerializeField]private int player_score;
    [SerializeField]private int ai_score;
    [SerializeField]private int turn=1;
    public int priority;
    public Card_data player_card_deployed;
    public Card_data ai_card_deployed;
    public Card_data divine_card;
    public Card_data full_send_card;
    public Card_data block_card;
    public TMPro.TextMeshProUGUI player_score_text;
    public TMPro.TextMeshProUGUI ai_score_text;
    public TMPro.TextMeshProUGUI AIhandCount;
    public TMPro.TextMeshProUGUI turn_text;
    public TMPro.TextMeshProUGUI priority_text;
    
    // Add dictionaries to track discarded cards
    private Dictionary<string, int> player_discards = new Dictionary<string, int>();
    private Dictionary<string, int> ai_discards = new Dictionary<string, int>();
    
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
        
        // Initialize discard dictionaries
        player_discards.Clear();
        ai_discards.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize discard tracking for all card types
        InitializeDiscardTracking();
        
        asignDeck(1);
        asignDeck(2);
        Reload(1);
        Reload(2);
        
        // Initial update of discard pile text
        UpdateDiscardText();
    }

    void InitializeDiscardTracking()
    {
        // Initialize player discard tracking
        player_discards["Punch"] = 0;
        player_discards["Power Fist"] = 0;
        player_discards["Pressure Point"] = 0;
        player_discards["Stun"] = 0;
        player_discards["Divine"] = 0;
        player_discards["Block"] = 0;
        player_discards["Full Send"] = 0;
        
        // Initialize AI discard tracking
        ai_discards["Punch"] = 0;
        ai_discards["Power Fist"] = 0;
        ai_discards["Pressure Point"] = 0;
        ai_discards["Stun"] = 0;
        ai_discards["Divine"] = 0;
        ai_discards["Block"] = 0;
        ai_discards["Full Send"] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player_score_text.text ="Player Damge Dealt: "+ player_score.ToString();
        turn_text.text = "Turn: " + turn.ToString();
        priority_text.text = "Priority: " + priority.ToString();
        ai_score_text.text ="AI damage dealt: "+ ai_score.ToString();
        int nullcount = 0;
        foreach(Card_data cards in ai_hand)
        {
            if(cards == null)
            {
                nullcount++;
            }
        }
        AIhandCount.text = "AI hand count: " + (ai_hand.Length - nullcount).ToString();
        if(turn == 1)
        {
            if (player_card_deployed == null)
            {
                
            }
            else if (player_card_deployed==punch_card)
            {
                //print("punch detected");
                AI_damage_taken();
                clear_combat_chain();
                priority=1;
            }
            else if (player_card_deployed == power_fist_card)
            {
                AI_card_search(block_card);
                if(ai_card_deployed==null)
                {
                    AI_card_search(power_fist_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(punch_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(stun_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(pressure_point_card);
                }
                AI_damage_taken();
                clear_combat_chain();
                turn = 2;
                priority = 2;
                Reload(2);
                
            }
            else if (player_card_deployed == pressure_point_card)
            {
                AI_card_search(punch_card);
                if(ai_card_deployed==null)
                {
                    AI_card_search(power_fist_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(stun_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(pressure_point_card);
                }
                AI_damage_taken();
                if(player_damage_dealt()>0)
                {

                    player_score+=3;
                }
                clear_combat_chain();
                turn = 2;
                priority = 2;
                Reload(2);
            }           
            else if (player_card_deployed == stun_card)
            {
                AI_card_search(punch_card);
                if(ai_card_deployed==null)
                {
                    AI_card_search(pressure_point_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(stun_card);
                }
                if(ai_card_deployed==null)
                {
                    AI_card_search(power_fist_card);
                }
                AI_damage_taken();

                if(player_damage_dealt()>0)
                {
                    foreach(Card_data card in player_hand)
                    {
                        int i = -1;
                        if(card == null)
                        {
                            i++;
                            int selection = Random.Range(0, player_deck.Count);
                            Card_data dealt = player_deck[selection];
                            player_deck.RemoveAt(selection);
                            player_hand[i] = dealt;
                            break;
                        }
                    break;
                    }
                }
                
                clear_combat_chain();
                priority = 1;
            }
            else if (player_card_deployed == divine_card)
            {
                AI_damage_taken();
                DrawCards(1, 2); // Draw 2 cards for player
                clear_combat_chain();
                turn = 2;
                priority = 2;
                Reload(2);
            }
            else if (player_card_deployed == full_send_card)
            {
                AI_card_search(block_card);
                if(ai_card_deployed==null)
                {
                    AI_card_search(power_fist_card);
                }
                AI_damage_taken();
                clear_combat_chain();
                turn = 2;
                priority = 2;
                Reload(2);
            }
        }
        if(turn == 2)
        {
            if(ai_card_deployed!=null && priority==1)
            {
                
            }
            else if(ai_card_deployed ==null&&priority==2)
            {
                int punches = 0;
                int powerFists = 0;
                int pressurePoints = 0;
                int stuns = 0;
                int blocks = 0;
                int divines = 0;
                int i = -1;
                foreach(Card_data card in ai_hand)
                {
                    i++;
                    if(card == punch_card)
                    {
                        punches++;
                    }
                    else if(card == power_fist_card)
                    {
                        powerFists++;
                    }
                    else if(card == pressure_point_card)
                    {
                        pressurePoints++;
                    }
                    else if(card == stun_card)
                    {
                        stuns++;
                    }
                    else if(card == block_card)
                    {
                        blocks++;
                    }
                    else if(card == divine_card)
                    {
                        divines++;
                    }
                }


                if (punches > 2)
                {
                    AI_card_search(punch_card);
                }
                else if (stuns > 0)
                {
                    AI_card_search(stun_card);
                }
                else if (powerFists > 1)
                { 
                    AI_card_search(power_fist_card);        
                }
                else if (pressurePoints > 0)
                {
                    AI_card_search(pressure_point_card);
                }
                else if (divines > 0 && ai_hand.Length - nullcount < 3)
                {
                    AI_card_search(divine_card);
                }
                else if(ai_card_deployed == null)
                {
                    turn = 1;
                    priority = 1;
                    Reload(1);
                }
            }
            else if(ai_card_deployed!=null&&priority==2)
            {
                player_damage_taken();
                if(ai_damage_dealt()>0)
                {
                    if(ai_card_deployed == pressure_point_card)
                    {
                        ai_score+=3;
                    }
                    else if(ai_card_deployed == stun_card)
                    {
                        int i = -1;
                        foreach(Card_data card in ai_hand)
                        {
                            i++;
                            if(card == null)
                            {
                                
                                int selection = Random.Range(0, ai_deck.Count);
                                Card_data dealt = ai_deck[selection];
                                ai_deck.RemoveAt(selection);
                                ai_hand[i] = dealt;
                                break;
                            }
                        break;
                        }
                    }

                }

                if(ai_card_deployed==divine_card)
                {
                    DrawCards(2, 2);
                }

                if(ai_card_deployed.goAgain)
                {
                    priority = 2;
                }
                else
                {
                    turn = 1;
                    priority = 1;
                    Reload(1);
                }
                clear_combat_chain();


            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(turn == 1 && priority==1)
            {
                turn = 2;
                priority = 2; 
                Reload(2);
            }
            if(turn == 2 && priority==1)
            {
                turn = 2;
                priority = 2;
  
            } 
        }


        if(ai_deck.Count<=0&&player_deck.Count<=0)
        {
            if(ai_score>player_score)
            {
                print("AI wins");
                SceneManager.LoadScene("AIWins");
            }
            else if(player_score>ai_score)
            {
                print("Player wins");
                SceneManager.LoadScene("PlayerWins");
            }
            else
            {
                print("Draw");
                SceneManager.LoadScene("draw");
            }
        }

        // Update discard pile text each frame
        UpdateDiscardText();
    }
        
        

    

    void Reload(int player)
    {
        if(player == 1)
        {
           int i = -1;
            foreach(Card_data card in player_hand)
            {
                i++;
                if(player_deck.Count<=0)
                {
                    break;
                }
                
                
                if(card == null){
                    Card_data dealt = null;
                    while(dealt==null)
                    {
                        int selection = Random.Range(0, player_deck.Count);
                        dealt = player_deck[selection];
                        player_deck.RemoveAt(selection);
                        player_hand[i] = dealt;
                        
                    }
                }
                
            }
  
        }
        else
        {
            int i = -1;
            foreach(Card_data card in ai_hand)
            {
                i++;
                if(ai_deck.Count<=0)
                {
                    break;
                }
                
                if(card == null){
                    Card_data dealt = null;
                    while(dealt==null)
                    {
                        int selection = Random.Range(0, ai_deck.Count);
                        dealt = ai_deck[selection];
                        ai_deck.RemoveAt(selection);
                        ai_hand[i] = dealt;
                    }
                }
                
            }
        }
    }

    void asignDeck(int player)
    {
        if(player == 1)
        {
            int punch = 20;
            int stun = 5;
            int powerFist = 15;
            int pressurePoint = 10;
            int divine = 5;
            int block = 4;
            int fullSend = 1;

            while(player_deck.Count < 60)
            {
                int selection = Random.Range(1, 8);
                if(selection == 1 && punch > 0)
                {
                    player_deck.Add(punch_card);
                    punch--;
                }
                else if(selection == 2 && stun > 0)
                {
                    player_deck.Add(stun_card);
                    stun--;
                }
                else if(selection == 3 && powerFist > 0)
                {
                    player_deck.Add(power_fist_card);
                    powerFist--;
                }
                else if(selection == 4 && pressurePoint > 0)
                {
                    player_deck.Add(pressure_point_card);
                    pressurePoint--;
                }
                else if(selection == 5 && divine > 0)
                {
                    player_deck.Add(divine_card);
                    divine--;
                }
                else if(selection == 6 && block > 0)
                {
                    player_deck.Add(block_card);
                    block--;
                }
                else if(selection == 7 && fullSend > 0)
                {
                    player_deck.Add(full_send_card);
                    fullSend--;
                }
            }
        }
        else
        {
            int punch = 20;
            int stun = 5;
            int powerFist = 15;
            int pressurePoint = 10;
            int divine = 5;
            int block = 4;
            int fullSend = 1;

            while(ai_deck.Count < 60)
            {
                int selection = Random.Range(1, 8);
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
                else if(selection == 5 && divine > 0)
                {
                    ai_deck.Add(divine_card);
                    divine--;
                }
                else if(selection == 6 && block > 0)
                {
                    ai_deck.Add(block_card);
                    block--;
                }
                else if(selection == 7 && fullSend > 0)
                {
                    ai_deck.Add(full_send_card);
                    fullSend--;
                }
            }
        }
    }

    void AI_card_search(Card_data card)
    {
        int i = -1;
        foreach(Card_data cards in ai_hand)
        {
            i++;
            if(cards == card)
            {
                ai_hand[i] = null;
                priority = 1;
                ai_card_deployed = card;
                break;
            }               
        }
    }

    void AI_damage_taken()
    {
        if(ai_card_deployed == null)
        {
            player_score+=player_card_deployed.damage;
        }
        else
        {
            if(player_damage_dealt()>=0)
            {
                player_score+=player_card_deployed.damage - ai_card_deployed.defense;
            }

        }
        
    }
    void player_damage_taken()
    {
        if(player_card_deployed == null)
        {
            ai_score+=ai_card_deployed.damage;
        }
        else
        {
            if(ai_damage_dealt()>=0)
            {
                ai_score+=ai_card_deployed.damage - player_card_deployed.defense;
            }
        }
    }

    int player_damage_dealt()
    {
        if(ai_card_deployed == null)
        {
            return player_card_deployed.damage;
        }
        else
        {
            return player_card_deployed.damage - ai_card_deployed.defense;
        }
    }
    int ai_damage_dealt()
    {
        if(player_card_deployed == null)
        {
            return ai_card_deployed.damage;
        }
        else
        {
            return ai_card_deployed.damage - player_card_deployed.defense;
        }
    }
    void clear_combat_chain()
    {
        if (player_card_deployed != null)
        {
            TrackDiscardedCard(player_card_deployed, true);
        }
        
        if (ai_card_deployed != null)
        {
            TrackDiscardedCard(ai_card_deployed, false);
        }
        
        player_card_deployed = null;
        ai_card_deployed = null;
    }

    void DrawCards(int player, int amount)
    {
        for(int j = 0; j < amount; j++)
        {
            if(player == 1)
            {
                for(int i = 0; i < player_hand.Length; i++)
                {
                    if(player_hand[i] == null && player_deck.Count > 0)
                    {
                        int selection = Random.Range(0, player_deck.Count);
                        Card_data dealt = player_deck[selection];
                        player_deck.RemoveAt(selection);
                        player_hand[i] = dealt;
                        break;
                    }
                }
            }
            else
            {
                for(int i = 0; i < ai_hand.Length; i++)
                {
                    if(ai_hand[i] == null && ai_deck.Count > 0)
                    {
                        int selection = Random.Range(0, ai_deck.Count);
                        Card_data dealt = ai_deck[selection];
                        ai_deck.RemoveAt(selection);
                        ai_hand[i] = dealt;
                        break;
                    }
                }
            }
        }
    }

    // Add this method to track when a card is played
    void TrackDiscardedCard(Card_data card, bool isPlayer)
    {
        if (card == null) return;
        
        Dictionary<string, int> discards = isPlayer ? player_discards : ai_discards;
        
        if (card == punch_card) discards["Punch"]++;
        else if (card == power_fist_card) discards["Power Fist"]++;
        else if (card == pressure_point_card) discards["Pressure Point"]++;
        else if (card == stun_card) discards["Stun"]++;
        else if (card == divine_card) discards["Divine"]++;
        else if (card == block_card) discards["Block"]++;
        else if (card == full_send_card) discards["Full Send"]++;
    }
    
    // Add this method to update the discard pile text displays
    void UpdateDiscardText()
    {
        // Update player discard text
        string playerText = "Player Discard:\n";
        foreach (KeyValuePair<string, int> card in player_discards)
        {
            if (card.Value > 0)
            {
                playerText += card.Key + ": " + card.Value + "\n";
            }
        }
        player_discard.text = playerText;
        
        // Update AI discard text
        string aiText = "AI Discard:\n";
        foreach (KeyValuePair<string, int> card in ai_discards)
        {
            if (card.Value > 0)
            {
                aiText += card.Key + ": " + card.Value + "\n";
            }
        }
        ai_discard.text = aiText;
    }
}


/*
There are 3 excess meta files in the project, they are not used in the game. Please ignore warnings:
         turnPriorityUI
         turnPriorityUIBootstrap
         turnpriorityUIManager
*/


    



    

