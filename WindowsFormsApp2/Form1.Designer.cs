namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.button1 = new System.Windows.Forms.Button();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart3 = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 12);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(776, 193);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clean";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.Location = new System.Drawing.Point(12, 240);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(776, 198);
            this.cartesianChart2.TabIndex = 2;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // cartesianChart3
            // 
            this.cartesianChart3.Location = new System.Drawing.Point(12, 457);
            this.cartesianChart3.Name = "cartesianChart3";
            this.cartesianChart3.Size = new System.Drawing.Size(773, 225);
            this.cartesianChart3.TabIndex = 3;
            this.cartesianChart3.Text = "cartesianChart3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 706);
            this.Controls.Add(this.cartesianChart3);
            this.Controls.Add(this.cartesianChart2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cartesianChart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Button button1;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private LiveCharts.WinForms.CartesianChart cartesianChart3;
    }
}

