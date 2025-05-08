using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EksamensProjekt.Service
{
    public class AuthStateService
    {
        public event Action? AuthStateChanged;
        

        public void NotifyAuthStateChanged()
        {
            AuthStateChanged?.Invoke();
        }
    }
}