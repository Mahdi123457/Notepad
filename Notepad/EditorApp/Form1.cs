using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorApp
{
    public partial class Form1 : Form
    {
        string FilePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
                ((TextBox)sender).BackColor = Color.Red;
            else if (sender is ListBox)
                ((ListBox)sender).BackColor = Color.Red;
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
                ((TextBox)sender).BackColor = Color.White;
            else if (sender is ListBox)
                ((ListBox)sender).BackColor = Color.White;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
            FilePath = "";
        }

        private void BtnBlue_Click(object sender, EventArgs e)
        {
            txtEditor.ForeColor = Color.Blue;
        }

        private void BtnRed_Click(object sender, EventArgs e)
        {
            txtEditor.ForeColor = Color.FromArgb(255, 0, 0);
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                txtEditor.SelectionColor = colorDialog1.Color;
        }

        private void BtnFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                txtEditor.SelectionFont = fontDialog1.Font;
                txtEditor.SelectionColor = fontDialog1.Color;
            }
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            txtEditor.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            txtEditor.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            txtEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Save(saveFileDialog1.FileName);
                FilePath = saveFileDialog1.FileName;
            }
        }

        private void Save(string fileName)
        {
            System.IO.File.WriteAllText(fileName, txtEditor.Rtf);
            txtEditor.Modified = false;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtEditor.Rtf = System.IO.File.ReadAllText(openFileDialog1.FileName);
                FilePath = openFileDialog1.FileName;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(FilePath))
                Save(FilePath);
            else
                saveAsToolStripMenuItem.PerformClick();
        }

        private void UndoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtEditor.Undo();
        }

        private void CopyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtEditor.Copy();
        }

        private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtEditor.Cut();
        }

        private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtEditor.Paste();
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            /*if (txtEditor.CanUndo)
                undoToolStripMenuItem1.Enabled = true;
            else
                undoToolStripMenuItem1.Enabled = false;*/
            undoToolStripMenuItem1.Enabled = txtEditor.CanUndo;

            /*if (txtEditor.SelectionLength == 0)
                copyToolStripMenuItem1.Enabled = false;
            else
                copyToolStripMenuItem1.Enabled = true;*/
            copyToolStripMenuItem1.Enabled = cutToolStripMenuItem1.Enabled = txtEditor.SelectionLength > 0;

            pasteToolStripMenuItem1.Enabled = Clipboard.ContainsText();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frm = new AboutBox1();
            frm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtEditor.Modified)
            {
                DialogResult dr = MessageBox.Show("Save Changes?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dr)
                {
                    case DialogResult.Cancel: e.Cancel = true; break;
                    case DialogResult.Yes: saveToolStripMenuItem.PerformClick(); break;
                }
            }
        }
    }
}
