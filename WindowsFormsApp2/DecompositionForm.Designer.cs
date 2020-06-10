namespace WindowsFormsApp2
{
    partial class DecompositionForm
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
            this.cartesianChart4 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart3 = new LiveCharts.WinForms.CartesianChart();
            this.button1 = new System.Windows.Forms.Button();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // cartesianChart4
            // 
            this.cartesianChart4.Location = new System.Drawing.Point(9, 261);
            this.cartesianChart4.Name = "cartesianChart4";
            this.cartesianChart4.Size = new System.Drawing.Size(776, 228);
            this.cartesianChart4.TabIndex = 5;
            this.cartesianChart4.Text = "cartesianChart4";
            // 
            // cartesianChart3
            // 
            this.cartesianChart3.Location = new System.Drawing.Point(12, 486);
            this.cartesianChart3.Name = "cartesianChart3";
            this.cartesianChart3.Size = new System.Drawing.Size(773, 225);
            this.cartesianChart3.TabIndex = 6;
            this.cartesianChart3.Text = "cartesianChart3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(859, 688);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 21);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(776, 193);
            this.cartesianChart1.TabIndex = 8;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // DecompositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 723);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cartesianChart3);
            this.Controls.Add(this.cartesianChart4);
            this.Name = "DecompositionForm";
            this.Text = "DecompositionForm";
            this.Load += new System.EventHandler(this.DecompositionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart4;
        private LiveCharts.WinForms.CartesianChart cartesianChart3;
        private System.Windows.Forms.Button button1;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
    }
}