using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EasyCon2.Forms
{
    internal class PackProjectDialog : Form
    {
        private TextBox txtName;
        private RadioButton rbMerge;
        private RadioButton rbNewName;
        private RadioButton rbCancel;
        private Button btnOk;
        private Button btnCancel;
        private Label lblInfo;

        public string ProjectName { get; private set; }
        public bool MergeReplace { get; private set; }

        public PackProjectDialog(string initialName, string scriptDir, string imgDir)
        {
            Text = "打包项目 - 选项";
            Width = 480;
            Height = 220;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            lblInfo = new Label() { Left = 10, Top = 10, Width = 440, Height = 30 };
            lblInfo.Text = $"目标 Script: {scriptDir}\r\n目标 ImgLabel: {imgDir}";
            Controls.Add(lblInfo);

            var lbl = new Label() { Left = 10, Top = 50, Text = "目标已存在，选择操作：", AutoSize = true };
            Controls.Add(lbl);

            rbMerge = new RadioButton() { Left = 10, Top = 75, Width = 440, Text = "合并并替换已有同名文件(推荐)" };
            rbNewName = new RadioButton() { Left = 10, Top = 100, Width = 440, Text = "使用其他项目名" };
            rbCancel = new RadioButton() { Left = 10, Top = 125, Width = 440, Text = "取消" };
            Controls.Add(rbMerge);
            Controls.Add(rbNewName);
            Controls.Add(rbCancel);

            txtName = new TextBox() { Left = 220, Top = 98, Width = 240 };
            txtName.Text = initialName;
            Controls.Add(txtName);

            btnOk = new Button() { Left = 260, Top = 150, Width = 80, Text = "确定", DialogResult = DialogResult.OK };
            btnCancel = new Button() { Left = 360, Top = 150, Width = 80, Text = "取消", DialogResult = DialogResult.Cancel };
            Controls.Add(btnOk);
            Controls.Add(btnCancel);

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            rbMerge.Checked = true;

            btnOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            if (rbCancel.Checked)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            if (rbNewName.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("请输入新的项目名。");
                    DialogResult = DialogResult.None;
                    return;
                }
                ProjectName = txtName.Text.Trim();
                MergeReplace = false;
            }
            else
            {
                ProjectName = txtName.Text.Trim();
                MergeReplace = true;
            }
        }
    }
}
