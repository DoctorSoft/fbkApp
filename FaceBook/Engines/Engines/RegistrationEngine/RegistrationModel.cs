using System;
using System.Collections.Generic;
using Constants;
using Engines.Engines.FillingGeneralInformationEngine;

namespace Engines.Engines.RegistrationEngine
{
    public class RegistrationModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string FacebookPassword { get; set; }

        public string EmailPassword { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public string HomepageUrl { get; set; }

        public FillingGeneralInformationModel UserInfo { get; set; } 
    }
}
