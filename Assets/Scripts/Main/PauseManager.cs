using System.Collections.Generic;
using UnityEngine;

using Unit;

public class PauseManager : MonoBehaviour
{
    private readonly List<IPauseHandler> pauseHandlers = new List<IPauseHandler>();

    [SerializeField] private Player player;
    [SerializeField] private UI_Elements pauseUI;

    public bool IsPaused { get; private set; }

    public static PauseManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            pauseUI.OnContinueButtonPressed.AddListener(Continue);
            player.OnPlayerShipsOver.AddListener(Pause);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Continue();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (GameFinisher.Instance.GameIsOver)
            return;

        if (IsPaused)
            Continue();
        else
            Pause();
    }

    public void Register(IPauseHandler handler)
    {
        pauseHandlers.Add(handler);
    }

    public void Unregister(IPauseHandler handler)
    {
        pauseHandlers.Remove(handler);
    }

    private void Pause()
    {
        IsPaused = true;

        Cursor.visible = true;

        pauseUI.ActivatePauseUI();

        for (var i = 0; i < pauseHandlers.Count; i++)
            pauseHandlers[i].SetPauseState(IsPaused);
    }

    private void Continue()
    {
        IsPaused = false;

        Cursor.visible = false;

        pauseUI.DeactivatePauseUI();

        for (var i = 0; i < pauseHandlers.Count; i++)
            pauseHandlers[i].SetPauseState(IsPaused);
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}