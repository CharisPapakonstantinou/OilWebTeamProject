using OilTeamProject.Models.Factories;
using System.Collections.Generic;

namespace OilTeamProject.ViewModels
{
    public class ProducersData
    {
        public IEnumerable<UsersAccount> UsersAccounts;
        public IEnumerable<OilPress> OilPresses;
        public IEnumerable<Factory> Factories;

    }
}