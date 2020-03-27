using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thompson_Cerradura
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void NuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String title = "Nueva Pestaña";
            TabPage nueva = new TabPage(title);
            int ancho = tabControl1.Size.Width;
            int alto = tabControl1.Size.Height;
            Size tam = new Size(ancho, alto);
            nueva.Size= tam;
            RichTextBox textBox = new RichTextBox();
            textBox.Name = "textBox";
            textBox.Size = nueva.Size;
            nueva.Controls.Add(textBox);
            tabControl1.TabPages.Add(nueva);
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == null)
            {
                String title = "Nueva Pestaña";
                TabPage nueva = new TabPage(title);
                nueva.Size = tabControl1.Size;
                RichTextBox textBox = new RichTextBox();
                textBox.Name = "textBox";
                textBox.Size = nueva.ClientSize;
                nueva.Controls.Add(textBox);
                tabControl1.TabPages.Add(nueva);
            }
            if (tabControl1.SelectedTab != null)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Archivos (*.er)|*.er";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    RichTextBox richTextBox1 = (RichTextBox)tabControl1.SelectedTab.Controls.Find("textBox", true)[0];
                    if (richTextBox1 != null)
                    {
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        tabControl1.SelectedTab.Text = openFileDialog1.FileName;
                    }

                }
            }



        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();

            saveFile1.DefaultExt = "*.er";
            saveFile1.Filter = "Archivos (*.er)|*.er";


            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                RichTextBox richTextBox1 = (RichTextBox)tabControl1.SelectedTab.Controls.Find("textBox", true)[0];
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void CargarThompsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Lexico.getInstancia().LimpiarLista();
            RichTextBox richTextBox1 = (RichTextBox)tabControl1.SelectedTab.Controls.Find("textBox", true)[0];
            LinkedList<Token> listaTokens = new LinkedList<Token>();
            LinkedList<Token> listaErrores = new LinkedList<Token>();
            listaTokens = Lexico.getInstancia().GetTokens(richTextBox1.Text);
            listaErrores = Lexico.getInstancia().GetErrores();
            
            foreach(Token t in listaTokens)
            {
                Console.WriteLine("ID: " + t.IdToken);
                Console.WriteLine("Nombre: " + t.NombreToken);
                Console.WriteLine("Valor: " + t.Valor);
                Console.WriteLine("Fila: " + t.Fila);
                Console.WriteLine("Columna: " + t.Columna);

            }
            foreach (Token t in listaErrores)
            {
                Console.WriteLine("ID: " + t.IdToken);
                Console.WriteLine("Nombre: " + t.NombreToken);
                Console.WriteLine("Valor: " + t.Valor);
                Console.WriteLine("Fila: " + t.Fila);
                Console.WriteLine("Columna: " + t.Columna);

            }

            Thompson.getInstancia().generarGrafos();
            flowLayoutPanel1.Controls.Clear();
            PictureBox imagen = new PictureBox();
            imagen.SizeMode = PictureBoxSizeMode.AutoSize;
            imagen.ImageLocation = @"imagen.png";
            flowLayoutPanel1.Controls.Add(imagen);
            Thompson.getInstancia().generarExpresiones(listaTokens);
        }
    }
}
