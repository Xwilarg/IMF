using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class RecompositionForm : Form
    {
        private float[][] allImfs;
        private float[] residue;

        Form mainForm;

        public RecompositionForm(Form mainForm, float[][] allImfs, float[] residue)
        {
            InitializeComponent();
            this.allImfs = allImfs;
            this.residue = residue;
            this.mainForm = mainForm;
        }

        private void RecompositionForm_Load(object sender, EventArgs e)
        {
            List<float> values = new List<float>();
            for (int i = 0; i < residue.Length; i++)
            {
                float val = residue[i];
                foreach (var elem in allImfs)
                    val += elem[i];
                values.Add(val);
            }
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<float>(values),
                    Title = "IMF"
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Dispose();
        }
    }
}
