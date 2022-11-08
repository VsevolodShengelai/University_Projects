using System;
using System.Collections.Generic;
using System.Linq;
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

using MeasuringDevice;

namespace Exercise3TestHarness
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

        IMeasuringDevice device;
        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            switch (deviceBox.SelectionBoxItem.ToString())
            {
                case "Length Device":
                    if ((bool)metricChoice.IsChecked)
                    {
                        device = new MeasureLengthDevice(Units.Metric);
                    }
                    else
                    {
                        device = new MeasureLengthDevice(Units.Imperial);
                    }
                    break;
                case "Mass Device":
                    if ((bool)metricChoice.IsChecked)
                    {
                        device = new MeasureMassDevice(Units.Metric);
                    }
                    else
                    {
                        device = new MeasureMassDevice(Units.Imperial);
                    }
                    break;
            }            
        }

        private void metricValue_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
                metricValueBox.Text = device.MetricValue().ToString();
        }

        private void imperialValue_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
                imperialValueBox.Text = device.ImperialValue().ToString();
        }

        private void rawData_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                rawDataValues.ItemsSource = null;
                rawDataValues.ItemsSource = device.GetRawData();
            }
        }

        private void startCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
                device.StartCollecting();
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
                device.StopCollecting();
        }
    }
}
