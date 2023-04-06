using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insert_and_get_data
{
    class Program
    {
        static void Main(string[] args)
        {
            string jwb;
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID: ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password: ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan Database tujuan: ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = AMMAR\\MSSQLSERVER01; " +
                                    "initial catalog = {0}; " +
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat seluruh data");
                                        Console.WriteLine("2. Tambah data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("4. Hapus data");
                                        Console.WriteLine("5. Search Data");
                                        Console.WriteLine("6. Ubah Data");
                                        Console.WriteLine("\nEnter your choice (1-4): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Gramedia1\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Gramedia\n");
                                                    Console.WriteLine("Masukkan Data Pembeli :");
                                                    string Pembeli = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Pembelian :");
                                                    string Pembelian = Console.ReadLine();
                                                    Console.WriteLine("Masukkan ID Kasir :");
                                                    string Kasir = Console.ReadLine();
                                                    Console.WriteLine("Masukkan ID Buku :");
                                                    string Buku = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Rak :");
                                                    string Rak = Console.ReadLine();
                                                    
                                                    try
                                                    {
                                                        pr.insert(Pembeli, Pembelian, Kasir, Buku, Rak, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Hapus Data Pembeli");
                                                    Console.WriteLine("Masukan Id_Pembeli: ");
                                                    string Pembeli = Console.ReadLine();
                                                    Console.WriteLine("Apakah anda yakin ingin menghapus Data Pembeli ini?(y)");
                                                    jwb = Console.ReadLine();

                                                    if (jwb.Equals("y"))
                                                    {
                                                        try
                                                        {
                                                            pr.delete(Pembeli, conn);
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Anda tidak memiliki " +
                                                                "akses untuk menghapus data");
                                                        }
                                                    }
                                                    else break;
                                                }
                                                break;
                                            case '5':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Cari Data Pembeli");
                                                    Console.WriteLine("Masukan Id_Pembeli: ");
                                                    string Pembeli = Console.ReadLine();
                                                    Console.WriteLine("Apakah anda yakin ingin Mencari Data Pembeli ini?(y)");
                                                    jwb = Console.ReadLine();

                                                    if (jwb.Equals("y"))
                                                    {
                                                        try
                                                        {
                                                            pr.search(Pembeli, conn);
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Anda tidak memiliki " +
                                                                "akses untuk mencari data");
                                                        }
                                                    }
                                                    else break;
                                                }
                                                break;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From Pembeli", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();

            }
            r.Close();
        }

        public void insert(string Pembeli, string Pembelian, string Kasir, string Buku, string Rak, SqlConnection con)
        {
            string str = "";
            str = "insert into Pembeli (Pembeli, Pembelian,Kasir, Buku, Rak)" + "values(@Pembeli, @Pembelian, @Kasir, @Buku, @Rak)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("Pembeli", Pembeli));
            cmd.Parameters.Add(new SqlParameter("Pembelian", Pembelian));
            cmd.Parameters.Add(new SqlParameter("Kasir", Kasir));
            cmd.Parameters.Add(new SqlParameter("Buku", Buku));
            cmd.Parameters.Add(new SqlParameter("Rak", Rak));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
        public void delete(string Pembeli, SqlConnection con)
        {
            string str = "";
            str = "delete from Pembeli where Pembeli = @Pembeli";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("Pembeli", Pembeli));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Dihapus");
        }
        public void search(string Buku, SqlConnection con)
        {
            string str = "";
            str = "select Buku from Buku where Buku = @Buku";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("Buku", Buku));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditemukan");
        }
        public void change(string Buku, SqlConnection con)
        {
            string str = "";
            str = "select Buku from Buku where Buku = @Buku";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("Buku", Buku));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditemukan");
        }

    }
}