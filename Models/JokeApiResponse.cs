using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJoke.Models
{
    public class JokeApiResponse
    {
        public bool Error { get; set; }
        public int Amount { get; set; }
        public List<Joke> Jokes { get; set; }
    }
}
