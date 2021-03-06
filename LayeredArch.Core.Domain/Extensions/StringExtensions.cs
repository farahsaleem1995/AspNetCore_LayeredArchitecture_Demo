﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Extensions
{
    public static class StringExtensions
    {
        public static int Levenshtein(this string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
        public static bool IsLevenshtein(this string s, string t, int threshold)
        {
            int levenshteinThreshold;
            if (Math.Abs(t.Length - s.Length) > threshold)
            {
                levenshteinThreshold = Math.Abs(t.Length - s.Length);
            }
            else
            {
                levenshteinThreshold = threshold;
            }

            var wordDistance = (t.ToLower()).Levenshtein(s.Trim().ToLower());
            if (wordDistance <= levenshteinThreshold)
            {
                return true;
            }
            return false;
        }

    }
}
