# DovizKurWinForm
Sql bağlantılarında server ismini değiştirerek uygulamayı çalışır hale getirebiliriz. => (Data Source = LAPTOP-O83MN8VH) 

baglanti = new SqlConnection("Data Source = LAPTOP-O83MN8VH;  Database = DovizKuru; Trusted_Connection = True;");  

App.config içinde bağlantı bulunmaktadır.

<connectionStrings>
    <add name="Context" connectionString="Server=LAPTOP-O83MN8VH; Database=DovizKuru; Trusted_Connection=True;" providerName="System.Data.SqlClient"/>
  </connectionStrings>
