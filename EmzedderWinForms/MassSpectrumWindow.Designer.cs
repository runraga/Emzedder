namespace EmzedderWinForms
{
    partial class MassSpectrumWindow
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
            massSpecPlot = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // massSpecPlot
            // 
            massSpecPlot.DisplayScale = 1F;
            massSpecPlot.Location = new Point(12, 12);
            massSpecPlot.Name = "massSpecPlot";
            massSpecPlot.Size = new Size(776, 426);
            massSpecPlot.TabIndex = 0;
            // 
            // MassSpectrumWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(massSpecPlot);
            Name = "MassSpectrumWindow";
            Text = "MassSpectrumWindow";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot massSpecPlot;
    }
}