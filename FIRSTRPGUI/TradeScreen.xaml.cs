﻿using GameEngine.GameModels;
using GameEngine.ViewModels;
using System.Windows;

namespace FIRSTRPGUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;

        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem =
                ((FrameworkElement)sender).DataContext as GroupedInventoryItem;

            if (groupedInventoryItem != null)
            {
                Session.CurrentPlayer.ReceiveGold(groupedInventoryItem.Item.SellPrice);
                Session.CurrentTrader.AddItemToInventory(groupedInventoryItem.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(groupedInventoryItem.Item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedInventoryItem =
                ((FrameworkElement)sender).DataContext as GroupedInventoryItem;

            if (groupedInventoryItem != null)
            {
                if (Session.CurrentPlayer.Gold >= groupedInventoryItem.Item.BuyPrice)
                {
                    Session.CurrentPlayer.SpendGold(groupedInventoryItem.Item.BuyPrice);
                    Session.CurrentTrader.RemoveItemFromInventory(groupedInventoryItem.Item);
                    Session.CurrentPlayer.AddItemToInventory(groupedInventoryItem.Item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}