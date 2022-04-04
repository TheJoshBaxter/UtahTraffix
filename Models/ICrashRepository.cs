using System;
using System.Collections.Generic;
using System.Linq;

namespace UtahTraffix.Models
{
    public interface iCrashRepository
    {
        IQueryable<Crash> Crashes { get; }

        public void Add(Crash crash);
        public void Edit(Crash crash);
        public void Delete(Crash crash);
        public List<Crash> GetCrashesFiltered(); //This needs to be edited
    }
}
