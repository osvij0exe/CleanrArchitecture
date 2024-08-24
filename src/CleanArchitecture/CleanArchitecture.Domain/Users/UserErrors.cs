using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new(
            "User.Found",
            "No existe el usuario buscado por este id");

        public static Error InvalidcREDENTIALS = new(
            "User.InvalidCredentials",
            "Las credenciales son incorrectas");

    }
}
