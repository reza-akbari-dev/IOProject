using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IOProject
{
    public partial class Form1 : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Placeholders
            txtPath.Text = "Enter Path";
            txtPath.ForeColor = Color.Gray;

            txtWordToCount.Text = "Enter Word";
            txtWordToCount.ForeColor = Color.Gray;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                log.Info("Reading file...");
                if (txtPath.Text.StartsWith("Enter"))
                    txtPath.Text = "";
                if (txtWordToCount.Text.StartsWith("Enter"))
                    txtWordToCount.Text = "";

                string aPath = txtPath.Text.Trim();
                string content = File.ReadAllText(aPath);
                rtbText.Text = content;
            }
            catch (Exception ex)
            {
                log.Error("Error while reading file: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        // Method to count a given word
        private int CountWord(string text, string word)
        {
            int count = 0;
            int i = text.IndexOf(word, StringComparison.OrdinalIgnoreCase); // Case-insensitive search
            while (i != -1)
            {
                count++;
                i = text.IndexOf(word, i + word.Length, StringComparison.OrdinalIgnoreCase);
            }
            return count;
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            try
            {
                string word = txtWordToCount.Text.Trim();
                string text = rtbText.Text;

                int countWord = CountWord(text, word);
                lblCount.Text = $"The word appeared: {countWord} times";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                string aPath = txtPath.Text.Trim();
                using (TextWriter txt = new StreamWriter(aPath))
                {
                    txt.Write(rtbText.Text);
                }
                lblCount.Text = "File was Updated";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
