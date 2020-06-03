using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public static readonly float MIDDLE_LIMIT = .5f;
        public static readonly float RANGE_MIN = -100f;
        public static readonly float RANGE_MAX = 100f;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var rand = new Random();
            ChartValues<float> content = new ChartValues<float>();
            for (int i = 0; i < 30; i++)
            {
                double sample = rand.NextDouble();
                double scaled = (sample * (RANGE_MAX + Math.Abs(RANGE_MIN))) + RANGE_MIN;
                content.Add((float)scaled);
            }
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = content
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var allValues = cartesianChart1.Series[0].Values.Cast<float>().ToArray();
            if (allValues.Length < 2)
                return;
            List<float> contentMiddle;
            int debugCounter = 0;
            do
            {
                contentMiddle = new List<float>();
                List<float> contentUp = new List<float>();
                List<float> contentDown = new List<float>();
                float first = allValues[0];
                float last = first;
                bool nextIsUp = allValues[1] > last;
                int i = 1;
                foreach (int val in allValues.Skip(1))
                {
                    if (nextIsUp)
                    {
                        if (val < last)
                        {
                            contentUp.Add(last);
                            nextIsUp = false;
                        }
                        else
                        {
                            contentUp.Add(contentUp.Count > 0 ? contentUp.Last() : 0f);
                        }
                        contentDown.Add(contentDown.Count > 0 ? contentDown.Last() : 0f);
                    }
                    else
                    {
                        if (val > last)
                        {
                            contentDown.Add(last);
                            nextIsUp = true;
                        }
                        else
                        {
                            contentDown.Add(contentDown.Count > 0 ? contentDown.Last() : 0f);
                        }
                        contentUp.Add(contentUp.Count > 0 ? contentUp.Last() : 0f);
                    }
                    last = val;
                    i++;
                }
                var lastValue = allValues.Last();
                var beforeLastValue = allValues[allValues.Length - 2];
                if (nextIsUp)
                {
                    if (beforeLastValue < lastValue)
                    {
                        contentUp.Add(last);
                    }
                    else
                    {
                        contentUp.Add(contentUp.Count > 0 ? contentUp.Last() : 0f);
                    }
                    contentDown.Add(contentDown.Count > 0 ? contentDown.Last() : 0f);
                }
                else
                {
                    if (beforeLastValue > lastValue)
                    {
                        contentDown.Add(last);
                    }
                    else
                    {
                        contentDown.Add(contentDown.Count > 0 ? contentDown.Last() : 0f);
                    }
                    contentUp.Add(contentUp.Count > 0 ? contentUp.Last() : 0f);
                }
                for (int y = 0; y < contentUp.Count; y++)
                    contentMiddle.Add((contentUp[y] + contentDown[y]) / 2f);
                cartesianChart2.Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<float>(contentUp)
                        },
                        new LineSeries
                        {
                            Values = new ChartValues<float>(contentMiddle)
                        },
                        new LineSeries
                        {
                            Values = new ChartValues<float>(contentDown)
                        },
                        new LineSeries
                        {
                            Values = new ChartValues<float>(allValues)
                        }
                    };
                List<float> imf = new List<float>();
                List<float> residue = new List<float>();
                for (int y = 0; y < contentMiddle.Count; y++)
                {
                    var tmp = allValues[y] - contentMiddle[y];
                    imf.Add(tmp);
                    residue.Add(allValues[y] - tmp);
                }
                cartesianChart3.Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<float>(imf)
                        },
                        new LineSeries
                        {
                            Values = new ChartValues<float>(residue)
                        }
                    };
                allValues = residue.ToArray();
                MessageBox.Show(this, "WAIT");
                debugCounter++;
            } while (contentMiddle.Any(v => Math.Abs(v) > MIDDLE_LIMIT)); // 
            MessageBox.Show(this, "Done");
        }
    }
}
