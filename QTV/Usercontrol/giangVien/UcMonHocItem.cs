using QTV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTV.Usercontrol.giangVien
{
    public partial class UcMonHocItem : UserControl
    {
        public MonHoc MonHoc { get; set; }
        public event EventHandler<MonHoc> ItemClicked;
        public string MaMon { get => lblMaMon.Text; set => lblMaMon.Text = value; }
        public string TenMon { get => lblTenMon.Text; set => lblTenMon.Text = value; }
        public UcMonHocItem()
        {
            InitializeComponent();
            SetupEvents();
        }
        private void SetupEvents()
        {
            this.Click += UcMonHoc_Click;
            mainPanel.Click += UcMonHoc_Click;
            img.Click += UcMonHoc_Click;
            container.Click += UcMonHoc_Click;
        }

        private void UcMonHoc_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, MonHoc);
        }
    }
}
