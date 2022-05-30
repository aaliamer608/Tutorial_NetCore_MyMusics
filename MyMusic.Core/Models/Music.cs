using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Core
{
    public class Music
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ArtistID { get; set; }
        public Artist Artist { get; set; }
    }
}
