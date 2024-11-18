using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QTV.Controllers;

namespace QTV.Usercontrol
{
    public partial class UC_QLLHP : UserControl
    {
        string connectstring = @"Data Source=LAPTOP;Initial Catalog=QLTN;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        private DataGridViewImageColumn deleteColumn;
        public UC_QLLHP()
        {
            InitializeComponent();
            LoadData();
        }

        private void UC_QLLHP_Load(object sender, EventArgs e)
        {
            // LoadData();
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Khi thêm hàng mới, tự động thêm biểu tượng xóa vào cột cuối
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["deleteColumn"].Value == null)
                {
                    dataGridView1.Rows[i].Cells["deleteColumn"].Value = deleteColumn.Image;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào cột xóa không
            if (e.ColumnIndex == dataGridView1.Columns["deleteColumn"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa dòng này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            {
                // Tạo một instance của form FormThemLHP
                QTV.Usercontrol.Form_thêm_LHP formThemLHP = new QTV.Usercontrol.Form_thêm_LHP();

                // Hiển thị form
                formThemLHP.ShowDialog();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form FormThemLHP
            QTV.Usercontrol.Form_thêm_LHP formThemLHP = new QTV.Usercontrol.Form_thêm_LHP();

            // Hiển thị form
            formThemLHP.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        // View specific methods
        private void LoadData()
        {
            ClassController classController = new ClassController();
            var data = classController.LoadClass();
            // select only MaLHP, TenLHP, TenGV, TenMon
            var table = data.DefaultView.ToTable(false, "MaLHP", "TenLHP", "TenGV", "TenMon");
            dataGridView1.DataSource = table;
            dataGridView1.Columns["MaLHP"].HeaderText = "Mã Lớp Học phần";
            dataGridView1.Columns["TenLHP"].HeaderText = "Tên Lớp Học Phần";
            dataGridView1.Columns["TenGV"].HeaderText = "Tên Giảng Viên";
            dataGridView1.Columns["TenMon"].HeaderText = "Tên Môn Học";
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
    
    
}
