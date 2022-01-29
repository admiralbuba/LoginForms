using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            using ApplicationContext db = new();
            UserDataGridView.DataSource = db.UserDatas.ToList();
        }

        private void UserDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var user = (UserData)UserDataGridView.Rows[e.RowIndex].DataBoundItem;
            using ApplicationContext db = new();
            db.Update(user);
            db.SaveChanges();
        }
    }
}
