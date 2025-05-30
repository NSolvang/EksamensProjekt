/// <summary>
/// Service til håndtering af ændringer i login-status.
/// Bruges til at opdatere UI, når bruger logger ind eller ud.
/// </summary>
public class AuthStateService
{
    /// <summary>
    /// Event, der bliver kaldt når login-status ændrer sig.
    /// UI-komponenter kan lytte til dette for at opdatere sig.
    /// </summary>
    public event Action? AuthStateChanged;
    
    /// <summary>
    /// Kalder eventet for at fortælle, at login-status er ændret.
    /// Bruges efter login, logout eller token-opdatering.
    /// </summary>
    public void NotifyAuthStateChanged()
    {
        AuthStateChanged?.Invoke();
    }
}