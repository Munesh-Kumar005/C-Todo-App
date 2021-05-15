using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CrudOpperations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GFMM88C\SQLEXPRESS;Initial Catalog=Crudsharp;Integrated Security=True");
        public int StudentId;
        private void button3_Click(object sender, EventArgs e)
        {
            Reset();

        }

        private void Reset()
        {
            StudentId = 0;
            txtSName.Clear();
            txtFName.Clear();
            txtRollNo.Clear();
            txtAddress.Clear();
            txtMobile.Clear();
            txtSName.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();

        }

        private void GetStudentRecord()
        {
           
            SqlCommand cmd = new SqlCommand("select * from StudentTb", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecorddataGridView.DataSource = dt;

        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentTb VALUES (@Name,@FatherName,@RollNumber,@Address,@Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtSName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New Record save" ,"Inserted Record",MessageBoxButtons.OK,MessageBoxIcon.Information);
                GetStudentRecord();
                Reset();
            }
        }

        private bool isValid()
        {
            if(txtSName.Text == "")
            {
                MessageBox.Show("Student name is required", "Failed", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void StudentRecorddataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentId = Convert .ToInt32(StudentRecorddataGridView.SelectedRows[0].Cells[0].Value);
            txtSName.Text = StudentRecorddataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFName.Text = StudentRecorddataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNo.Text = StudentRecorddataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecorddataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecorddataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentTb SET Name = @Name, FatherName = @FatherName,@RollNumber = RollNumber,Address  = @Address,Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtSName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Information Updated", "Updated Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentRecord();
                Reset();
            }
            else
            {
                MessageBox.Show("Plz select", "select ? Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE StudentTb  WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
              
                cmd.Parameters.AddWithValue("@ID", this.StudentId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted", "Deleted Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentRecord();
                Reset();
            }

            else
            {
                MessageBox.Show("Plz select for delete", "select ? Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
