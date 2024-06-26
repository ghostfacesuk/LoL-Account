﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LoL_Accounts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateAccountDropdown(); // Populate the account dropdown list on form
        }

        // Populate the dropdown list with account names from accounts.txt
        private void PopulateAccountDropdown()
        {
            string accountsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\Accounts\accounts.txt");
            if (File.Exists(accountsFilePath))
            {
                string[] accountNames = File.ReadAllLines(accountsFilePath);

                // Clear the existing items in the ComboBox before adding the new ones
                comboBox1.Items.Clear();

                comboBox1.Items.AddRange(accountNames);
            }
        }

        // Main form loading
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            button1.Parent = pictureBox1;
            button2.Parent = pictureBox1;
            button3.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button2.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
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

                    // Check if the account name already exists
                    string fileName = Path.Combine(accountsFolderPath, "accounts.txt");
                    if (File.Exists(fileName))
                    {
                        string[] existingAccounts = File.ReadAllLines(fileName);
                        foreach (string existingAccount in existingAccounts)
                        {
                            if (existingAccount.Equals(accountName, StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Account already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if the account already exists
                            }
                        }
                    }

                    // Create a new .txt file and store the account name
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.WriteLine(accountName);
                    }

                    // Rename and copy RiotGamesPrivateSettings.yaml to the Accounts folder
                    string sourceFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\RiotGamesPrivateSettings.yaml");
                    string destinationFilePath = Path.Combine(accountsFolderPath, $"{accountName}.yaml");

                    // Copy the file with renaming
                    File.Copy(sourceFilePath, destinationFilePath, true);

                    MessageBox.Show("Account created successfully!");

                    // Update dropdown menu
                    PopulateAccountDropdown();
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

        // Account selection label
        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        // Account selection
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Load account button
        private void button2_Click(object sender, EventArgs e)
        {
            // Get the selected account name from the dropdown list
            string selectedAccount = comboBox1.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedAccount))
            {
                try
                {
                    // Destination folder paths
                    string riotClientDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data");
                    string lolDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\League of Legends\Data");

                    // Delete the existing "ShutdownData.yaml" file if it exists in Riot Client Data folder
                    string riotClientShutdownDataFilePath = Path.Combine(riotClientDataFolderPath, "ShutdownData.yaml");
                    if (File.Exists(riotClientShutdownDataFilePath))
                    {
                        File.Delete(riotClientShutdownDataFilePath);
                    }

                    // Delete the existing "ShutdownData.yaml" file if it exists in League of Legends Data folder
                    string lolShutdownDataFilePath = Path.Combine(lolDataFolderPath, "ShutdownData.yaml");
                    if (File.Exists(lolShutdownDataFilePath))
                    {
                        File.Delete(lolShutdownDataFilePath);
                    }

                    // Source and destination file paths
                    string sourceFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\Accounts", $"{selectedAccount}.yaml");

                    // Copy the file to the Riot Client Data folder with the name "RiotGamesPrivateSettings.yaml"
                    File.Copy(sourceFilePath, Path.Combine(riotClientDataFolderPath, "RiotGamesPrivateSettings.yaml"), true);

                    MessageBox.Show("Account loaded successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete account button
        private void button3_Click(object sender, EventArgs e)
        {
            // Get the selected account name from the dropdown list
            string selectedAccount = comboBox1.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedAccount))
            {
                try
                {
                    // Source and destination file paths
                    string sourceFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\Accounts", $"{selectedAccount}.yaml");
                    string accountsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Riot Games\Riot Client\Data\Accounts");
                    string accountsFileName = Path.Combine(accountsFolderPath, "accounts.txt");

                    // Remove the selected account from the dropdown list
                    comboBox1.Items.Remove(selectedAccount);

                    // Remove the corresponding .yaml file from the "Accounts" directory
                    if (File.Exists(sourceFilePath))
                    {
                        File.Delete(sourceFilePath);
                    }

                    // Remove the account from the list in the accounts.txt file
                    if (File.Exists(accountsFileName))
                    {
                        List<string> accounts = File.ReadAllLines(accountsFileName).ToList();
                        accounts.Remove(selectedAccount);
                        File.WriteAllLines(accountsFileName, accounts);
                    }

                    MessageBox.Show("Account deleted successfully!");

                    // Update dropdown menu
                    PopulateAccountDropdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
