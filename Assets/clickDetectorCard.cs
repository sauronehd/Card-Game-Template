using UnityEngine;

public class clickDetectorCard : MonoBehaviour
{
    public GameObject parent;

    private void OnMouseDown()
    {
        Card card = parent.GetComponent<Card>();
        if (card != null)
        {
            card.gm.player_hand[card.ID - 1] = null;
            card.data = null;
            print("clicked " + card.ID);
        }
    }
}

