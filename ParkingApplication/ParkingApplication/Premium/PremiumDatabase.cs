using ParkingApplication.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.Premium
{
    class PremiumDatabase
    {
        Dictionary<string, PremiumUser> premiumUsers;
        ICodeGenerator generator;

        public PremiumDatabase(ICodeGenerator generator, List<PremiumUser> premiumUsers = null)
        {
            this.premiumUsers = new Dictionary<string, PremiumUser>();
            if(premiumUsers!=null)
            {
                foreach(PremiumUser t in premiumUsers)
                {
                    this.premiumUsers.Add(t.Code, t);
                }
            }
            this.generator = generator;
        }

        public PremiumUser RegisterPremiumUser(string plateNumber)
        {
            string code = generator.Generate();
            PremiumUser u = new PremiumUser(code, DateTime.Now, plateNumber);
            premiumUsers.Add(code, u);
            return u;
        }

        public PremiumUser GetPremiumUser(string plateNumber, string code)
        {
            if(premiumUsers.ContainsKey(code) && premiumUsers[code].RegistrationPlate == plateNumber)
            {
                return premiumUsers[code];
            }
            else
            {
                throw new InvalidPremiumUserCodeException();
            }
        }

        public PremiumUser FindUserByCode(string code) // TODO: bool?
        {
            if(premiumUsers.ContainsKey(code))
            {
                return premiumUsers[code];
            }

            throw new InvalidPremiumUserCodeException();
        }
    }
}
