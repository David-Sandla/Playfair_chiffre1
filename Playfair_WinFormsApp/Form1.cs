namespace Playfair_WinFormsApp;
using Playfair_Chiffre_Lib;
using PlayfairChiffre_Lib;

public partial class Form1 : Form
{
    private IChiffre chiffre;
    
    public Form1()
    {
        InitializeComponent();
    }
    private void EncryptButton_Click(object sender, EventArgs e)
    {
        if (!TryInitChiffre()) return;
        string text = InputTextBox.Text;
        string encrypted = chiffre.Encrypt(text);
        OutputTextBox.Text = encrypted;
    }

    private void DecryptButton_Click(object sender, EventArgs e)
    {
        if (!TryInitChiffre()) return;
        string text = InputTextBox.Text;
        string decrypted = chiffre.Decrypt(text);
        OutputTextBox.Text = decrypted;
    }

    private bool TryInitChiffre()
    {
        string key = KeyTextBox.Text;
        if (string.IsNullOrWhiteSpace(key))
        {
            MessageBox.Show("Bitte gib einen Schlüssel ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        chiffre = new PlayfairChriffre(key);
        return true;
    }
}