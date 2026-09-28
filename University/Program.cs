
namespace CampusRegistrarSuite
{
    // Domain entity representing a student exam performance record
    public class StudentRecord
    {
        public int FinalMark { get; set; } // The target numeric key (0 - 100)
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string ModuleCode { get; set; }

        public StudentRecord(int finalMark, string studentNumber, string studentName, string moduleCode)
        {
            FinalMark = finalMark;
            StudentNumber = studentNumber;
            StudentName = studentName;
            ModuleCode = moduleCode;
        }

        public override string ToString() =>
            $"[Mark: {FinalMark,2}%] {StudentNumber} | {StudentName,-18} | Module: {ModuleCode}";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "University Academic Registry & Marks Auditor";

            // Unordered student records matching your exact sequence: 7, 2, 9, 4, 3, 8, 1 (scaled as marks)
            StudentRecord[] masterRegistry = GetInitialRegistry();

            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.WriteLine("========================================================================");
                Console.WriteLine("       UNIVERSITY REGISTRAR: MARKS SEARCHING & SORTING AUDITOR          ");
                Console.WriteLine("========================================================================");
                Console.WriteLine(" CURRENT SUBMITTED MARKS (UNSORTED EXAM SCRIPT ORDER):");
                PrintRegistry(masterRegistry);
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine(" --- SORTING BENCHMARKS ---");
                Console.WriteLine(" [1] Selection Sort: Find lowest mark and swap into place");
                Console.WriteLine(" [2] Insertion Sort: Insert each mark into an ordered sub-array");
                Console.WriteLine(" [3] Bubble Sort (Standard): Pairwise bubble passes across whole class");
                Console.WriteLine(" [4] Efficient Bubble Sort: Early exit when script order stabilizes");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine(" --- SEARCH BENCHMARKS ---");
                Console.WriteLine(" [5] Sequential Search: Unsorted linear scan across class scripts O(N)");
                Console.WriteLine(" [6] Binary Search: Divide-and-conquer on sorted mark registry O(log N)");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine(" [X] Exit Terminal");
                Console.WriteLine("========================================================================");
                Console.Write("Select Auditor Operation: ");

                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine("\n");

                switch (choice)
                {
                    case '1':
                        RunSelectionSort(CloneArray(masterRegistry));
                        break;
                    case '2':
                        RunInsertionSort(CloneArray(masterRegistry));
                        break;
                    case '3':
                        RunStandardBubbleSort(CloneArray(masterRegistry));
                        break;
                    case '4':
                        RunEfficientBubbleSort(CloneArray(masterRegistry));
                        break;
                    case '5':
                        RunSequentialSearch(masterRegistry);
                        break;
                    case '6':
                        RunBinarySearch(masterRegistry);
                        break;
                    case 'X':
                        active = false;
                        Console.WriteLine("Registry system logged off.");
                        break;
                    default:
                        Console.WriteLine("Invalid entry. Press any key to retry...");
                        Console.ReadKey();
                        break;
                }

