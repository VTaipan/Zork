using UnityEngine;
using Newtonsoft.Json;
using Zork.Common;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI LocationText;
   
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    
    [SerializeField]
    private TextMeshProUGUI MovesText;

    [SerializeField]
    private UnityInputService InputService;

    [SerializeField]
    private UnityOutputService OutputService;

    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
        _game.Player.LocationChanged += Player_LocationChanged;
        //_game.Player.MovesChanged += Player_MoveChange;
        //_game.Player.ScoreChanged += Player_ScoreChange;
        _game.Run(InputService, OutputService);
    }

    private void Player_LocationChanged(object sender, Room location)
    {
        LocationText.text = location.Name;
    }

    //private void Player_ScoreChange(object sender, Player _score)
    //{
    //    ScoreText.text = "Score: " + _score.Score.ToString();
    //}

    //private void Player_MoveChange(object sender, Player _moves)
    //{
    //        MovesText.text = "Moves: " + _moves.Moves.ToString();
    //}

    private void Start()
    {
        InputService.SetFocus();
        LocationText.text = _game.Player.CurrentRoom.Name;
        ScoreText.text = "Score: 0";
        MovesText.text = "Moves: 0";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            InputService.ProcessInput();
            InputService.SetFocus();
        }

        if (_game.IsRunning == false)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

#else
            Application.Quit();
#endif
        }
    }

    private Game _game;
}
