using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

namespace ExtractPdfText
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      PDDocument doc = PDDocument.load(textBox1.Text);
      PDFTextStripper pdfStripper = new PDFTextStripper();
      textBox2.Text = pdfStripper.getText(doc);
    }
  }
}