                if (active)
                {
                    Console.Write("\nPress any key to return to Registrar Menu...");
                    Console.ReadKey();
                }
            }
        }

        #region Sorting Algorithms

        // 1. SELECTION SORT
        static void RunSelectionSort(StudentRecord[] records)
        {
            Console.WriteLine("--- Selection Sort: Marks Ascending ---");
            int comparisons = 0;
            int swaps = 0;

            for (int i = 0; i < records.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < records.Length; j++)
                {
                    comparisons++;
                    if (records[j].FinalMark < records[minIndex].FinalMark)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    StudentRecord temp = records[i];
                    records[i] = records[minIndex];
                    records[minIndex] = temp;
                    swaps++;
                }
            }

            PrintRegistry(records);
            Console.WriteLine($"\nTelemetry: {comparisons} Comparisons executed | {swaps} Record swaps performed.");
        }

        // 2. INSERTION SORT
        static void RunInsertionSort(StudentRecord[] records)
        {
            Console.WriteLine("--- Insertion Sort: Marks Ascending ---");
            int comparisons = 0;
            int shifts = 0;

            for (int i = 1; i < records.Length; i++)
            {
                StudentRecord temp = records[i];
                int j = i;

                while (j > 0)
                {
                    comparisons++;
                    if (records[j - 1].FinalMark > temp.FinalMark)
                    {
                        records[j] = records[j - 1]; // Shift record right
                        shifts++;
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                records[j] = temp;
            }

            PrintRegistry(records);
            Console.WriteLine($"\nTelemetry: {comparisons} Comparisons executed | {shifts} Element shifts performed.");
        }

        // 3. STANDARD BUBBLE SORT
        static void RunStandardBubbleSort(StudentRecord[] records)
        {
            Console.WriteLine("--- Standard Bubble Sort: Marks Ascending ---");
            int comparisons = 0;
            int swaps = 0;

            for (int i = 0; i < records.Length - 1; i++)
            {
                for (int j = 0; j < records.Length - 1 - i; j++)
                {
                    comparisons++;
                    if (records[j].FinalMark > records[j + 1].FinalMark)
                    {
                        StudentRecord temp = records[j];
                        records[j] = records[j + 1];
                        records[j + 1] = temp;
                        swaps++;
                    }
                }
            }

            PrintRegistry(records);
            Console.WriteLine($"\nTelemetry: {comparisons} Comparisons executed | {swaps} Swaps performed.");
        }

        // 4. EFFICIENT BUBBLE SORT
        static void RunEfficientBubbleSort(StudentRecord[] records)
        {
            Console.WriteLine("--- Optimized Bubble Sort (Early Exit) ---");
            int comparisons = 0;
            int swaps = 0;
            int passesRun = 0;
            bool isSwapped;

            for (int i = 0; i < records.Length - 1; i++)
            {
                isSwapped = false;
                passesRun++;

                for (int j = 0; j < records.Length - 1 - i; j++)
                {
                    comparisons++;
                    if (records[j].FinalMark > records[j + 1].FinalMark)
                    {
                        StudentRecord temp = records[j];
                        records[j] = records[j + 1];
                        records[j + 1] = temp;
                        swaps++;
                        isSwapped = true;
                    }
                }

                // If no elements were swapped, records are already in order
                if (!isSwapped)
                {
                    Console.WriteLine($"[EARLY EXIT]: List fully ordered on pass #{passesRun}. Halting unnecessary iterations!");
                    break;
                }
            }

            PrintRegistry(records);
            Console.WriteLine($"\nTelemetry: Finished in {passesRun} Passes | {comparisons} Comparisons | {swaps} Swaps.");
        }

        #endregion

        #region Searching Algorithms

        // 5. SEQUENTIAL / LINEAR SEARCH
        static void RunSequentialSearch(StudentRecord[] unsortedRecords)
        {
            Console.WriteLine("--- Sequential Search: Scanning Unsorted Exam Pile O(N) ---");
            Console.Write("Enter mark to find (e.g. 7, 2, 9, 4, 3, 8, 1): ");

            if (!int.TryParse(Console.ReadLine(), out int targetMark))
            {
                Console.WriteLine("Invalid mark value.");
                return;
            }

            int comparisons = 0;
            int foundIndex = -1;

            for (int i = 0; i < unsortedRecords.Length; i++)
            {
                comparisons++;
                if (unsortedRecords[i].FinalMark == targetMark)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex != -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[RECORD LOCATED] Found after {comparisons} check(s) at array index [{foundIndex}]:");
                Console.WriteLine("  " + unsortedRecords[foundIndex]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[NOT FOUND] Mark {targetMark} was not found after checking all {comparisons} scripts.");
            }
            Console.ResetColor();
        }

        // 6. BINARY SEARCH
        static void RunBinarySearch(StudentRecord[] records)
        {
            Console.WriteLine("--- Binary Search: Logarithmic Registry Query O(log N) ---");

            // Binary search requires pre-sorted data
            StudentRecord[] sorted = CloneArray(records);
            Array.Sort(sorted, (a, b) => a.FinalMark.CompareTo(b.FinalMark));

            Console.WriteLine("Pre-sorted Marks Registry:");
            PrintRegistry(sorted);

            Console.Write("\nEnter mark to find (e.g. 7, 2, 9, 4, 3, 8, 1): ");
            if (!int.TryParse(Console.ReadLine(), out int targetMark))
            {
                Console.WriteLine("Invalid mark value.");
                return;
            }

            int lower = 0;
            int upper = sorted.Length - 1;
            int steps = 0;
            int foundIndex = -1;

            Console.WriteLine("\n[TRACE LOG]:");
            while (lower <= upper)
            {
                steps++;
                int mid = (lower + upper) / 2;
                Console.WriteLine($" Step {steps}: Checking midpoint index [{mid}] (Mark: {sorted[mid].FinalMark}%). Active search slice: [{lower}..{upper}]");

                if (sorted[mid].FinalMark == targetMark)
                {
                    foundIndex = mid;
                    break;
                }
                else if (targetMark < sorted[mid].FinalMark)
                {
                    upper = mid - 1; // Discard upper half
                }
                else
                {
                    lower = mid + 1; // Discard lower half
                }
            }

            if (foundIndex != -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[RECORD LOCATED] Found in only {steps} binary division(s) at index [{foundIndex}]:");
                Console.WriteLine("  " + sorted[foundIndex]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[NOT FOUND] Mark {targetMark} does not exist in registry (Confirmed in {steps} divisions).");
            }
            Console.ResetColor();
        }

        #endregion

        #region Helpers & Data Seeding

        static void PrintRegistry(StudentRecord[] records)
        {
            for (int i = 0; i < records.Length; i++)
            {
                Console.WriteLine($"  [{i}] {records[i]}");
            }
        }

        static StudentRecord[] CloneArray(StudentRecord[] source)
        {
            StudentRecord[] copy = new StudentRecord[source.Length];
            for (int i = 0; i < source.Length; i++) copy[i] = source[i];
            return copy;
        }

        static StudentRecord[] GetInitialRegistry()
        {
            // Exact initial mark sequence from your original code: 7, 2, 9, 4, 3, 8, 1
            return new StudentRecord[]
            {
                new StudentRecord(7, "202100412", "Lethabo Khumalo",  "CSIS1614"),
                new StudentRecord(2, "202100893", "Sipho Ndlovu",     "CSIS1614"),
                new StudentRecord(9, "202101244", "Chantal Du Plessis","CSIS1614"),
                new StudentRecord(4, "202102115", "Kagiso Mokoena",   "CSIS1614"),
                new StudentRecord(3, "202103348", "Liam Van Zyl",     "CSIS1614"),
                new StudentRecord(8, "202104502", "Anrich Smith",     "CSIS1614"),
                new StudentRecord(1, "202105991", "Nthabiseng Dlamini","CSIS1614")
            };
        }

        #endregion
    }
}