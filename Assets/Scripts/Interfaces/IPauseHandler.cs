public interface IPauseHandler
{
    bool isPaused { get; set; }

    void SetPauseState(bool isPaused);
}