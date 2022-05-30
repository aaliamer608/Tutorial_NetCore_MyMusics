using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Core
{
    public class Artist
    {
        public Artist()
        {
            Musics = new Collection<Music>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Music> Musics { get; set; }
    }
}
