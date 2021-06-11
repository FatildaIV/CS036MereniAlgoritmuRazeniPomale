using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace CS031MereniAlgoritmuProhozeni
{
    public partial class MereniAlgoritmuProhozeniForm : Form
    {
        public MereniAlgoritmuProhozeniForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            StopkyTestDateTime();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestHighResolutionDateTime();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestStopwatchHashSet();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestStopwatch();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestStopwatchDrift();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
        }

        private void StopkyTestDateTime()
        {

            vystupTextBox.AppendText("Testing DateTime for 1 seconds...");

            var distinctValues = new HashSet<DateTime>();
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                distinctValues.Add(DateTime.UtcNow);
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Precision: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / distinctValues.Count,
                    distinctValues.Count));

        }

        private void StopkyTestHighResolutionDateTime()
        {

            vystupTextBox.AppendText("Testing High Resolution DateTime for 1 seconds...");

            var distinctValues = new HashSet<DateTime>();
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                distinctValues.Add(HighResolutionDateTime.UtcNow);
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Precision: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / distinctValues.Count,
                    distinctValues.Count));

        }

        private void StopkyTestStopwatchHashSet()
        {

            vystupTextBox.AppendText("Testing Stopwatch for 1 seconds...");

            var distinctValues = new HashSet<long>();
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                distinctValues.Add(sw.ElapsedTicks);
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Resolution: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / distinctValues.Count,
                    distinctValues.Count));

        }

        private void StopkyTestStopwatch()
        {

            vystupTextBox.AppendText("Testing Stopwatch for 1 seconds...");

            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {

            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Resolution: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / sw.ElapsedTicks,
                    sw.ElapsedTicks));

        }

        private void StopkyTestStopwatchDrift()
        {
            var start = HighResolutionDateTime.UtcNow;
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 10)
            {
                DateTime nowBasedOnStopwatch = start + sw.Elapsed;
                TimeSpan diff = HighResolutionDateTime.UtcNow - nowBasedOnStopwatch;

                vystupTextBox.AppendText(string.Format("Stopwatch to UTC drift: {0:0.000000} ms", diff.TotalMilliseconds));
                vystupTextBox.AppendText(Environment.NewLine);

                Thread.Sleep(1000);
            }
        }
        private void ProhoditPromenna<T>(ref T a, ref T b)
        {
            T pom = a;
            a = b;
            b = pom;
        }

        private static void StaticProhoditPromenna<T>(ref T a, ref T b)
        {
            T pom = a;
            a = b;
            b = pom;
        }
        private void ProhoditPromenna(ref int a, ref int b)
        {
            int pom = a;
            a = b;
            b = pom;
        }
        public delegate void ProceduraProhozeni(ref int a, ref int b);

        private long OpakovatProhozeni(ProceduraProhozeni proceduraProhozeni, int n)
        {
            var sw = Stopwatch.StartNew();
            int a = 1;
            int b = 2;
            for (int i = 0; i < n; i++)
            {
                proceduraProhozeni(ref a, ref b);
            }
            return sw.ElapsedMilliseconds;
        }
        private void MeritProhozeni(int max)
        {
            VytizitProcesor();
            string vypis = "Prohození s pomocnou proměnnou {1}x: {0:0.000000} ms";
            vypis = "{0};{1:0.000000};{2:0.000000};{3:0.000000}";
            for (int i = 1; i < max; i *= 10)
            {





                vystupTextBox.AppendText(
                    string.Format(
                        vypis,
                        i,
                         OpakovatProhozeni(ProhoditPromenna, i),
                         OpakovatProhozeni(ProhoditPromenna<int>, i),
                         OpakovatProhozeni(StaticProhoditPromenna<int>, i)));


                vystupTextBox.AppendText(Environment.NewLine);

            }
        }

        private void meritProhozeniButton_Click(object sender, EventArgs e)
        {
            MeritProhozeni(100000000);
        }
        private void VytizitProcesor()
        {
            OpakovatProhozeni(ProhoditPromenna, 100000000);
        }

        private void MereniAlgoritmuProhozeniForm_Load(object sender, EventArgs e)
        {

        }
        // metoda potvrdí zda pole je seřazeno zadaným způsobem, nebo neseřazeno

        static Razeni PoleRazeni<T>(T[] pole)
        {
            bool vzestupne = true, sestupne = true;

            for (int i = 0; i < pole.Length - 1; i++)
            {
                 vzestupne = vzestupne &&((dynamic)pole[i] <= (dynamic)pole[i + 1]);
                 sestupne = sestupne &&((dynamic)pole[i] >= (dynamic)pole[i + 1]);

            }
            return (vzestupne && sestupne)? Razeni.Serazeno:
                   (!vzestupne && !sestupne)? Razeni.Neserazeno:
                   (vzestupne)? Razeni.Vzestupne:
                   Razeni.Sestupne;
        }

        enum Razeni
        {
            Neserazeno,
            Serazeno,
            Sestupne,
            Vzestupne            
        }
        static void BubbleSort<T>(ref T[] pole, Razeni razeni)
        {
            for (int i = 0; i < pole.Length; i++)
            {      
                for (int j = 0; j < pole.Length - 1; j++)
                {
                    bool podminka = ((dynamic)pole[j] < (dynamic)pole[j + 1]);

                    if (razeni == Razeni.Sestupne ? podminka : !podminka)
                    {      
                      StaticProhoditPromenna<T>(ref pole[j], ref pole[j + 1]);           
                    }
                }
            }
        }

        static void SelectionSort<T>(ref T[] pole, Razeni razeni)
        {
            for (int i = 0; i < pole.Length - 1; i++)
            {  
                int minIndex = i;           
                for (int j = i; j < pole.Length; j++)
                {
                    bool podminka = ((dynamic)pole[j] < (dynamic)pole[minIndex]);

                    if (razeni == Razeni.Sestupne ? podminka : !podminka)
                    {
                        minIndex = j;
                    }
                }
                StaticProhoditPromenna<T>(ref pole[i], ref pole[minIndex]);
            }
        }
        static void InsertionSort<T>(ref T[] pole, Razeni razeni)
        {
            for (int i = 0; i < pole.Length; i++)
            {
                int j = i + 1;
                bool podminka = ((dynamic)pole[j - 1] < (dynamic)pole[j]);
                bool prohazovat = (razeni == Razeni.Sestupne ? podminka : !podminka);

                while (j > 1 && prohazovat)
                {
                   
                    StaticProhoditPromenna<T>(ref pole[j - 1], ref pole[j]);
                    j--;
                    podminka = ((dynamic)pole[j - 1] < (dynamic)pole[j]);
                    prohazovat = (razeni == Razeni.Sestupne ? podminka : !podminka);
                }
            }
        }


        private void MeritRazeni(int max)
        {
            MeritBubbleSort(1000);
            
            //VytizitProcesor();
            WriteLine("Počet položek;Bubble Sort;Selection Sort;Insertion Sort");
            for (int i = 10; i < max; i *= 2)
            {
                WriteLine(
                    string.Format(
                        "{0};{1:0.000000};{2:0.000000};{3:0.000000}",
                        i,
                        MeritBubbleSort(i),
                        MeritSelectionSort(i),
                        MeritInsertionSort(i)));

        }
        }

        private double MeritBubbleSort(int pocetPolozek)
        {
            int[] cisla = NewRandomArray(pocetPolozek, -20, 20);
            var sw = Stopwatch.StartNew();
            BubbleSort(ref cisla,Razeni.Vzestupne);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        private double MeritSelectionSort(int pocetPolozek)
        {
            int[] cisla = NewRandomArray(pocetPolozek, -20, 20);
            var sw = Stopwatch.StartNew();
            SelectionSort(ref cisla, Razeni.Vzestupne);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }
        private double MeritInsertionSort(int pocetPolozek)
        {
            int[] cisla = NewRandomArray(pocetPolozek, -20, 20);
            var sw = Stopwatch.StartNew();
            InsertionSort(ref cisla, Razeni.Vzestupne);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        private void meritRazeniButton_Click(object sender, EventArgs e)
        {
            int[] cisla = NewRandomArray(20, -20, 20);


            WriteLine(string.Format("{0}, {1}", ToString(cisla), PoleRazeni(cisla)));
            InsertionSort(ref cisla,Razeni.Vzestupne);
            WriteLine(string.Format("{0}, {1}", ToString(cisla), PoleRazeni(cisla)));
            InsertionSort(ref cisla, Razeni.Sestupne);
            WriteLine(string.Format("{0}, {1}", ToString(cisla), PoleRazeni(cisla)));
            WriteLine();
            MeritRazeni(15000);
        }

        private Random generator = new Random();

        private int[]  NewRandomArray(int length, int min, int max)
        {
            int[] numbers = new int[length];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = generator.Next(min, max);
            }
            return numbers;
        }

        private string ToString<T>(T[] arr)
        {
            return string.Format("{0}", string.Join(", ", arr));
        }

        private void WriteLine()
        {
            vystupTextBox.AppendText(Environment.NewLine);
        }

        private void WriteLine(string text)
        {
            vystupTextBox.AppendText(text);
            WriteLine();
        }
    }
    }
