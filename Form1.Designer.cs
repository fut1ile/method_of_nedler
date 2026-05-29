namespace NelderMeadApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbFunction = new System.Windows.Forms.TextBox();
            this.pnlStartPoint = new System.Windows.Forms.Panel();
            this.tbVariables = new System.Windows.Forms.TextBox();
            this.tbAlpha = new System.Windows.Forms.TextBox();
            this.tbBeta = new System.Windows.Forms.TextBox();
            this.tbDelta = new System.Windows.Forms.TextBox();
            this.tbGamma = new System.Windows.Forms.TextBox();
            this.tbMaxIter = new System.Windows.Forms.TextBox();
            this.tbTolerance = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.dgvIterations = new System.Windows.Forms.DataGridView();
            this.lblResult = new System.Windows.Forms.Label();
            this.pbPlot = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPresets = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlot)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFunction
            // 
            this.tbFunction.Location = new System.Drawing.Point(117, 12);
            this.tbFunction.Name = "tbFunction";
            this.tbFunction.Size = new System.Drawing.Size(200, 20);
            this.tbFunction.TabIndex = 0;
            // 
            // pnlStartPoint
            // 
            this.pnlStartPoint.Location = new System.Drawing.Point(117, 67);
            this.pnlStartPoint.Name = "pnlStartPoint";
            this.pnlStartPoint.Size = new System.Drawing.Size(200, 100);
            this.pnlStartPoint.TabIndex = 2;
            // 
            // tbVariables
            // 
            this.tbVariables.Location = new System.Drawing.Point(117, 38);
            this.tbVariables.Name = "tbVariables";
            this.tbVariables.Size = new System.Drawing.Size(31, 20);
            this.tbVariables.TabIndex = 3;
            // 
            // tbAlpha
            // 
            this.tbAlpha.Location = new System.Drawing.Point(117, 173);
            this.tbAlpha.Name = "tbAlpha";
            this.tbAlpha.Size = new System.Drawing.Size(70, 20);
            this.tbAlpha.TabIndex = 4;
            // 
            // tbBeta
            // 
            this.tbBeta.Location = new System.Drawing.Point(117, 201);
            this.tbBeta.Name = "tbBeta";
            this.tbBeta.Size = new System.Drawing.Size(70, 20);
            this.tbBeta.TabIndex = 5;
            // 
            // tbDelta
            // 
            this.tbDelta.Location = new System.Drawing.Point(117, 253);
            this.tbDelta.Name = "tbDelta";
            this.tbDelta.Size = new System.Drawing.Size(70, 20);
            this.tbDelta.TabIndex = 7;
            // 
            // tbGamma
            // 
            this.tbGamma.Location = new System.Drawing.Point(117, 227);
            this.tbGamma.Name = "tbGamma";
            this.tbGamma.Size = new System.Drawing.Size(70, 20);
            this.tbGamma.TabIndex = 6;
            // 
            // tbMaxIter
            // 
            this.tbMaxIter.Location = new System.Drawing.Point(117, 305);
            this.tbMaxIter.Name = "tbMaxIter";
            this.tbMaxIter.Size = new System.Drawing.Size(70, 20);
            this.tbMaxIter.TabIndex = 9;
            // 
            // tbTolerance
            // 
            this.tbTolerance.Location = new System.Drawing.Point(117, 279);
            this.tbTolerance.Name = "tbTolerance";
            this.tbTolerance.Size = new System.Drawing.Size(70, 20);
            this.tbTolerance.TabIndex = 8;
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(382, 11);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(128, 47);
            this.btnRun.TabIndex = 10;
            this.btnRun.Text = "Старт";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // dgvIterations
            // 
            this.dgvIterations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIterations.Location = new System.Drawing.Point(5, 347);
            this.dgvIterations.Name = "dgvIterations";
            this.dgvIterations.Size = new System.Drawing.Size(366, 150);
            this.dgvIterations.TabIndex = 11;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(678, 45);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(123, 13);
            this.lblResult.TabIndex = 12;
            this.lblResult.Text = "Результат будет здесь";
            // 
            // pbPlot
            // 
            this.pbPlot.Location = new System.Drawing.Point(395, 81);
            this.pbPlot.Name = "pbPlot";
            this.pbPlot.Size = new System.Drawing.Size(671, 493);
            this.pbPlot.TabIndex = 13;
            this.pbPlot.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Функция:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Кол-во переменных:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Начальная точка:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "α (отражение):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "β (сжатие):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "γ (растяжение):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "δ (усадка):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Точность:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 305);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Макс. итераций:";
            // 
            // cmbPresets
            // 
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Location = new System.Drawing.Point(835, 7);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(231, 21);
            this.cmbPresets.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(783, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Задача:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 577);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbPresets);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbPlot);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.dgvIterations);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.tbMaxIter);
            this.Controls.Add(this.tbTolerance);
            this.Controls.Add(this.tbDelta);
            this.Controls.Add(this.tbGamma);
            this.Controls.Add(this.tbBeta);
            this.Controls.Add(this.tbAlpha);
            this.Controls.Add(this.tbVariables);
            this.Controls.Add(this.pnlStartPoint);
            this.Controls.Add(this.tbFunction);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFunction;
        private System.Windows.Forms.Panel pnlStartPoint;
        private System.Windows.Forms.TextBox tbVariables;
        private System.Windows.Forms.TextBox tbAlpha;
        private System.Windows.Forms.TextBox tbBeta;
        private System.Windows.Forms.TextBox tbDelta;
        private System.Windows.Forms.TextBox tbGamma;
        private System.Windows.Forms.TextBox tbMaxIter;
        private System.Windows.Forms.TextBox tbTolerance;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.DataGridView dgvIterations;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.PictureBox pbPlot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPresets;
        private System.Windows.Forms.Label label10;
    }
}

