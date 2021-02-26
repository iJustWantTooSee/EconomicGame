using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game;

namespace FormsEconomicGame
{
    public partial class Form1 : Form
    {
        private GameEngine _game = null;
        private double _currentMoney = 2000;
        private double _currenCurrency = 0;
        private double _currentTime = 1;
        private bool _isClickPause = false;

        private double _startedCurrency;
        private int _amountDays = 1;
        private int _currentDay = 0;
        public Form1()
        {
            InitializeComponent();
            label9.Text = $"{_currentMoney}";
            label10.Text = $"{_currenCurrency}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            label7.Visible = true;
            BuyCurrency.Visible = true;
            SellCurrency.Visible = true;
            Finished();
            chart1.Series[0].Points.Clear();
            _amountDays = (int)StartNumbersDays.Value;
            _startedCurrency = (double)StartedCurrency.Value;
            _game = new GameEngine(_currentMoney, _currenCurrency, _startedCurrency);

            chart1.Series[0].Points.AddXY(_currentDay++, _startedCurrency);
            timer1.Start();
        }

        private void BuyCurrency_ValueChanged(object sender, EventArgs e)
        {
            if (!_game.IsBoughtCurrency((double)BuyCurrency.Value))
            {
                MessageBox.Show("У вас недостаточно денег!");
            }
            label3.Text = $"Current Day: {_currentDay - 1}";
            label8.Text = $"Time until the next day: {_currentTime++ % 12} of 12";
            label9.Text = $"{Math.Round(_game.GetMoney(), 5)}";
            label10.Text = $"{Math.Round(_game.GetCurrency(), 5)}";
            BuyCurrency.Value = 0;
        }

        private void SellCurrency_ValueChanged(object sender, EventArgs e)
        {
            if (!_game.IsSoldCurrency((double)SellCurrency.Value))
            {
                MessageBox.Show("У вас недостаточно валюты!");
            }
            label3.Text = $"Current Day: {_currentDay - 1}";
            label8.Text = $"Time until the next day: {_currentTime++ % 12} of 12";
            label9.Text = $"{Math.Round(_game.GetMoney(), 5)}";
            label10.Text = $"{Math.Round(_game.GetCurrency(), 5)}";
            SellCurrency.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = $"Current Day: {_currentDay-1}";
            label8.Text = $"Time until the next day: {_currentTime++ % 12} of 12";
            label9.Text = $"{Math.Round(_game.GetMoney(), 5)}";
            label10.Text = $"{Math.Round(_game.GetCurrency(), 5)}";

            if (_currentTime % 12 == 0)
            {
                chart1.Series[0].Points.AddXY(_currentDay++, Math.Round(_game.GetNewCurrencyExchangeRate(),5));
            }
            if (_currentDay > _amountDays)
            {
                timer1.Stop();
                Finished();
                MessageBox.Show($"Ваша прибыль: {_game.GetResultTheCalculateProfit()}");
            }
        }

        private void Finished()
        {
            _currentTime = 1;
            _amountDays = 1;
            _currentDay = 0;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (!_isClickPause)
            {
                _isClickPause = true;
                timer1.Stop();
                PauseButton.Text = "Continue";
            }
            else
            {
                _isClickPause = false;
                timer1.Start();
                PauseButton.Text = "Pause";
            }
        }
    }
}
