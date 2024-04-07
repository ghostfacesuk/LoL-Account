using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LoL_Accounts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Create Account button
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the account name from the input box
            string accountName = textBox1.Text.Trim();

            // Check if the account name is not empty
            if (!string.IsNullOrWhiteSpace(accountName))
            {
                try
                {
                    // Create a directory if it doesn't exist
                    string accountsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\Accounts");
                    if (!Directory.Exists(accountsFolderPath))
                    {
                        Directory.CreateDirectory(accountsFolderPath);
                    }

                    // Create a new .txt file and store the account name in the first directory
                    string fileName = Path.Combine(accountsFolderPath, "accounts.txt");
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.WriteLine(accountName);
                    }

                    // Store accounts.txt file in the second directory
                    string secondFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data");
                    if (!Directory.Exists(secondFolderPath))
                    {
                        Directory.CreateDirectory(secondFolderPath);
                    }
                    string secondFileName = Path.Combine(secondFolderPath, "accounts.txt");
                    File.Copy(fileName, secondFileName, true);

                    // Copy RiotGamesPrivateSettings.yaml to the Accounts folder
                    string sourceFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\RiotGamesPrivateSettings.yaml");
                    string destinationFilePath = Path.Combine(accountsFolderPath, "RiotGamesPrivateSettings.yaml");

                    // Copy the file
                    File.Copy(sourceFilePath, destinationFilePath, true);

                    MessageBox.Show("Account created successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter an account name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Create Account input name text box
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Create Account label
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
