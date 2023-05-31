using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;

namespace eczane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("data source=DESKTOP-P44PA8U\\SQLEXPRESS;Initial Catalog=eczaneilacstok;Integrated Security=true");
        private void verilerigoruntule()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("select *from ilac", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["ilacad"].ToString());
                ekle.SubItems.Add(oku["ilacsirketi"].ToString());
                ekle.SubItems.Add(oku["ilacturu"].ToString());
                ekle.SubItems.Add(oku["ilackutuadedi"].ToString());
                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoruntule();
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                    baglan.Open();
                string kayit = "insert into ilac(id,ilacad,ilacsirketi,ilacturu,ilackutuadedi) values (@id,@ilacad,@ilacsirketi,@ilacturu,@ilackutuadedi)";
                SqlCommand komut = new SqlCommand(kayit, baglan);
                komut.Parameters.AddWithValue("@id", textBox1.Text);
                komut.Parameters.AddWithValue("@ilacad", textBox2.Text);
                komut.Parameters.AddWithValue("@ilacsirketi", textBox3.Text);
                komut.Parameters.AddWithValue("@ilacturu", textBox4.Text);
                komut.Parameters.AddWithValue("@ilackutuadedi", textBox5.Text);

                komut.ExecuteNonQuery();

                baglan.Close();
                MessageBox.Show("Kayýt Ýþlemi Gerçekleþti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayýt Ýþlemi Sýrasýnda Hata Oluþtu." + hata.Message);
            }
            verilerigoruntule();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand kayýtsil = new SqlCommand("Delete from ilac where id=@id", baglan);

            kayýtsil.Parameters.AddWithValue("@id", textBox1.Text);
            kayýtsil.ExecuteNonQuery();
            baglan.Close();
            verilerigoruntule();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int id = 0;
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = 0;
            baglan.Open();
            SqlCommand komut = new SqlCommand("update ilac set id='" + textBox1.Text.ToString() + "',ilacad='" + textBox2.Text.ToString() + "',ilacsirketi='" + textBox3.Text.ToString() + "',ilacturu='" + textBox4.Text.ToString() + "',ilackutuadedi='" + textBox5.Text.ToString() + "'where id =" + id + "", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigoruntule();
        }
    }
}