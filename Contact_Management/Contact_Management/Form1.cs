using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Contact_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
  public static  int rowindex;
        DataTable Table = new DataTable();
        string path = "D:\\Contacts\\Contacts.txt";
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Table.Columns.Add("Name", typeof(string));
            Table.Columns.Add("Gender", typeof(string));
            Table.Columns.Add("City", typeof(string));
            Table.Columns.Add("Email", typeof(string));
            Table.Columns.Add("Email 2", typeof(string));
            Table.Columns.Add("Contact_1", typeof(String));
             Table.Columns.Add("Contact_2", typeof(String));
            Table.Columns.Add("Address ",typeof(string));
     
            string[] str = File.ReadAllLines(path);
             string[] sep = { "," };
            for(int i=0;i<str.Length;i++)
            {
                if (str[i] != "")
                {
                    String[] s = str[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    Table.Rows.Add(s[0], s[1], s[3], s[4], s[5], s[6], s[7], s[2]);
                }
            }

                        
            dataGridView1.DataSource = Table;
               }
        public void rowcolour()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        private void Add_new_Button_Click(object sender, EventArgs e)
        {

            TextWriter writer=new StreamWriter(path ,true);
            string ctn =  Name_box.Text + "\t,\t"+ comboBox1.SelectedItem.ToString() + "\t,\t"+ address_Box.Text + "\t,\t"+ citybox.Text + "\t,\t"+ emailbox.Text + "\t,\t"+ emailbox2.Text + "\t,\t"+contactBox1.Text + "\t,\t"+ contactbox2.Text + "\t,\t"+address_Box.Text ;
            writer.Write(ctn+"\n");
            writer.Close();
            Table.Rows.Add(Name_box.Text, comboBox1.SelectedItem.ToString(), citybox.Text, emailbox.Text, emailbox2.Text, contactBox1.Text, contactbox2.Text, address_Box.Text);
           

        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            int Row_index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(Row_index);
            string[] str = File.ReadAllLines(path);
            string[] sep = { "," };
            for (int i = 0; i < str.Length; i++)
            {
                String[] s = str[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
             //   Table.Rows.Add(s[0], s[1], s[3], s[4], s[5], s[6], s[7], s[2]);
                if (Name_box.Text == s[0])
                {
                    str[i] = null;
                }
            }
            File.WriteAllLines(path, str);
            //File.Delete(path + Name_box.Text + ".txt");

        }

        private void Name_Search_Button_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(Table);
            dv.RowFilter = "Name like "+"'"+Name_box.Text+"%'";
            dataGridView1.DataSource = dv;
        }

        private void mailsearch_btn_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(Table);
            dv.RowFilter = "Email like " + "'" + emailbox.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void contact_search_button_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(Table);
            dv.RowFilter = "Contact_1 like " +"'" +contactBox1.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            Name_box.Text = "";
            citybox.Text = "";
            comboBox1.SelectedIndex = -1;
            emailbox.Text = "";
            contactBox1.Text = "";
            address_Box.Clear();
        }

        private void City_box2_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Table);
            dv.RowFilter = "city like '" + City_box2.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Table);
            dv.RowFilter = "gender like '" + comboBox2.SelectedItem.ToString() + "%'";
            dataGridView1.DataSource = dv;
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           rowindex=e.RowIndex;
           DataGridViewRow row = dataGridView1.Rows[rowindex];
           Name_box.Text = row.Cells[0].Value.ToString();

           citybox.Text = row.Cells[2].Value.ToString();
           emailbox.Text = row.Cells[3].Value.ToString();
           emailbox2.Text = row.Cells[4].Value.ToString();
               contactBox1.Text = row.Cells[5].Value.ToString();
               contactbox2.Text = row.Cells[6].Value.ToString();
               address_Box.Text = row.Cells[7].Value.ToString();
           comboBox1.SelectedItem = row.Cells[1].Value.ToString();
           StreamReader reader= new StreamReader(path );
           string str = reader.ReadToEnd();
           string[] sep = { "," };
           string[] ar = str.Split(sep, StringSplitOptions.RemoveEmptyEntries);

           reader.Close();
           //string[] temp = File.ReadAllLines(path + Name_box.Text+".txt");
          // address_Box.Text = temp[2];
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow newrow = dataGridView1.Rows[rowindex];
            newrow.Cells[0].Value = Name_box.Text;
            newrow.Cells[1].Value =comboBox1.SelectedItem.ToString();
            newrow.Cells[2].Value =citybox.Text;
            newrow.Cells[3].Value =emailbox.Text;
            newrow.Cells[4].Value = emailbox2.Text;
            newrow.Cells[5].Value = contactBox1.Text;
            newrow.Cells[6].Value = contactbox2.Text;
            newrow.Cells[7].Value = address_Box.Text;
            TextWriter writer = new StreamWriter(path, true);
            string ctn ="\n"+ Name_box.Text + "\t,\t" + comboBox1.SelectedItem.ToString() + "\t,\t" + address_Box.Text + "\t,\t" + citybox.Text + "\t,\t" + emailbox.Text + "\t,\t" + emailbox2.Text + "\t,\t" + contactBox1.Text + "\t,\t" + contactbox2.Text + "\t,\t"+address_Box.Text;
            writer.Write(ctn+"\n");

            writer.Close();
            Table.Rows.Add(Name_box.Text, comboBox1.SelectedItem.ToString(), citybox.Text, emailbox.Text, emailbox2.Text, contactBox1.Text, contactbox2.Text,address_Box.Text);
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        

     
      

        
    }
}
