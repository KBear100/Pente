﻿using System;
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

namespace Pente
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        public void SingleplayerOnClick(object sender, RoutedEventArgs e)
        {
            Button playButton = (Button)FindName("Play_Btn");
            Label player1Label = (Label)FindName("Player1_Lbl");
            TextBox player1NameBox = (TextBox)FindName("Player1Name_TxtBox");
            playButton.Visibility = Visibility.Visible;
            player1Label.Visibility = Visibility.Visible;
            player1NameBox.Visibility = Visibility.Visible;
            Board.numPlayers = 1;
        }
        
        public void MultiplayerOnClick(object sender, RoutedEventArgs e)
        {
            Button playButton = (Button)FindName("Play_Btn");
            Label player1Label = (Label)FindName("Player1_Lbl");
            Label player2Label = (Label)FindName("Player2_Lbl");
            TextBox player1NameBox = (TextBox)FindName("Player1Name_TxtBox");
            TextBox player2NameBox = (TextBox)FindName("Player2Name_TxtBox");
            playButton.Visibility = Visibility.Visible;
            player1Label.Visibility = Visibility.Visible;
            player2Label.Visibility = Visibility.Visible;
            player1NameBox.Visibility = Visibility.Visible;
            player2NameBox.Visibility = Visibility.Visible;
            Board.numPlayers = 2;
        }

        public void PlayOnClick(object sender, RoutedEventArgs e)
        {
            TextBox player1NameBox = (TextBox)FindName("Player1Name_TxtBox");
            TextBox player2NameBox = (TextBox)FindName("Player2Name_TxtBox");

            Board.player1Name = player1NameBox.Text;

            if (Board.numPlayers == 2)
            {
                Board.player2Name = player2NameBox.Text;
            }
            else
            {
                Board.player2Name = "AI";
            }

            Window mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}