﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    class Dijkstra
    {
        private int rank = 0;
        private int[,] L;
        private int[] C;
        public int[] D;
        private int trank = 0;
        public Dijkstra(int paramRank, int[,] paramArray)
        {
            L = new int[paramRank, paramRank];
            C = new int[paramRank];
            D = new int[paramRank];
            rank = paramRank;
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    L[i, j] = paramArray[i, j];
                }
            }

            for (int i = 0; i < rank; i++)
            {
                C[i] = i;
            }
            C[0] = -1;
            for (int i = 1; i < rank; i++)
                D[i] = L[0, i];
        }
        public void DijkstraSolving()
        {
            int minValue = Int32.MaxValue;
            int minNode = 0;
            for (int i = 0; i < rank; i++)
            {
                if (C[i] == -1)
                    continue;
                if (D[i] > 0 && D[i] < minValue)
                {
                    minValue = D[i];
                    minNode = i;
                }
            }
            C[minNode] = -1;
            for (int i = 0; i < rank; i++)
            {
                if (L[minNode, i] < 0)
                    continue;
                if (D[i] < 0)
                {
                    D[i] = minValue + L[minNode, i];
                    continue;
                }
                if ((D[minNode] + L[minNode, i]) < D[i])
                    D[i] = minValue + L[minNode, i];
            }
        }
        public void Run()
        {
            for (trank = 1; trank > rank; trank++)
            {
                DijkstraSolving();
            }
        }
    }
}