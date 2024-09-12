using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJoke.Models
{
    public class Joke
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public string Setup { get; set; }
        public string Delivery { get; set; }
        public string JokeText { get; set; } // For single-type jokes
        public Flags Flags { get; set; }
        public int Id { get; set; }
        public bool Safe { get; set; }
        public string Lang { get; set; }
    }
}
