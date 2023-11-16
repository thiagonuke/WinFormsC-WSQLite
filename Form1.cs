using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using WinFormsApp_Anne.Class;

namespace WinFormsApp_Anne
{
    public partial class Form1 : Form
    {
        private readonly string ConnectionString = "Data Source=C:\\Projetos\\projC\\WinFormsApp_Anne\\Database.db;";

        public Form1()
        {

            InitializeComponent();

            EstilizarLogin();

        }


        #region MetodosLogin

        private void button1_Click(object sender, EventArgs e)
        {
            if(email.Text != "" && senha.Text != "")
            {
                var emailTXT = email.Text;
                var senhaTXT = senha.Text;

                DataTable dataTable = obterdataTables(emailTXT, senhaTXT);

                Usuario usuario = new Usuario();

                try
                {
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow row in dataTable.Rows)
                        {
                            usuario.Nome = row["Nome"].ToString();
                            usuario.Email = row["Email"].ToString();
                            usuario.Senha = row["Senha"].ToString();
                            usuario.Grupo = row["Grupo"].ToString();

                        }

                        MessageBox.Show("Usuario encontrado com sucesso!");

                        //================ Implementar app após login  aqui ===============





                        //==================================================================

                    }
                    else
                    {
                        MessageBox.Show("Usuario não encontrado");
                    }
                }
                catch(Exception ex) { }

                  
            }
            else
            {
                MessageBox.Show("Preencha os dados corretamente para prosseguir!");
            }



        }

        private DataTable obterdataTables(string emailTXT, string senhaTXT)
        {
            var data = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM cadastros where senha = {senhaTXT} and email = '{emailTXT}'";


                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(data);
                    }
                }
            }

            return data;
        }


        #endregion

        #region EstilizarLogin
        private void EstilizarLogin()
        {

            try
            {
                Color corPersonalizada = ColorTranslator.FromHtml("#B5E4FF");
                Color corBorda = ColorTranslator.FromHtml("#5EAEE0");

                int valorTransparencia = 128;

                this.BackColor = Color.Black;

                Color corTransparente = Color.FromArgb(valorTransparencia, corPersonalizada);

                button1.BackColor = ColorTranslator.FromHtml("#00FFFFFF");
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 0;

                button1.FlatAppearance.BorderColor = Color.FromArgb(94, 174, 224); // #5EAEE0
                button1.FlatAppearance.BorderSize = 2;
                button1.Cursor = Cursors.Hand;

                Color corFundo = Color.FromArgb(128, 0, 0, 0);
                button1.BackColor = corFundo;

                pictureBox1.Paint += (s, ev) =>
                {

                    using (Pen pen = new Pen(corBorda, 5))
                    {
                        int radius = 20;
                        int x = 0;
                        int y = 0;
                        int width = pictureBox1.Width;
                        int height = pictureBox1.Height;

                        ev.Graphics.DrawArc(pen, x, y, radius * 2, radius * 2, 180, 90);
                        ev.Graphics.DrawLine(pen, x + radius, y, x + width - radius, y);
                        ev.Graphics.DrawArc(pen, x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
                        ev.Graphics.DrawLine(pen, x + width, y + radius, x + width, y + height - radius);
                        ev.Graphics.DrawArc(pen, x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
                        ev.Graphics.DrawLine(pen, x + width - radius, y + height, x + radius, y + height);
                        ev.Graphics.DrawArc(pen, x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
                        ev.Graphics.DrawLine(pen, x, y + height - radius, x, y + radius);
                    }
                };

                ArredondarBordas(email);
                ArredondarBordas(senha);
                ArredondarBordas(button1);



            }
            catch (Exception ex) { }


        }

        private void ArredondarBordas(Control control)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 10;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
        }

        #endregion

        
    }
}