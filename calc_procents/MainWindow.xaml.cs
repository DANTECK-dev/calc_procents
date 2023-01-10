using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calc_procents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Summ_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)e.Source).Text != "Сумма")
            {
                ((TextBox)e.Source).Text = string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c))) + " Рублей";
                //if (((TextBox)e.Source).Text.Length > 10) ((TextBox)e.Source).Text = ((TextBox)e.Source).Text.Substring(0, 10) + " руб.";
                //((TextBox)e.Source).Text += " Рублей";
                if (((TextBox)e.Source).Text == " Рублей") ((TextBox)e.Source).Text = "Сумма";
                Summ_TextBox.SelectionStart = string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c))).Length;
            }
        }

        private void Proc_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)e.Source).Text != "Процент")
            {
                ((TextBox)e.Source).Text = string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c) || c == '.')) + "%";
                //if (((TextBox)e.Source).Text.Length > 10) ((TextBox)e.Source).Text = ((TextBox)e.Source).Text.Substring(0, 10) + "%";
                //((TextBox)e.Source).Text += "%";
                if (((TextBox)e.Source).Text == "%") ((TextBox)e.Source).Text = "Процент";
                Proc_TextBox.SelectionStart = string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c) || c == '.')).Length;
            }
        }

        private void Date_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)e.Source).Text != "Срок в годах")
            {
                int temp_int = int.Parse(string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c))));
                if (temp_int > 200) return;
                string temp_str = "";
                if (temp_int == 0) temp_str = "Лет";
                else if (temp_int >= 5 || temp_int <= 20) temp_str = "Лет";
                else if (temp_int.ToString()[temp_int.ToString().Length - 1] == '1') temp_str = "Год";
                else if (int.Parse(temp_int.ToString()[temp_int.ToString().Length - 1].ToString()) >= 2 || int.Parse(temp_int.ToString()[temp_int.ToString().Length - 1].ToString()) <= 4) temp_str = "Года";
                ((TextBox)e.Source).Text = string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c))) + " " + temp_str;
                //if (((TextBox)e.Source).Text.Length > 10) ((TextBox)e.Source).Text = ((TextBox)e.Source).Text.Substring(0, 10) + " месяцев";
                //((TextBox)e.Source).Text += " Месяцев";
                if (((TextBox)e.Source).Text == " Год" || ((TextBox)e.Source).Text == " Года" || ((TextBox)e.Source).Text == " Лет") ((TextBox)e.Source).Text = "Срок в годах";
                Date_TextBox.SelectionStart = string.Join("", ((TextBox)e.Source).Text.Where(c => char.IsDigit(c))).Length;
            }
        }

        private void Calc_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Summ_TextBox.Text == "Сумма")
            {
                MessageBox.Show("Введите сумму", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (Proc_TextBox.Text == "Процент")
            {
                MessageBox.Show("Введите процентную ставку", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (Date_TextBox.Text == "Срок в годах")
            {
                MessageBox.Show("Введите срок (в годах)", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Result_List.Items.Clear();

            int summ = int.Parse(string.Join("", Summ_TextBox.Text.Where(c => char.IsDigit(c))));
            double proc = double.Parse(string.Join("", Proc_TextBox.Text.Where(c => char.IsDigit(c) || c == '.')).Replace('.', ','));
            int date = int.Parse(string.Join("", Date_TextBox.Text.Where(c => char.IsDigit(c))));
            double capital = summ;
            Result_List.Items.Add(0 + " Год \t Накопления " + capital.ToString("#,#", new CultureInfo("ru-RU")));

            for (int i = 0; i < date; i++)
            {
                capital += capital * proc / 100;
                Result_List.Items.Add((i + 1) + " Год \t Накопления " + capital.ToString("#,#", new CultureInfo("ru-RU")));
            }
        }
    }
}
