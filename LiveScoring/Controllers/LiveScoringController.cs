﻿using LiveScoring.Models;
using LiveScoring.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiveScoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveScoringController : ControllerBase
    {
        private ILeaderboard _leaderboard;
        private IScoring _scoring;

        public LiveScoringController(ILeaderboard leaderboardService, IScoring scoring)
        {
            this._leaderboard = leaderboardService;
            this._scoring = scoring;
        }


        [HttpGet]
        [Route("leaderboard")]
        public IEnumerable<RaceResult> GetLeaderboard([FromQuery] Sport sport, [FromQuery] int gp)
        {
            return this._leaderboard.GetLeaderboard(sport, gp);
        }


        [HttpGet]
        [Route("leaderboard/sport/{sport}/gp/{gp}")]
        public IEnumerable<RaceResult> GetLeaderboard2([FromRoute] Sport sport, [FromRoute] int gp)
        {
            return this._leaderboard.GetLeaderboard(sport, gp);
        }

        [HttpPost]
        [Route("score/sport/{sport}/gp/{gp}")]
        public ActionResult<int> GetScoring([FromRoute] Sport sport, [FromRoute] int gp, [FromBody] IList<Driver> drivers)
        {
            if(drivers == null || drivers.Count() != 3)
            {
                return BadRequest();
            }

            IList<RaceResult> leaderboard = this._leaderboard.GetLeaderboard(sport, gp);
            return this._scoring.CalculateScore(leaderboard, drivers);
        }

        [HttpPost]
        [Route("score-detailed/sport/{sport}/gp/{gp}")]
        public ActionResult<Dictionary<int, IList<ScoringEvent>>> GetDetailedScoring([FromRoute] Sport sport, [FromRoute] int gp, [FromBody] IList<Driver> drivers)
        {
            if (drivers == null || drivers.Count() != 3)
            {
                return BadRequest();
            }

            IList<RaceResult> leaderboard = this._leaderboard.GetLeaderboard(sport, gp);
            return this._scoring.CalculateDetaildScore(leaderboard, drivers);
        }

    }
}