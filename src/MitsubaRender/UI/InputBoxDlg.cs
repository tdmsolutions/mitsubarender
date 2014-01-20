using System;
using System.Windows.Forms;

namespace MitsubaRender.UI
{
    public partial class InputBoxDlg : Form
    {
      
        public string InputText = String.Empty;
        public string TopicText = String.Empty;
        public string Titul = String.Empty;


        public InputBoxDlg()
        {
            InitializeComponent();
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(InputText) || String.IsNullOrWhiteSpace(InputText))
            {
                //TODO Localize me
                MessageBox.Show("Please type a name");
            }

            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }

        }

        private void InputBoxDlgLoad(object sender, EventArgs e)
        {
            Text = Titul;
            labelX1.Text = TopicText;
            if (!String.IsNullOrEmpty(InputText)) textBoxX1.Text = InputText;
            textBoxX1.Focus();
        }

        private void TextBoxX1TextChanged(object sender, EventArgs e)
        {
            InputText = textBoxX1.Text;
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TextBoxX1KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrEmpty(InputText) || String.IsNullOrWhiteSpace(InputText))
                {
                    //TODO Localize me
                    MessageBox.Show("Please type a name");
                }

                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}