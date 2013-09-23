using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ayende.com
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 1, 59, 12, 43, 4, 58, 5, 13, 46, 3, 6 }; // 1, 2, 3, 5, 6, 7, 4 };

            var lists = Solution(A);

            var numbers = new List<int>();
            numbers.AddRange(lists.Select(r => r.First()));
            numbers.AddRange(lists.Select(r => r.Last()));

            var joinedgroup = Solution(numbers.ToArray());

            if (joinedgroup.Count != numbers.Count)
            {
                foreach (var joined in joinedgroup.Where(r => r.Count > 1))
                {
                    var seqLo = lists.Where(r => r.Last() == joined.First()).SingleOrDefault();
                    var seqHi = lists.Where(r => r.First() == joined.Last()).SingleOrDefault();
                    if (seqHi == null || seqLo == null)
                        continue;

                    seqLo.AddRange(seqHi);

                    lists.Remove(seqHi);
                }
            }

            foreach (var seq in lists)
            {
                Console.WriteLine();
                foreach (var number in seq)
                {
                    Console.Write("{0}, ", number);
                }
            }

            Console.ReadKey();
        }

        private static IList<List<int>> Solution(int[] A)
        {
            List<List<int>> sequences = new List<List<int>>();
            bool isAdded;
            sequences.Add(new List<int>());
            sequences[0].Add(A[0]);

            for (int i = 1; i < A.Length; i++)
            {
                isAdded = false;
                for (int index = 0; index < sequences.Count; index++)
                {
                    var sequence = sequences[index];

                    int last = sequence.Last();
                    int first = sequence.First();

                    if (Math.Abs(A[i] - first) == 1)
                    {
                        int insertIndex = ((A[i] - first) + 1) / 2;
                        isAdded = true;
                        sequence.Insert(insertIndex, A[i]);
                        break;
                    }
                    else
                        if (Math.Abs(A[i] - last) == 1)
                        {
                            int insertOffset = (A[i] - last - 1) / 2;
                            isAdded = true;
                            sequence.Insert(sequence.Count - insertOffset, A[i]);
                            break;
                        }
                }
                if (!isAdded)
                {
                    sequences.Add(new List<int>() { A[i] });
                }
            }
            return sequences;
        }
    }
}
