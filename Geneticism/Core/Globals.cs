using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneticism.Core
{
    public static class Globals
    {
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ,.?!@#$%^&*()-=_+;'[]<>:";
        public static Random Rand = new Random();
        public static double BaseMutationChance = .01;
        public static int DefaultFitness = 0;
        public static char RandomChar()
        {
            return Alphabet[ThreadRandom.Next(Alphabet.Length)];
        }
    }
}
