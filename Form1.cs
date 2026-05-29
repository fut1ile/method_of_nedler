using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NelderMeadApp
{
    public partial class Form1 : Form
    {
        private List<TextBox> startPointTextBoxes = new List<TextBox>();

        private List<(string Name, string Function, int Variables, string[] StartPoint, string Alpha, string Beta, string Gamma, string Delta)> presets = new List<(string, string, int, string[], string, string, string, string)>
        {
            ("Сфера (простая)", "x1^2 + x2^2", 2, new[] { "5", "5" }, "1", "0.5", "2", "0.5"),
            ("Смещённая сфера", "(x1 - 2)^2 + (x2 + 3)^2", 2, new[] { "0", "0" }, "1", "0.5", "2", "0.5"),
            ("Функция Розенброка", "100*(x2 - x1^2)^2 + (1 - x1)^2", 2, new[] { "-1.2", "1" }, "1", "0.5", "2", "0.5"),
            ("Функция Химмельблау", "(x1^2 + x2 - 11)^2 + (x1 + x2^2 - 7)^2", 2, new[] { "0", "0" }, "1", "0.5", "2", "0.5"),
            ("Функция Бута", "(x1 + 2*x2 - 7)^2 + (2*x1 + x2 - 5)^2", 2, new[] { "0", "0" }, "1", "0.5", "2", "0.5"),
            ("Овражная функция", "x1^2 + 100*x2^2", 2, new[] { "5", "1" }, "1", "0.5", "2", "0.5"),
            ("Функция Экли", "-20 * exp(-0.2 * sqrt(0.5*(x1^2 + x2^2))) - exp(0.5*(cos(2*3.14159*x1) + cos(2*3.14159*x2))) + 20 + exp(1)", 2, new[] { "1", "1" }, "1", "0.5", "2", "0.5"),
            ("3D сфера", "x^2 + y^2 + z^2", 3, new[] { "3", "-2", "5" }, "1", "0.5", "2", "0.5"),
            ("4D сфера", "x1^2 + x2^2 + x3^2 + x4^2", 4, new[] { "1", "2", "3", "4" }, "1", "0.5", "2", "0.5"),
        };

        public Form1()
        {
            InitializeComponent();

            tbVariables.TextChanged += (s, e) => UpdateStartPointFields();
            cmbPresets.SelectedIndexChanged += CmbPresets_SelectedIndexChanged;
            btnRun.Click += BtnRun_Click;

            dgvIterations.Columns.Add("Iteration", "№");
            dgvIterations.Columns.Add("Point", "Точка");
            dgvIterations.Columns.Add("Value", "Значение F");

            foreach (var preset in presets)
                cmbPresets.Items.Add(preset.Name);
            UpdateStartPointFields();
        }

        private void CmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPresets.SelectedIndex < 0) return;

            var preset = presets[cmbPresets.SelectedIndex];

            tbFunction.Text = preset.Function;
            tbVariables.Text = preset.Variables.ToString();
            tbAlpha.Text = preset.Alpha;
            tbBeta.Text = preset.Beta;
            tbGamma.Text = preset.Gamma;
            tbDelta.Text = preset.Delta;

            tbTolerance.Text = "0.000001";
            tbMaxIter.Text = "1000";

            UpdateStartPointFields();
            for (int i = 0; i < preset.StartPoint.Length && i < startPointTextBoxes.Count; i++)
                startPointTextBoxes[i].Text = preset.StartPoint[i];
        }

        private void UpdateStartPointFields()
        {
            if (!int.TryParse(tbVariables.Text, out int dim) || dim < 1 || dim > 10)
                dim = 2;

            pnlStartPoint.Controls.Clear();
            startPointTextBoxes.Clear();

            for (int i = 0; i < dim; i++)
            {
                Label lbl = new Label
                {
                    Text = $"x{i + 1}:",
                    Location = new Point(10, 25 * i + 5),
                    AutoSize = true
                };
                TextBox tb = new TextBox
                {
                    Location = new Point(50, 25 * i + 2),
                    Width = 70,
                    Text = "0"
                };
                pnlStartPoint.Controls.Add(lbl);
                pnlStartPoint.Controls.Add(tb);
                startPointTextBoxes.Add(tb);
            }
        }

        private double ParseDouble(string text, string fieldName)
        {
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                throw new Exception($"Некорректное значение в поле \"{fieldName}\"");
            return value;
        }

        private int ParseInt(string text, string fieldName)
        {
            if (!int.TryParse(text, out int value))
                throw new Exception($"Некорректное значение в поле \"{fieldName}\"");
            return value;
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            try
            {
                int dim = ParseInt(tbVariables.Text, "Переменных");
                double[] x0 = new double[dim];

                for (int i = 0; i < dim; i++)
                    x0[i] = ParseDouble(startPointTextBoxes[i].Text, $"x{i + 1}");

                double alpha = ParseDouble(tbAlpha.Text, "α");
                double beta = ParseDouble(tbBeta.Text, "β");
                double gamma = ParseDouble(tbGamma.Text, "γ");
                double delta = ParseDouble(tbDelta.Text, "δ");
                double tol = ParseDouble(tbTolerance.Text, "Точность");
                int maxIter = ParseInt(tbMaxIter.Text, "Макс. итераций");

                string expr = tbFunction.Text.Trim();
                if (string.IsNullOrEmpty(expr))
                    throw new Exception("Введите функцию");

                Func<double[], double> func = ExpressionParser.Compile(expr, dim);

                var optimizationResult = NelderMeadSimplex.Run(
                    func, x0, maxIter, tol, alpha, beta, gamma, delta);

                lblResult.Text = $"Минимум: F({string.Join(", ", optimizationResult.BestPoint.Select(v => v.ToString("F6")))}) = {optimizationResult.BestValue:F10}";

                dgvIterations.Rows.Clear();
                for (int i = 0; i < optimizationResult.History.Count; i++)
                {
                    var item = optimizationResult.History[i];
                    double[] pt = item.Item1;
                    double val = item.Item2;
                    string pointStr = string.Join("; ", pt.Select(v => v.ToString("F4")));
                    dgvIterations.Rows.Add(i, pointStr, val.ToString("F8"));
                }

                if (dim == 2)
                {
                    pbPlot.Image?.Dispose();
                    DrawPlot2D(optimizationResult.History);
                }
                else
                {
                    pbPlot.Image?.Dispose();
                    Bitmap bmp = new Bitmap(pbPlot.Width, pbPlot.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        g.DrawString("График доступен только для 2D",
                            new Font("Arial", 12), Brushes.Gray, 10, 10);
                    }
                    pbPlot.Image = bmp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawPlot2D(List<(double[] point, double value)> history)
        {
            if (history.Count < 2)
            {
                Bitmap emptyBmp = new Bitmap(pbPlot.Width, pbPlot.Height);
                using (Graphics g = Graphics.FromImage(emptyBmp))
                {
                    g.Clear(Color.White);
                    g.DrawString("Недостаточно данных для графика",
                        new Font("Arial", 12), Brushes.Gray, 10, 10);
                }
                pbPlot.Image?.Dispose();
                pbPlot.Image = emptyBmp;
                return;
            }

            Bitmap bmp = new Bitmap(pbPlot.Width, pbPlot.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                double minX = history.Min(p => p.point[0]);
                double maxX = history.Max(p => p.point[0]);
                double minY = history.Min(p => p.point[1]);
                double maxY = history.Max(p => p.point[1]);

                double rangeX = maxX - minX;
                double rangeY = maxY - minY;
                if (rangeX < 0.001) rangeX = 1;
                if (rangeY < 0.001) rangeY = 1;

                double marginX = rangeX * 0.2;
                double marginY = rangeY * 0.2;
                minX -= marginX;
                maxX += marginX;
                minY -= marginY;
                maxY += marginY;

                int paddingLeft = 70;
                int paddingRight = 160;
                int paddingTop = 30;
                int paddingBottom = 50;
                int plotWidth = bmp.Width - paddingLeft - paddingRight;
                int plotHeight = bmp.Height - paddingTop - paddingBottom;

                int ToScreenX(double x) => paddingLeft + (int)((x - minX) / (maxX - minX) * plotWidth);
                int ToScreenY(double y) => paddingTop + plotHeight - (int)((y - minY) / (maxY - minY) * plotHeight);

                double niceStep(double range)
                {
                    double step = Math.Pow(10, Math.Floor(Math.Log10(range)));
                    if (range / step < 2) step /= 5;
                    else if (range / step < 5) step /= 2;
                    return step;
                }

                double stepX = niceStep(maxX - minX);
                double stepY = niceStep(maxY - minY);

                using (Pen gridPen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    double startX = Math.Ceiling(minX / stepX) * stepX;
                    for (double x = startX; x <= maxX; x += stepX)
                    {
                        int sx = ToScreenX(x);
                        g.DrawLine(gridPen, sx, paddingTop, sx, paddingTop + plotHeight);
                    }

                    double startY = Math.Ceiling(minY / stepY) * stepY;
                    for (double y = startY; y <= maxY; y += stepY)
                    {
                        int sy = ToScreenY(y);
                        g.DrawLine(gridPen, paddingLeft, sy, paddingLeft + plotWidth, sy);
                    }
                }

                using (Pen axisPen = new Pen(Color.Black, 2))
                {
                    int yAxisY = Math.Max(paddingTop, Math.Min(paddingTop + plotHeight, ToScreenY(0)));
                    g.DrawLine(axisPen, paddingLeft, yAxisY, paddingLeft + plotWidth, yAxisY);

                    int xAxisX = Math.Max(paddingLeft, Math.Min(paddingLeft + plotWidth, ToScreenX(0)));
                    g.DrawLine(axisPen, xAxisX, paddingTop, xAxisX, paddingTop + plotHeight);
                }

                using (Font tickFont = new Font("Arial", 9))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;

                    double startX = Math.Ceiling(minX / stepX) * stepX;
                    for (double x = startX; x <= maxX; x += stepX)
                    {
                        int sx = ToScreenX(x);
                        int sy = Math.Max(paddingTop, Math.Min(paddingTop + plotHeight, ToScreenY(0)));
                        g.DrawString(x.ToString("0.##"), tickFont, Brushes.Black, sx, sy + 8);
                    }

                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Center;

                    double startY = Math.Ceiling(minY / stepY) * stepY;
                    for (double y = startY; y <= maxY; y += stepY)
                    {
                        int sx = Math.Max(paddingLeft, Math.Min(paddingLeft + plotWidth, ToScreenX(0)));
                        int sy = ToScreenY(y);
                        g.DrawString(y.ToString("0.##"), tickFont, Brushes.Black, sx - 8, sy);
                    }
                }

                using (Font axisLabelFont = new Font("Arial", 11, FontStyle.Bold))
                {
                    g.DrawString("x₁", axisLabelFont, Brushes.Black, paddingLeft + plotWidth + 10, ToScreenY(0) - 20);
                    g.DrawString("x₂", axisLabelFont, Brushes.Black, ToScreenX(0) - 25, paddingTop - 25);
                }

                using (Pen pen = new Pen(Color.Blue, 2))
                {
                    for (int i = 0; i < history.Count - 1; i++)
                    {
                        Point p1 = new Point(ToScreenX(history[i].point[0]), ToScreenY(history[i].point[1]));
                        Point p2 = new Point(ToScreenX(history[i + 1].point[0]), ToScreenY(history[i + 1].point[1]));
                        g.DrawLine(pen, p1, p2);
                    }
                }

                for (int i = 0; i < history.Count; i++)
                {
                    int x = ToScreenX(history[i].point[0]);
                    int y = ToScreenY(history[i].point[1]);

                    if (i == 0)
                    {
                        g.FillEllipse(Brushes.Red, x - 5, y - 5, 10, 10);
                        g.DrawEllipse(Pens.DarkRed, x - 5, y - 5, 10, 10);
                    }
                    else if (i == history.Count - 1)
                    {
                        g.FillEllipse(Brushes.Green, x - 6, y - 6, 12, 12);
                        g.DrawEllipse(Pens.DarkGreen, x - 6, y - 6, 12, 12);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Blue, x - 2, y - 2, 4, 4);
                    }
                }

                int legendX = bmp.Width - 150;
                int legendY = paddingTop + 10;

                g.FillRectangle(Brushes.WhiteSmoke, legendX - 5, legendY - 5, 145, 140);
                g.DrawRectangle(Pens.Gray, legendX - 5, legendY - 5, 145, 140);

                g.DrawString("ЛЕГЕНДА", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, legendX, legendY);

                g.FillEllipse(Brushes.Red, legendX, legendY + 25, 10, 10);
                g.DrawEllipse(Pens.DarkRed, legendX, legendY + 25, 10, 10);
                g.DrawString($"Старт: ({history[0].point[0]:F3}, {history[0].point[1]:F3})",
                    new Font("Arial", 8), Brushes.Black, legendX + 15, legendY + 23);

                g.FillEllipse(Brushes.Green, legendX, legendY + 48, 12, 12);
                g.DrawEllipse(Pens.DarkGreen, legendX, legendY + 48, 12, 12);
                var last = history.Last();
                g.DrawString($"Финиш: ({last.point[0]:F3}, {last.point[1]:F3})",
                    new Font("Arial", 8), Brushes.Black, legendX + 15, legendY + 46);

                g.DrawLine(new Pen(Color.Blue, 2), legendX, legendY + 75, legendX + 15, legendY + 75);
                g.DrawString($"Итераций: {history.Count}",
                    new Font("Arial", 8), Brushes.Black, legendX + 18, legendY + 69);

                g.DrawString($"F(x₁, x₂) = {last.value:F8}",
                    new Font("Arial", 8, FontStyle.Bold), Brushes.Black, legendX, legendY + 95);
            }

            pbPlot.Image?.Dispose();
            pbPlot.Image = bmp;
        }
    }
}
