using PensionManagementPortalAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortalAPP.Repository
{
    public class TeamRepo : ITeamRepo
    {
        public List<Team> _teamList;
        public TeamRepo()
        {
            _teamList = new List<Team>()
            {
                new Team{EmployeeID = 935317, EmployeeName = "Diksha", Designation = "Program Analyst Trainee"},
                new Team{EmployeeID = 935345, EmployeeName = "Peeyush", Designation = "Program Analyst Trainee"},
                new Team{EmployeeID = 935349, EmployeeName = "Satpreet", Designation = "Program Analyst Trainee"},
                new Team{EmployeeID = 935364, EmployeeName = "Shashank", Designation = "Program Analyst Trainee"},
                new Team{EmployeeID = 935382, EmployeeName = "Rahul", Designation = "Program Analyst Trainee"},
                new Team{EmployeeID = 935371, EmployeeName = "Ritvik", Designation = "Program Analyst Trainee"},
            };
        }
        public IEnumerable<Team> GetTeam()
        {
            return _teamList;
        }
    }
}
