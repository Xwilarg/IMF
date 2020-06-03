using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public static readonly float MIDDLE_LIMIT = .5f;
        public static readonly float RANGE_MIN = -100f;
        public static readonly float RANGE_MAX = 100f;

        public static readonly int BUTTON_POS_X = 800;
        public static readonly int BUTTON_POS_Y = 230;
        public static readonly int BUTTON_SIZE_X = 75;
        public static readonly int BUTTON_SIZE_Y = 23;
        public static readonly int BUTTON_SPACE_Y = 10;

        private static Dictionary<Button, int> imfButtons;

        public Form1()
        {
            InitializeComponent();
            imfButtons = new Dictionary<Button, int>();
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

        private static void DeleteAllButtons()
        {
            foreach (var b in imfButtons)
            {
                b.Key.Dispose();
            }
            imfButtons.Clear();
        }

        private void button1_Click(object _, EventArgs __)
        {
            DeleteAllButtons();
            var allValues = cartesianChart1.Series[0].Values.Cast<float>().ToArray();
            if (allValues.Length < 2)
                return;
            List<float> contentMiddle;
            int nbSpikes;
            int imfCount = 1;
            List<LineSeries> allImfs = new List<LineSeries>();
            do
            {
                contentMiddle = new List<float>();
                List<float> contentUp = new List<float>();
                List<float> contentDown = new List<float>();
                nbSpikes = 0;
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
                            nbSpikes++;
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
                contentUp.Add(0f);
                contentDown.Add(0f);
                for (int y = 0; y < contentUp.Count; y++)
                    contentMiddle.Add((contentUp[y] + contentDown[y]) / 2f);
                List<float> imf = new List<float>();
                List<float> residue = new List<float>();
                for (int y = 0; y < contentMiddle.Count; y++)
                {
                    var tmp = allValues[y] - contentMiddle[y];
                    imf.Add(tmp);
                    residue.Add(allValues[y] - tmp);
                }
                allImfs.Add(new LineSeries
                {
                    Values = new ChartValues<float>(imf),
                    Title = "IMF n°" + imfCount
                });
                Button b = new Button();
                Controls.Add(b);
                b.Text = "IMF n°" + imfCount;
                b.Location = new Point(BUTTON_POS_X, BUTTON_POS_Y + ((BUTTON_SIZE_Y + BUTTON_SPACE_Y) * imfCount));
                b.Size = new Size(BUTTON_SIZE_X, BUTTON_SIZE_Y);
                b.Click += (object obj, EventArgs ___) =>
                {
                    SeriesCollection tmp = new SeriesCollection();
                    var imfs = cartesianChart4.Series.ToList();
                    var button = (Button)obj;
                    var deleted = imfButtons[button];
                    button.Dispose();
                    imfs.RemoveAt(deleted);
                    tmp.AddRange(imfs);
                    cartesianChart4.Series = tmp;
                    List<Button> toModify = new List<Button>();
                    foreach (var bt in imfButtons)
                    {
                        if (bt.Value > deleted)
                            toModify.Add(bt.Key);
                    }
                    foreach (var bt in toModify)
                    {
                        imfButtons[bt] -= 1;
                    }
                };
                imfButtons.Add(b, imfCount - 1);
                cartesianChart3.Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<float>(imf),
                            Title = "IMF"
                        },
                        new LineSeries
                        {
                            Values = new ChartValues<float>(residue),
                            Title = "Résidu"
                        }
                    };
                allValues = residue.ToArray();
                imfCount++;
            } while (nbSpikes >= 2); //while (contentMiddle.Any(v => Math.Abs(v) > MIDDLE_LIMIT));
            SeriesCollection sc = new SeriesCollection();
            sc.AddRange(allImfs);
            cartesianChart4.Series = sc;
        }
    }
}
