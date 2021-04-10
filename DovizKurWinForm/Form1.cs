using DovizKurWinForm.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DovizKurWinForm.DataBase;
using System.Data.SqlClient;

namespace DovizKurWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Context context = new Context();
            context.Database.Create();
        }

        SqlConnection baglanti;
        SqlCommand komut;

        
       

        private void button1_Click(object sender, EventArgs e)
        {
            WebProxy webProxy = new WebProxy(WebProxy.GetDefaultProxy().Address);

            WebClient webClient = new WebClient();
            webClient.Proxy = webProxy;
            string url = webClient.DownloadString("https://www.tcmb.gov.tr/kurlar/today.xml");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(url);

            XmlNodeList liste = xmlDocument.SelectNodes("Tarih_Date/Currency");

            void gridAyari()
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ColumnCount = 3;
                dataGridView1.Columns[0].Name = "ad";
                dataGridView1.Columns[1].Name = "alis";
                dataGridView1.Columns[2].Name = "satis";
                
                
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                
                
            }

            foreach (XmlNode item in liste)
            {
                string ad = item["Isim"].InnerText;
                string alis = item["ForexBuying"].InnerText;
                string satis = item["ForexSelling"].InnerText;
                
                


                // listBox1.Items.Add(ad + "-----" + alis + "-----" + satis);
                gridAyari();
                dataGridView1.Rows.Add(ad, alis, satis);
                
            }

            //Context context = new Context();
            //context.Database.Create();

            //listBox1.FormattingEnabled = true;
            //listBox1.HorizontalScrollbar = true;
            //string[] veriler = { ad, alis, satis };

            if (dataGridView1.Rows.Count != 0)
            {
                
                
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {

                    var sorgu = "insert into Kurs(ad,alis,satis) values (@ad,@alis,@satis)";

                    baglanti = new SqlConnection("Data Source = LAPTOP-O83MN8VH;  Database = DovizKuru; Trusted_Connection = True;");
                    baglanti.Open();
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@ad", dataGridView1.Rows[i].Cells["ad"].Value.ToString());
                    komut.Parameters.AddWithValue("@alis", float.Parse(dataGridView1.Rows[i].Cells["alis"].Value.ToString()));
                    // komut.Parameters.AddWithValue("@satis", float.Parse( dataGridView1.Rows[i].Cells["satis"].Value.ToString()));
                    if (dataGridView1.Rows[i].Cells["satis"].Value == null)
                    {
                        komut.Parameters.AddWithValue("@satis", DBNull.Value);
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@satis", dataGridView1.Rows[i].Cells["satis"].Value.ToString());
                    }
                    
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }



                //foreach (var items in dataGridView1.Rows)
                //{
                //    komut = new SqlCommand(sorgu, baglanti);
                //    komut.Parameters.AddWithValue("@ad", dataGridView1.Rows[]);
                //    komut.Parameters.AddWithValue("@alis", items[1]);
                //    komut.Parameters.AddWithValue("@satis", items[2]);
                //    komut.Parameters.AddWithValue("@alis", items[1]);
                //    komut.Parameters.AddWithValue("@satis", items[2]);



                //    baglanti.Open();
                //    komut.ExecuteNonQuery();
                //    baglanti.Close();
                //}
                MessageBox.Show("Başarılı olarak eklendi", "Kaydetme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Başarısız", "Kaydetme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }


        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.Rows.Count != 0)
        //    {
        //        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
        //        {

        //            var sorgu = "insert into Kurs(ad,alis,satis) values (@ad,@alis,@satis)";

        //            baglanti = new SqlConnection("Data Source = LAPTOP-O83MN8VH;  Database = DovizKuru; Trusted_Connection = True;");
        //            baglanti.Open();
        //            komut = new SqlCommand(sorgu, baglanti);
        //            komut.Parameters.AddWithValue("@ad",dataGridView1.Rows[i].Cells["ad"].Value.ToString());
        //            komut.Parameters.AddWithValue("@alis", float.Parse( dataGridView1.Rows[i].Cells["alis"].Value.ToString()));
        //            // komut.Parameters.AddWithValue("@satis", float.Parse( dataGridView1.Rows[i].Cells["satis"].Value.ToString()));
        //            if (dataGridView1.Rows[i].Cells["satis"].Value==null)
        //            {
        //                komut.Parameters.AddWithValue("@satis", DBNull.Value);
        //            }
        //            else
        //            {
        //                komut.Parameters.AddWithValue("@satis",dataGridView1.Rows[i].Cells["satis"].Value.ToString());
        //            }
        //            komut.ExecuteNonQuery();
        //            baglanti.Close();
        //        }


        //        //foreach (var items in dataGridView1.Rows)
        //        //{
        //        //    komut = new SqlCommand(sorgu, baglanti);
        //        //    komut.Parameters.AddWithValue("@ad", dataGridView1.Rows[]);
        //        //    komut.Parameters.AddWithValue("@alis", items[1]);
        //        //    komut.Parameters.AddWithValue("@satis", items[2]);
        //        //    komut.Parameters.AddWithValue("@alis", items[1]);
        //        //    komut.Parameters.AddWithValue("@satis", items[2]);



        //        //    baglanti.Open();
        //        //    komut.ExecuteNonQuery();
        //        //    baglanti.Close();
        //        //}
        //        MessageBox.Show("Başarılı olarak eklendi", "Kaydetme İşlemi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Başarısız", "Kaydetme İşlemi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
        //    }
        //}
    }
}
