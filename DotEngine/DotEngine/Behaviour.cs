namespace DotEngine;

public class Behaviour : Component
{
    private bool _isEnabled;

    public event Action<bool> EnabledChanged;

    public bool Enabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            EnabledChanged?.Invoke(value);
        }
    }
    
    public bool IsActiveAndEnabled => GameObject.IsActive && Enabled;
}