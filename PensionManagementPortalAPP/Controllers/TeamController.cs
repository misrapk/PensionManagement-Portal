using Microsoft.AspNetCore.Mvc;
using PensionManagementPortalAPP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortalAPP.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepo _teamRepo;

        public TeamController(ITeamRepo teamRepo)
        {
            _teamRepo = teamRepo;
        }
        public ViewResult Index()
        {
            var model =  _teamRepo.GetTeam();
            return View(model);
        }
    }
}
