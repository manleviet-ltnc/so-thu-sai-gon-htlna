using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace So_Thu_Sai_Gon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            lstDanhsach.Items.Add(lstThuMoi.SelectedItem);
        }
        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.X, e.Y);
            if (index != -1)

                lb.DoDragDrop(lb.Items[index].ToString(),
                               DragDropEffects.Copy);

        }
        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void lstDanhsach_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                bool test = false;
                for (int i = 0; i < lstDanhsach.Items.Count; i++)
                {
                    string st = lstDanhsach.Items[i].ToString();
                    string data = e.Data.GetData(DataFormats.Text).ToString();
                    if (data == st)
                        test = true;
                }
                if (test == false)
                {
                    int newIndex = lstDanhsach.IndexFromPoint(lstDanhsach.PointToClient(new Point(e.X, e.Y)));
                    lstDanhsach.Items.Remove(e.Data.GetData(DataFormats.Text));
                    if (newIndex != -1)
                        lstDanhsach.Items.Insert(newIndex, e.Data.GetData(DataFormats.Text));
                    else
                    {
                        ListBox lb = (ListBox)sender;
                        lb.Items.Add(e.Data.GetData(DataFormats.Text));
                    }

                }
            }
        }
        bool issave = false;
        private void Save(object sender, EventArgs e)
        { 
            // Mo tap tin
            StreamWriter write = new StreamWriter("danhsachthu.txt");
            if (write == null) return;
            foreach (var item in lstDanhsach.Items)
                write.WriteLine(item.ToString());
            write.Close();
            issave = true;
        }
        private void mnuClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("thumoi.txt");

            if (reader == null) return;

            string input = null;
            while ((input = reader.ReadLine()) != null)
            {
                lstThuMoi.Items.Add(input);
            }
            reader.Close();

            using (StreamReader rs = new StreamReader("danhsachthu.txt"))
            {
                input = null;
                while ((input = rs.ReadLine()) != null)
                {
                    lstDanhsach.Items.Add(input);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = string.Format("Bây giờ là {0}:{1}:{2} ngày {3} tháng {4} năm {5}",
                                          DateTime.Now.Hour,
                                          DateTime.Now.Minute,
                                          DateTime.Now.Second,
                                          DateTime.Now.Day,
                                          DateTime.Now.Month,
                                          DateTime.Now.Year);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            lstDanhsach.Items.Remove(lstDanhsach.SelectedItem);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (issave == false)
            {
                DialogResult kq = MessageBox.Show("Ban co muon luu danh sach?", "THONG BAO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    Save(sender, e);
                    e.Cancel = false;
                }
                else if (kq == DialogResult.No)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
            else
                mnuClose_Click(sender, e);

        }

       
    }
}









        
