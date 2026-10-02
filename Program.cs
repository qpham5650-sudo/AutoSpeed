using System;
using System.Collections.Generic;

namespace AutoSpeed
{
    abstract class PhuongTien
    {
        private string _maPT;
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        public string MaPT
        {
            get { return _maPT; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _maPT = "PT000";
                else
                    _maPT = value;
            }
        }

        public string TenHang
        {
            get { return _tenHang; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên hãng không được để trống!");

                _tenHang = value;
            }
        }

        public int NamSanXuat
        {
            get { return _namSanXuat; }
            set
            {
                int year = DateTime.Now.Year;

                if (value < 1900 || value > year)
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");

                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get { return _giaGoc; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Giá gốc phải > 0");

                _giaGoc = value;
            }
        }

        public PhuongTien(string ma, string ten,
            int namSX, decimal gia)
        {
            MaPT = ma;
            TenHang = ten;
            NamSanXuat = namSX;
            GiaGoc = gia;
        }

        public abstract decimal TinhGiaLanBanh();

        public virtual string GetInfo()
        {
            return MaPT + " - "
                 + TenHang + " - "
                 + NamSanXuat;
        }
    }

    class OTo : PhuongTien
    {
        public int SoChoNgoi { get; set; }
        public double DungTichDongCo { get; set; }

        public OTo(
            string ma,
            string ten,
            int nam,
            decimal gia,
            int cho,
            double dongCo)
            : base(ma, ten, nam, gia)
        {
            SoChoNgoi = cho;
            DungTichDongCo = dongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                return GiaGoc +
                    GiaGoc * 0.12m +
                    GiaGoc * 0.30m;
            }

            return GiaGoc + GiaGoc * 0.10m;
        }

        public override string GetInfo()
        {
            return base.GetInfo()
                + " - " + SoChoNgoi + " chỗ";
        }
    }

    class XeMay : PhuongTien
    {
        public int DungTichXylanh { get; set; }

        public XeMay(
            string ma,
            string ten,
            int nam,
            decimal gia,
            int cc)
            : base(ma, ten, nam, gia)
        {
            DungTichXylanh = cc;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
                return GiaGoc + GiaGoc * 0.02m;

            return GiaGoc + GiaGoc * 0.05m;
        }
    }

    class QuanLyPhuongTien
    {
        List<PhuongTien> ds =
            new List<PhuongTien>();

        public void Add(PhuongTien pt)
        {
            ds.Add(pt);
        }

        public void DisplayAll()
        {
            foreach (PhuongTien pt in ds)
            {
                Console.WriteLine(
                    pt.GetInfo());

                Console.WriteLine(
                    "Gia lan banh: "
                    + pt.TinhGiaLanBanh());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            QuanLyPhuongTien ql =
                new QuanLyPhuongTien();

            OTo oto =
                new OTo(
                    "OT01",
                    "Toyota",
                    2023,
                    1000000000m,
                    5,
                    2.0);

            XeMay xm =
                new XeMay(
                    "XM01",
                    "Honda",
                    2024,
                    50000000m,
                    150);

            ql.Add(oto);
            ql.Add(xm);

            ql.DisplayAll();

            Console.ReadKey();
        }
    }
}
