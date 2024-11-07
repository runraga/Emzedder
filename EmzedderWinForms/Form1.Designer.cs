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
            components = new System.ComponentModel.Container();
            openFileDialog1 = new OpenFileDialog();
            colorDialog1 = new ColorDialog();
            timeLabel = new Label();
            label1 = new Label();
            addLeftLabel = new Label();
            addLabel = new Label();
            addRightLabel = new Label();
            label5 = new Label();
            sum = new NumericUpDown();
            difference = new NumericUpDown();
            label2 = new Label();
            minusRightLabel = new Label();
            label4 = new Label();
            minusLeftLabel = new Label();
            product = new NumericUpDown();
            label3 = new Label();
            multiplyRightLabel = new Label();
            label7 = new Label();
            multiplyLeftLabel = new Label();
            quotient = new NumericUpDown();
            label9 = new Label();
            divideRightLabel = new Label();
            label11 = new Label();
            divideLeftLabel = new Label();
            startButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)sum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)difference).BeginInit();
            ((System.ComponentModel.ISupportInitialize)product).BeginInit();
            ((System.ComponentModel.ISupportInitialize)quotient).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog1.Title = "Select a picture file";
            // 
            // timeLabel
            // 
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Font = new Font("Segoe UI", 15.75F);
            timeLabel.Location = new Point(21, 126);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(246, 37);
            timeLabel.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F);
            label1.Location = new Point(12, 75);
            label1.Name = "label1";
            label1.Size = new Size(126, 37);
            label1.TabIndex = 4;
            label1.Text = "Time left:";
            // 
            // addLeftLabel
            // 
            addLeftLabel.AutoSize = true;
            addLeftLabel.Font = new Font("Segoe UI", 15.75F);
            addLeftLabel.Location = new Point(297, 13);
            addLeftLabel.Name = "addLeftLabel";
            addLeftLabel.Size = new Size(29, 37);
            addLeftLabel.TabIndex = 5;
            addLeftLabel.Text = "?";
            // 
            // addLabel
            // 
            addLabel.AutoSize = true;
            addLabel.Font = new Font("Segoe UI", 15.75F);
            addLabel.Location = new Point(332, 13);
            addLabel.Name = "addLabel";
            addLabel.Size = new Size(35, 37);
            addLabel.TabIndex = 6;
            addLabel.Text = "+";
            addLabel.Click += label3_Click;
            // 
            // addRightLabel
            // 
            addRightLabel.AutoSize = true;
            addRightLabel.Font = new Font("Segoe UI", 15.75F);
            addRightLabel.Location = new Point(373, 13);
            addRightLabel.Name = "addRightLabel";
            addRightLabel.Size = new Size(29, 37);
            addRightLabel.TabIndex = 7;
            addRightLabel.Text = "?";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15.75F);
            label5.Location = new Point(408, 13);
            label5.Name = "label5";
            label5.Size = new Size(35, 37);
            label5.TabIndex = 8;
            label5.Text = "=";
            // 
            // sum
            // 
            sum.Location = new Point(448, 23);
            sum.MaximumSize = new Size(100, 0);
            sum.Name = "sum";
            sum.Size = new Size(50, 27);
            sum.TabIndex = 1;
            sum.ValueChanged += sum_ValueChanged;
            sum.Enter += answer_Enter;
            // 
            // difference
            // 
            difference.Location = new Point(448, 75);
            difference.MaximumSize = new Size(100, 0);
            difference.Name = "difference";
            difference.Size = new Size(50, 27);
            difference.TabIndex = 2;
            difference.Enter += answer_Enter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F);
            label2.Location = new Point(408, 65);
            label2.Name = "label2";
            label2.Size = new Size(35, 37);
            label2.TabIndex = 13;
            label2.Text = "=";
            // 
            // minusRightLabel
            // 
            minusRightLabel.AutoSize = true;
            minusRightLabel.Font = new Font("Segoe UI", 15.75F);
            minusRightLabel.Location = new Point(373, 65);
            minusRightLabel.Name = "minusRightLabel";
            minusRightLabel.Size = new Size(29, 37);
            minusRightLabel.TabIndex = 12;
            minusRightLabel.Text = "?";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F);
            label4.Location = new Point(332, 65);
            label4.Name = "label4";
            label4.Size = new Size(28, 37);
            label4.TabIndex = 11;
            label4.Text = "-";
            // 
            // minusLeftLabel
            // 
            minusLeftLabel.AutoSize = true;
            minusLeftLabel.Font = new Font("Segoe UI", 15.75F);
            minusLeftLabel.Location = new Point(297, 65);
            minusLeftLabel.Name = "minusLeftLabel";
            minusLeftLabel.Size = new Size(29, 37);
            minusLeftLabel.TabIndex = 10;
            minusLeftLabel.Text = "?";
            // 
            // product
            // 
            product.Location = new Point(448, 126);
            product.MaximumSize = new Size(100, 0);
            product.Name = "product";
            product.Size = new Size(50, 27);
            product.TabIndex = 3;
            product.Enter += answer_Enter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F);
            label3.Location = new Point(408, 116);
            label3.Name = "label3";
            label3.Size = new Size(35, 37);
            label3.TabIndex = 18;
            label3.Text = "=";
            // 
            // multiplyRightLabel
            // 
            multiplyRightLabel.AutoSize = true;
            multiplyRightLabel.Font = new Font("Segoe UI", 15.75F);
            multiplyRightLabel.Location = new Point(373, 116);
            multiplyRightLabel.Name = "multiplyRightLabel";
            multiplyRightLabel.Size = new Size(29, 37);
            multiplyRightLabel.TabIndex = 17;
            multiplyRightLabel.Text = "?";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 15.75F);
            label7.Location = new Point(332, 116);
            label7.Name = "label7";
            label7.Size = new Size(29, 37);
            label7.TabIndex = 16;
            label7.Text = "x";
            // 
            // multiplyLeftLabel
            // 
            multiplyLeftLabel.AutoSize = true;
            multiplyLeftLabel.Font = new Font("Segoe UI", 15.75F);
            multiplyLeftLabel.Location = new Point(297, 116);
            multiplyLeftLabel.Name = "multiplyLeftLabel";
            multiplyLeftLabel.Size = new Size(29, 37);
            multiplyLeftLabel.TabIndex = 15;
            multiplyLeftLabel.Text = "?";
            // 
            // quotient
            // 
            quotient.Location = new Point(448, 177);
            quotient.MaximumSize = new Size(100, 0);
            quotient.Name = "quotient";
            quotient.Size = new Size(50, 27);
            quotient.TabIndex = 4;
            quotient.Enter += answer_Enter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 15.75F);
            label9.Location = new Point(408, 167);
            label9.Name = "label9";
            label9.Size = new Size(35, 37);
            label9.TabIndex = 23;
            label9.Text = "=";
            // 
            // divideRightLabel
            // 
            divideRightLabel.AutoSize = true;
            divideRightLabel.Font = new Font("Segoe UI", 15.75F);
            divideRightLabel.Location = new Point(373, 167);
            divideRightLabel.Name = "divideRightLabel";
            divideRightLabel.Size = new Size(29, 37);
            divideRightLabel.TabIndex = 22;
            divideRightLabel.Text = "?";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 15.75F);
            label11.Location = new Point(332, 167);
            label11.Name = "label11";
            label11.Size = new Size(35, 37);
            label11.TabIndex = 21;
            label11.Text = "÷";
            // 
            // divideLeftLabel
            // 
            divideLeftLabel.AutoSize = true;
            divideLeftLabel.Font = new Font("Segoe UI", 15.75F);
            divideLeftLabel.Location = new Point(297, 167);
            divideLeftLabel.Name = "divideLeftLabel";
            divideLeftLabel.Size = new Size(29, 37);
            divideLeftLabel.TabIndex = 20;
            divideLeftLabel.Text = "?";
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 15F);
            startButton.Location = new Point(12, 12);
            startButton.Name = "startButton";
            startButton.Size = new Size(244, 52);
            startButton.TabIndex = 5;
            startButton.Text = "Start Quiz";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 227);
            Controls.Add(startButton);
            Controls.Add(quotient);
            Controls.Add(label9);
            Controls.Add(divideRightLabel);
            Controls.Add(label11);
            Controls.Add(divideLeftLabel);
            Controls.Add(product);
            Controls.Add(label3);
            Controls.Add(multiplyRightLabel);
            Controls.Add(label7);
            Controls.Add(multiplyLeftLabel);
            Controls.Add(difference);
            Controls.Add(label2);
            Controls.Add(minusRightLabel);
            Controls.Add(label4);
            Controls.Add(minusLeftLabel);
            Controls.Add(sum);
            Controls.Add(label5);
            Controls.Add(addRightLabel);
            Controls.Add(addLabel);
            Controls.Add(addLeftLabel);
            Controls.Add(label1);
            Controls.Add(timeLabel);
            Font = new Font("Segoe UI", 9F);
            Name = "Form1";
            Text = "eMZedder";
            ((System.ComponentModel.ISupportInitialize)sum).EndInit();
            ((System.ComponentModel.ISupportInitialize)difference).EndInit();
            ((System.ComponentModel.ISupportInitialize)product).EndInit();
            ((System.ComponentModel.ISupportInitialize)quotient).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private ColorDialog colorDialog1;
        private Label timeLabel;
        private Label label1;
        private Label addLeftLabel;
        private Label addLabel;
        private Label addRightLabel;
        private Label label5;
        private NumericUpDown sum;
        private NumericUpDown difference;
        private Label label2;
        private Label minusRightLabel;
        private Label label4;
        private Label minusLeftLabel;
        private NumericUpDown product;
        private Label label3;
        private Label multiplyRightLabel;
        private Label label7;
        private Label multiplyLeftLabel;
        private NumericUpDown quotient;
        private Label label9;
        private Label divideRightLabel;
        private Label label11;
        private Label divideLeftLabel;
        private Button startButton;
        private System.Windows.Forms.Timer timer1;
    }
}
