using UnityEngine;
using TMPro;

public class TicTacToeManager : MonoBehaviour
{
    public TMP_Text text;
    public Tile[] tiles;
    public GameObject pauseMenu;

    public enum Turn
    {
        Cricle,
        Cross
    }
    public Turn turn = Turn.Cricle;

    private bool gameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        text.text = "It's now " + turn.ToString() + " turn";
    }

    public void UpdateTurn()
    {
        if (gameOver) return;

        switch (turn)
        {
            case Turn.Cricle:
                turn = Turn.Cross;
                break;
            case Turn.Cross:
                turn = Turn.Cricle;
                break;
        }

        text.text = "It's now " + turn.ToString() + " turn";
        CheckGameState();
    }

    public void CheckGameState()
    {
        Tile.state[,] board = new Tile.state[3, 3];

        foreach (Tile tile in tiles)
        {
            board[tile.indexA - 1, tile.indexB - 1] = tile.GetState();
        }

        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != Tile.state.Empty &&
                board[i, 0] == board[i, 1] &&
                board[i, 1] == board[i, 2])
            {
                EndGame(board[i, 0], false);
                return;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] != Tile.state.Empty &&
                board[0, i] == board[1, i] &&
                board[1, i] == board[2, i])
            {
                EndGame(board[0, i], false);
                return;
            }
        }

        if (board[0, 0] != Tile.state.Empty &&
            board[0, 0] == board[1, 1] &&
            board[1, 1] == board[2, 2])
        {
            EndGame(board[0, 0], false);
            return;
        }

        if (board[0, 2] != Tile.state.Empty &&
            board[0, 2] == board[1, 1] &&
            board[1, 1] == board[2, 0])
        {
            EndGame(board[0, 2], false);
            return;
        }

        bool draw = true;
        foreach (Tile tile in tiles)
        {
            if (tile.GetState() == Tile.state.Empty)
            {
                draw = false;
                break;
            }
        }

        if (draw)
        {
            EndGame(Tile.state.Empty, true);
        }
    }

    private void EndGame(Tile.state winner, bool isDraw)
    {
        gameOver = true;

        if (isDraw)
        {
            text.text = "Draw!";
        }
        else
        {
            text.text = winner.ToString() + " wins!";
        }

        foreach (Tile tile in tiles)
        {
            tile.enabled = false;
        }

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
}