using System;
using System.Collections.Generic;
using System.Linq;

namespace UtahTraffix.Models
{
    public interface ICrashRepository
    {
        IQueryable<Crash> Crashes { get; }

        public void Save();
        public void Add(Crash crash);
        public void Edit(Crash crash);
        public void Delete(Crash crash);
        public List<Crash> GetCrashesFiltered(int page, int crashesPerPage, string searchString, string filterColumn); //This needs to be edited... I deleted a lot of stuff out of this function
        public List<Crash> GetCrashesSimple(int page, int crashesPerPage);
    }
}
