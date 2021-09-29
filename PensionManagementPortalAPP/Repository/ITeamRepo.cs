using PensionManagementPortalAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortalAPP.Repository
{
    public interface ITeamRepo
    {
        IEnumerable<Team> GetTeam();
    }
}
