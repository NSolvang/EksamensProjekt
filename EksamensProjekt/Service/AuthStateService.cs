using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EksamensProjekt.Service
{
    /// <summary>
    /// Service der håndterer notifikationer for ændringer i authentication state
    /// Bruges til at opdatere UI komponenter når brugerens login status ændrer sig
    /// </summary>
    public class AuthStateService
    {
        /// <summary>
        /// Event som bliver triggered når authentication state ændrer sig
        /// Komponenter kan subscribe til dette event for at opdatere deres UI
        /// </summary>
        public event Action? AuthStateChanged;
        
        /// <summary>
        /// Notificerer alle subscribers når authentication state er ændret
        /// Kaldes typisk efter successful login, logout eller token refresh
        /// </summary>
        public void NotifyAuthStateChanged()
        {
            // Invoke eventet hvis der er nogen subscribers
            // Null-conditional operator (?) sikrer at vi ikke får NullReferenceException
            AuthStateChanged?.Invoke();
        }
    }
}