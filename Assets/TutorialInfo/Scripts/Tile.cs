using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TicTacToeManager ticTacToeManager;
    Vector3 originalScale;
    public enum state
    {
        Circle,
        Cross,
        Empty
    }

    public int indexA;
    public int indexB;
    private TMP_Text textState; 

    public state TileState = Tile.state.Empty;

    public state GetState()
    {
        return TileState;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textState = GetComponentInChildren<TMP_Text>();
        originalScale = transform.localScale;
        textState.raycastTarget = false;
    }


    public void OnClick()
    {
        Debug.Log("Clicked on tile:["+indexA+","+indexB+"]...");
        switch (TileState) {
            case state.Empty:
                var turn = ticTacToeManager.turn;
                if(turn == TicTacToeManager.Turn.Cricle)
                {
                    TileState = state.Circle;
                    textState.text = "O";
                    Debug.Log("... and set to Circle");
                }
                else
                {
                    TileState = state.Cross;
                    textState.text = "X";
                    Debug.Log("... and set to Cross");
                }
                ticTacToeManager.UpdateTurn();
                return;
            default:
                Debug.Log("... but the tile already has been set to "+TileState);
                return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
