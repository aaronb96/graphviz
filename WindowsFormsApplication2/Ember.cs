using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Ember
    {
        public bool MarBejarva = false;
        public Ember parja;
        public List<Ember> gyermeke = new List<Ember>();

        public string Nev;

        public Ember(string Nev)
        {
            this.Nev = Nev;
        }

       
    }
}
