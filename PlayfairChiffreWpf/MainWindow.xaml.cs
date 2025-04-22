using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Playfair_Chiffre_Lib;
using PlayfairChiffre_Lib;

namespace PlayfairChiffreWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IChiffre chiffre;
    
    public MainWindow()
    {
        InitializeComponent();
    }
    private void OnEncrypt(object sender, RoutedEventArgs e)
    {
        if (!TryInitChiffre()) return;
        string text = InputTextBox.Text;
        string encrypted = chiffre.Encrypt(text);
        OutputTextBlock.Text = encrypted;
    }

    private void OnDecrypt(object sender, RoutedEventArgs e)
    {
        if (!TryInitChiffre()) return;
        string text = InputTextBox.Text;
        string decrypted = chiffre.Decrypt(text);
        OutputTextBlock.Text = decrypted;
    }

    private bool TryInitChiffre()
    {
        string key = KeyTextBox.Text;
        if (string.IsNullOrWhiteSpace(key))
        {
            MessageBox.Show("Bitte gib einen Schlüssel ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        chiffre = new PlayfairChriffre(key);
        return true;
    }
}