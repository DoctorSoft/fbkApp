using System;
using Constants;

namespace Engines.Engines.RegistrationEngine
{
    public class RegistrationModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }
    }
}
