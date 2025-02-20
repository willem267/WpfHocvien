using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfHocvien.Models;
using WpfHocvien.MyModels;

namespace WpfHocvien.UI
{
    /// <summary>
    /// Interaction logic for WindowHocvien.xaml
    /// </summary>
    public partial class WindowHocvien : Window
    {
        public static RoutedUICommand lenhThem = new RoutedUICommand();
        public static RoutedUICommand lenhXoa = new RoutedUICommand();
        public static RoutedUICommand lenhSua = new RoutedUICommand();
        public WindowHocvien()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            qlhvContext db = new qlhvContext();
            List<Lylich> ds = db.Lyliches.ToList();
            foreach (Lylich lylich in ds)
            {
                lylich.MalopNavigation = db.Lops.Find(lylich.Malop);
            }
            dgHocvien.ItemsSource = ds.Select(t=>CHocvien.chuyendoi(t)).ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            qlhvContext db = new qlhvContext();
            cmbMalop.ItemsSource = db.Lops.ToList();
            hienthi();
        }

        private void them_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CHocvien hv = gridHocvien.DataContext as CHocvien;
            Lylich x = CHocvien.chuyendoi(hv);
            qlhvContext db = new qlhvContext();
            if (x == null || string.IsNullOrEmpty(x.Mshv) || string.IsNullOrEmpty(x.Tenhv) || string.IsNullOrEmpty(x.Malop) || x.Phai ==null || x.Ngaysinh ==null)
            {
                e.CanExecute = false;
                return;
            }
            if (db.Lyliches.Find(x.Mshv) != null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;

        }

        private void them_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CHocvien hv = gridHocvien.DataContext as CHocvien;
            Lylich x = CHocvien.chuyendoi(hv);
            qlhvContext db = new qlhvContext();
            db.Lyliches.Add(x);
            db.SaveChanges();
            hienthi();

        }

        private void xoa_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(dgHocvien==null || dgHocvien.SelectedItem == null)
            {
                e.CanExecute = false;
                return;
            }
           CHocvien hv = dgHocvien.SelectedItem as CHocvien;
            qlhvContext db = new qlhvContext();
            Diemthi x = null;
            foreach(Diemthi d in db.Diemthis.Where(t=> t.Mshv == hv.Mshv))
            {
                x = d;
                break;
            }
            if (x!=null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
        }

        private void xoa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CHocvien hv = dgHocvien.SelectedItem as CHocvien;
            MessageBoxResult ok = MessageBox.Show("Mày có thật sự muốn xóa?", "Cảnh báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            qlhvContext db = new qlhvContext();
            Lylich ll = db.Lyliches.Find(hv.Mshv);
            if(ll!=null)
            {
               
                db.Lyliches.Remove(ll);
                db.SaveChanges();
                hienthi();

            }
        }

        private void sua_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void sua_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CHocvien hv = gridHocvien.DataContext as CHocvien;
            Lylich x = CHocvien.chuyendoi(hv);
            qlhvContext db = new qlhvContext();
            Lylich ll = db.Lyliches.Find(x.Mshv);
            if (ll != null)
            {
                ll.Tenhv = x.Tenhv;
                ll.Ngaysinh = x.Ngaysinh;
                ll.Phai = x.Phai;
                //ll.Malop = x.Malop;
                db.Lyliches.Update(ll);
                db.SaveChanges();
                hienthi();
            }

        }

        private void dgHocvien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgHocvien == null || dgHocvien.SelectedItem == null)
            {
                return;
            }
            CHocvien hv = dgHocvien.SelectedItem as CHocvien;
            CHocvien h = new CHocvien();
            txtMshv.IsReadOnly = true;
            cmbMalop.IsEnabled = false;
            h.Mshv=hv.Mshv;
            h.Tenhv = hv.Tenhv;
            h.Ngaysinh = hv.Ngaysinh;
            h.Phai = hv.Phai;
            h.Malop = hv.Malop;
            gridHocvien.DataContext = h;
        }
    }
}
