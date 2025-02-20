using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WpfHocvien.Models;

namespace WpfHocvien.MyModels
{
    class CHocvien
    {

        public string Mshv { get; set; }
        public string? Tenhv { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public bool? Phai { get; set; } 
        public string? Malop { get; set; }
        public string? NgaysinhView
        {
            get
            {
                if (Ngaysinh == null || Ngaysinh.HasValue==false)
                {
                    return string.Empty;
                }
                else
                {
                    return Ngaysinh.Value.ToShortDateString();
                }
            }
        }
        public string? PhaiView
        {
            get
            {
                if (Phai == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Phai.Value ? "Nam" : "Nữ";
                }
            }
        }
        public string Tenlop
        {
            get
            {
                if (MalopNavigation == null)
                {
                    return string.Empty;
                }
                else
                {
                    return MalopNavigation.Tenlop;
                }
            }
        }
        public Lop? MalopNavigation { get; set; }
        public static CHocvien chuyendoi(Lylich x)
        {
            return new CHocvien
            {
                Mshv = x.Mshv,
                Tenhv = x.Tenhv,
                Ngaysinh = x.Ngaysinh,
                Phai = x.Phai,
                Malop = x.Malop,
                MalopNavigation = x.MalopNavigation
            };


        }
        public static Lylich chuyendoi(CHocvien x)
        {
            return new Lylich
            {
                Mshv = x.Mshv,
                Tenhv = x.Tenhv,
                Ngaysinh = x.Ngaysinh,
                Phai = x.Phai,
                Malop = x.Malop,
                MalopNavigation = x.MalopNavigation
            };


        }


    }
}
