using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test5
{
    public partial class trangchu : Form
    {
        private Size originalFormSize;
        private Dictionary<Control, Rectangle> originalControlBounds = new Dictionary<Control, Rectangle>();
        private Dictionary<Control, float> originalFontSizes = new Dictionary<Control, float>();


        public trangchu()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;
            SaveOriginalBounds(this);
        }

        private void SaveOriginalBounds(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                originalControlBounds[c] = c.Bounds;
                originalFontSizes[c] = c.Font.Size; // Lưu font size ban đầu

                if (c.HasChildren)
                    SaveOriginalBounds(c);
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;
            ResizeControls(this, xRatio, yRatio);
        }

        private void ResizeControls(Control parent, float xRatio, float yRatio)
        {
            foreach (Control c in parent.Controls)
            {
                if (originalControlBounds.ContainsKey(c))
                {
                    Rectangle original = originalControlBounds[c];
                    c.Left = (int)(original.Left * xRatio);
                    c.Top = (int)(original.Top * yRatio);
                    c.Width = (int)(original.Width * xRatio);
                    c.Height = (int)(original.Height * yRatio);

                    // Resize font đúng cách từ font gốc đã lưu
                    if (originalFontSizes.ContainsKey(c))
                    {
                        float originalFontSize = originalFontSizes[c];
                        c.Font = new Font(c.Font.FontFamily, originalFontSize * Math.Min(xRatio, yRatio), c.Font.Style);
                    }
                }

                // 🔁 Gọi đệ quy cho control con
                if (c.HasChildren)
                {
                    ResizeControls(c, xRatio, yRatio);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void trangchu_btn_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();      // Xóa toàn bộ controls hiện tại
            this.InitializeComponent(); // Tải lại từ đầu (nạp lại Designer)
            Form1_Load(this, EventArgs.Empty); // Gọi lại màn hình load
        }

        private void lichlamviec_btn_Click(object sender, EventArgs e)
        {

        }

        private void kiemhang_btn_Click(object sender, EventArgs e)
        {

        }
        private void nhanvien_btn_Click(object sender, EventArgs e)
        {
            nhanvien t = new nhanvien();
            t.Dock = DockStyle.Fill;
            pictureBox1.Controls.Clear();
            pictureBox1.Controls.Add(t);
        }

        private void thoatra_btn_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo" + "" + "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
            // else: không làm gì, đóng hộp thoại và quay lại form
        }

        private void cafeshop_lb_Click(object sender, EventArgs e)
        {

        }

        private void trangchu_Load(object sender, EventArgs e)
        {

        }
    }
}
