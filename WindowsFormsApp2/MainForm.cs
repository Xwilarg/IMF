using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public static readonly float RANGE_MIN = -100f;
        public static readonly float RANGE_MAX = 100f;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            var rand = new Random();
            List<float> numbers = new List<float>();
            for (int i = 0; i < 30; i++)
            {
                double sample = rand.NextDouble();
                double scaled = (sample * (RANGE_MAX + Math.Abs(RANGE_MIN))) + RANGE_MIN;
                numbers.Add((float)scaled);
            }
            string text = string.Join(", ", numbers);
            UpdateGraph(text);
            textBox1.Text = text;
        }

        private void button1_Click(object _, EventArgs __)
        {
            new DecompositionForm(this, cartesianChart1.Series[0].Values.Cast<float>().ToArray()).Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            UpdateGraph(tb.Text);
        }

        private void UpdateGraph(string text)
        {
            var values = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ChartValues<float> content = new ChartValues<float>();
            foreach (string s in values)
            {
                if (float.TryParse(s, out float f))
                {
                    content.Add(f);
                }
                else
                {
                    cartesianChart1.Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<float>()
                        }
                    };
                    return;
                }
            }
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = content
                }
            };
        }
    }
}
