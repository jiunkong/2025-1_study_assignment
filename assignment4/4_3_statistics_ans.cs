using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            
            double[] average = new double[3]; // math, science, english
            Tuple<int, double>[] totalscore = new Tuple<int, double>[stdCount];
            double[] maxscore = { 0, 0, 0 };
            double[] minscore = { 1000, 1000, 1000 };
            
            for (int i = 0; i < stdCount; i++) {
                double t = 0.0;
                for (int j = 0; j < 3; j++) t += double.Parse(data[i+1,j+2]);
                totalscore[i] = Tuple.Create(i, t);
            }
            
            Array.Sort(totalscore, (a, b) => a.Item2 < b.Item2 ? 1 : -1);
            
            for (int i = 0; i < 3; i++) { // subject
                double total = 0.0;
                for (int j = 0; j < stdCount; j++) {
                    double score = double.Parse(data[j+1,i+2]);
                    total += score;
                    if (maxscore[i] < score) maxscore[i] = score;
                    if (minscore[i] > score) minscore[i] = score;
                }
                average[i] = total / stdCount;
            }
            Console.WriteLine("Average Scores:");
            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"{data[0,i+2]}: {average[i]:0.00}");
            }

            Console.WriteLine("\nMax and min Scores:");
            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"{data[0,i+2]}: ({maxscore[i]}, {minscore[i]})");
            }
            
            Console.WriteLine("\nStudents rank by total scores:");
            for (int i = 0; i < stdCount; i++) {
                for (int j = 0; j < stdCount; j++) {
                    if (totalscore[j].Item1 == i) {
                        string rank = (j+1).ToString();
                        if (rank == "1") rank += "st";
                        else if (rank == "2") rank += "nd";
                        else if (rank == "3") rank += "rd";
                        else rank += "th";
                        Console.WriteLine($"{data[i+1,1]}: {rank}");
                        break;
                    }
                }
            }
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 2nd
Bob: 5th
Charlie: 1st
David: 4th
Eve: 3rd

*/
