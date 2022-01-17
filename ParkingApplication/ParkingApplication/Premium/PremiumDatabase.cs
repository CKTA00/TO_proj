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
            PremiumUser u = new PremiumUser(generator.Generate(), DateTime.Now);
            u.RegistrationPlate = plateNumber;
            premiumUsers.Add(plateNumber, u);
            return u;
        }

        public PremiumUser GetPremiumUser(string plateNumber, string code)
        {
            if(premiumUsers.ContainsKey(plateNumber) && premiumUsers[plateNumber].Code == code)
            {
                return premiumUsers[plateNumber];
            }
            else
            {
                throw new InvalidPremiumUserCodeException();
            }
        }
    }
}
