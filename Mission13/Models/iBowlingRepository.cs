using System;
using System.Collections.Generic;
using System.Linq;

namespace Mission13.Models
{
    public interface iBowlingRepository
    {
        IQueryable<Bowler> Bowlers { get; }
        IQueryable<Team> Teams { get; }

        public void Add(Bowler bowler);
        public void Edit(Bowler bowler);
        public void Delete(Bowler bowler);
        public List<Bowler> GetBowlersFiltered(int teamId);
    }
}
