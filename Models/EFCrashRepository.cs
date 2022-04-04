using System;
using System.Collections.Generic;
using System.Linq;
namespace UtahTraffix.Models
{
    public class EFCrashRepository : iCrashRepository
    { 
        private CrashDbContext _context { get; set; }

        public EFCrashRepository(CrashDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<Crash> Crashes => _context.Crashes;
       

        public void Add(Crash Crash)
        {
            _context.Add(Crash);
            _context.SaveChanges();
        }
        public void Edit(Crash Crash)
        {
            _context.Update(Crash);
            _context.SaveChanges();
        }
        public void Delete(Crash Crash)
        {
            _context.Remove(Crash);
            _context.SaveChanges();
        }

        public List<Crash> GetCrashesFiltered() //This needs to be edited... I deleted a lot of stuff out of this function
        {
            
            return _context.Crashes.ToList();
            
        }
    }
}




