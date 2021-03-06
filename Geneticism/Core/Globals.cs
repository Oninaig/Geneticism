﻿using System;
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
        public static double BaseMutationChance = .02;
        public static int DefaultFitness = 0;
        public static char RandomChar()
        {
            return Alphabet[ThreadRandom.Next(Alphabet.Length)];
        }


        //threadstatic string builder for gc performance
        [ThreadStatic] private static StringBuilder _localBuilder;

        public static StringBuilder GetStringBuilder(string startingStr = null)
        {
            StringBuilder builder = _localBuilder;
            if (builder == null)
            {
                builder = _localBuilder = new StringBuilder();
            }
            builder.Clear();
            if (startingStr != null)
                builder.Append(startingStr);
            return builder;
        }
    }
}
