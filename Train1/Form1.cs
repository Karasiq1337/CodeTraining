using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Train1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void EbashButton_Click(object sender, EventArgs e)
        {
            int[] input = ArrayManipulator.ParseArray(textBox1.Text);
            int[] output = ArrayManipulator.Manipulate(input);
            label3.Text = String.Join(" ", output);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] input = ArrayManipulator.ParseArray(textBox1.Text);
            int[] output = ArrayManipulator.DeletePrime(input);
            label3.Text = String.Join(" ", output);
        }
    }
}
