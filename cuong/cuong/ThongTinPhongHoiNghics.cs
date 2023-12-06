using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cuong
{
    public partial class ThongTinPhongHoiNghics : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = "Data Source=DESKTOP-BRGJGMP\\MSSQLSERVER156;Initial Catalog=QLHN; Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public ThongTinPhongHoiNghics()
        {
            InitializeComponent();

        }
        public void LoadData()
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM HoiNghi";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
           txt_mhn.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_thn.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_sntg.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
           cb_phn.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void ThongTinPhongHoiNghics_Load(object sender, System.EventArgs e)
        {
            con = new SqlConnection(str);
            con.Open();
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                cmd = con.CreateCommand();
                cmd.CommandText = @"DELETE FROM HoiNghi WHERE maHoiNghi = '" + txt_mhn.Text + "'";
                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_mhn.Text == string.Empty)
                    MessageBox.Show("Bạn chưa nhập mã loại sân", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (txt_thn.Text == string.Empty)
                        MessageBox.Show("Bạn chưa nhập tên loại sân", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (cb_phn.Text == string.Empty)
                            MessageBox.Show("Bạn chưa nhập đơn giá loại sân", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                          if (txt_sntg.Text == string.Empty)
                            MessageBox.Show("Bạn chưa nhập đơn giá loại sân", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                cmd = con.CreateCommand();
                                cmd.CommandText = @"INSERT INTO HoiNghi VALUES(N'" + txt_mhn.Text + @"',N'" + txt_thn.Text + @"','"+txt_sntg.Text+@"',N'" + cb_phn.Text + "')";
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Thêm Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lỗi, Không Được Nhập Trùng Mã Loại Sân", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
