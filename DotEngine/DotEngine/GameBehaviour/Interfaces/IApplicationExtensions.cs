namespace DotEngine;

public interface IApplicationExtensions
{
    void OnApplicationFocus(bool hasFocus);
    void OnApplicationPause(bool pauseStatus);
    void OnApplicationQuit();
}