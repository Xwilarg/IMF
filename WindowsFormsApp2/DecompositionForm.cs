using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class DecompositionForm : Form
    {
        public static readonly int BUTTON_POS_X = 800;
        public static readonly int BUTTON_POS_Y = 230;
        public static readonly int BUTTON_SIZE_X = 75;
        public static readonly int BUTTON_SIZE_Y = 23;
        public static readonly int BUTTON_SPACE_Y = 10;

        private static Dictionary<Button, int> imfButtons;

        private float[] allValues;

        private Form mainForm;

        public DecompositionForm(Form mainForm, float[] values)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            allValues = values;
            imfButtons = new Dictionary<Button, int>();
        }

        private void DecompositionForm_Load(object sender, EventArgs e)
        {
            cartesianChart1.Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<float>(allValues)
                        }
                    };
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

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Dispose();
        }
    }
}
