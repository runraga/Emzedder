namespace EmzedderWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog1 = new OpenFileDialog();
            flowLayoutPanel1 = new FlowLayoutPanel();
            chooseDatafileButton = new Button();
            datafilePathLabel = new Label();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Thermo Raw Files (*.raw)|*.raw|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = "C:\\Users\\runra\\source\\repos\\Emzedder\\Emzedder\\TestRawDatafiles";
            openFileDialog1.Title = "Select a picture file";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(chooseDatafileButton);
            flowLayoutPanel1.Controls.Add(datafilePathLabel);
            flowLayoutPanel1.Location = new Point(10, 9);
            flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(745, 29);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // chooseDatafileButton
            // 
            chooseDatafileButton.Location = new Point(3, 2);
            chooseDatafileButton.Margin = new Padding(3, 2, 3, 2);
            chooseDatafileButton.Name = "chooseDatafileButton";
            chooseDatafileButton.Size = new Size(119, 22);
            chooseDatafileButton.TabIndex = 0;
            chooseDatafileButton.Text = "Open Data file";
            chooseDatafileButton.UseVisualStyleBackColor = true;
            chooseDatafileButton.Click += ChooseDatafileButton_Click;
            // 
            // datafilePathLabel
            // 
            datafilePathLabel.Location = new Point(128, 2);
            datafilePathLabel.Margin = new Padding(3, 2, 3, 2);
            datafilePathLabel.Name = "datafilePathLabel";
            datafilePathLabel.Size = new Size(146, 22);
            datafilePathLabel.TabIndex = 1;
            datafilePathLabel.Text = "Open a datafile...";
            datafilePathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1.25F;
            formsPlot1.Location = new Point(10, 43);
            formsPlot1.Margin = new Padding(3, 2, 3, 2);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(745, 252);
            formsPlot1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(766, 297);
            Controls.Add(formsPlot1);
            Controls.Add(flowLayoutPanel1);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "eMZedder";
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button chooseDatafileButton;
        private Label datafilePathLabel;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}
