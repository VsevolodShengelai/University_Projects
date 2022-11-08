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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Switch powerPlantSwitch = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void shutdownButton_Click(object sender, RoutedEventArgs _)
        {
            SuccessFailureResult disconnectStatus, rodInsertStatus;
            CoolantSystemStatus primaryCoolantStatus, backupCoolantStatus;
            double coreTemperatureBeforeShutdown, coreTemperatureAfterShutdown,
                radiationLevelAfterShutdown;
            outputTextBlock.Text = "";
            var nl = Environment.NewLine;
            var b = int.MaxValue;

            unchecked
            {
                int a = b + 10;
            }
            
            try
            {
                disconnectStatus = powerPlantSwitch.DisconnectPowerGenerator();
                outputTextBlock.Text += "Power generator disconnected status: " + disconnectStatus;
            }
            catch (PowerGeneratorCommsException e)
            {
                outputTextBlock.Text += "*** Exception in step 1: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                primaryCoolantStatus = powerPlantSwitch.VerifyPrimaryCoolantSystem();
                outputTextBlock.Text += "Primary coolant system status: " + primaryCoolantStatus;
            }
            catch (CoolantTemperatureReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 2: " + e.Message;
            }
            catch (CoolantPressureReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 2: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                backupCoolantStatus = powerPlantSwitch.VerifyBackupCoolantSystem();
                outputTextBlock.Text += "Backup coolant system status: " + backupCoolantStatus;
            }
            catch (CoolantTemperatureReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 3: " + e.Message;
            }
            catch (CoolantPressureReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 3: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                coreTemperatureBeforeShutdown = powerPlantSwitch.GetCoreTemperature();
                outputTextBlock.Text += "Core temperature before shutdown: " + coreTemperatureBeforeShutdown;
            }
            catch (CoreTemperatureReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 4: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                rodInsertStatus = powerPlantSwitch.InsertRodCluster();
                outputTextBlock.Text += "Rod insert status: " + rodInsertStatus;
            }
            catch (RodClusterReleaseException e)
            {
                outputTextBlock.Text += "*** Exception in step 5: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                coreTemperatureAfterShutdown = powerPlantSwitch.GetCoreTemperature();
                outputTextBlock.Text += "Core temperature after shutdown: " + coreTemperatureAfterShutdown;
            }
            catch (CoreTemperatureReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 6: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                radiationLevelAfterShutdown = powerPlantSwitch.GetRadiationLevel();
                outputTextBlock.Text += "Radiation level after shutdown: " + radiationLevelAfterShutdown;
            }
            catch (CoreRadiationLevelReadException e)
            {
                outputTextBlock.Text += "*** Exception in step 7: " + e.Message;
            }
            outputTextBlock.Text += nl;

            try
            {
                powerPlantSwitch.SignalShutdownComplete();
                outputTextBlock.Text += "SHUTDOWN SIGNAL COMPLETE";
            }
            catch (SignallingException e)
            {
                outputTextBlock.Text += "*** Exception in step 8: " + e.Message;
            }
            outputTextBlock.Text += nl;
        }
    }
}
