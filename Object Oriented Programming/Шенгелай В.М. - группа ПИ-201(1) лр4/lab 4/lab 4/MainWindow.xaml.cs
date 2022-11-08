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

namespace TestClient
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

        /// <summary>
        /// Initiate the reactor shutdown using a Switch object
        /// Record details of shutdown status in a TextBlock - recording all exceptions thrown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {

            this.textBlock1.Text = "Initiating test sequence: " + DateTime.Now.ToLongTimeString();
            SwitchDevices.Switch sd = new SwitchDevices.Switch();

            SwitchDevices.SuccessFailureResult powerGeneratorSuccessFailureResult;
            SwitchDevices.CoolantSystemStatus primaryCoolantSystemStatus, backupCoolantSystemStatus;
            double coreTemaperatureBeforeShutdown, coreTemaperatureAfterShutdown, coreRadiationAfterShutdown;
            SwitchDevices.SuccessFailureResult rodClusterSuccessFailureResult;


            // Step 1 - disconnect from the Power Generator

            try
            {
                powerGeneratorSuccessFailureResult = sd.DisconnectPowerGenerator();
                if (powerGeneratorSuccessFailureResult == SwitchDevices.SuccessFailureResult.Success)
                {
                    this.textBlock1.Text += "\nStep 1: Successfully disconnected power generation system";
                }
                if (powerGeneratorSuccessFailureResult == SwitchDevices.SuccessFailureResult.Fail)
                {
                    this.textBlock1.Text += "\nStep 1: Failed to disconnect power generation system";
                }
            }
            catch (SwitchDevices.PowerGeneratorCommsException M)
            {
                this.textBlock1.Text += "\nStep 1: Failed to disconnect power generation system";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }


            // Step 2 - Verify the status of the Primary Coolant System
            try
            {
                primaryCoolantSystemStatus = sd.VerifyPrimaryCoolantSystem();
                if (primaryCoolantSystemStatus == SwitchDevices.CoolantSystemStatus.OK)
                {
                    this.textBlock1.Text += "\nStep 2: Primary coolant system OK";
                }
                if (primaryCoolantSystemStatus == SwitchDevices.CoolantSystemStatus.Check)
                {
                    this.textBlock1.Text += "\nStep 2: Primary coolant system requires manual check";
                }
                if (primaryCoolantSystemStatus == SwitchDevices.CoolantSystemStatus.Fail)
                {
                    this.textBlock1.Text += "\nStep 2: Problem reported with primary coolant system";
                }
            }
            catch (SwitchDevices.CoolantTemperatureReadException M)
            {
                this.textBlock1.Text += "\nStep 2: Problem reported with primary coolant system";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }
            catch (SwitchDevices.CoolantPressureReadException M)
            {
                this.textBlock1.Text += "\nStep 2: Problem reported with primary coolant system";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }


            // Step 3 - Verify the status of the Backup Coolant System
            try
            {
                backupCoolantSystemStatus = sd.VerifyBackupCoolantSystem();
                if (backupCoolantSystemStatus == SwitchDevices.CoolantSystemStatus.OK)
                {
                    this.textBlock1.Text += "\nStep 3: Backup coolant system OK";
                }
                if (backupCoolantSystemStatus == SwitchDevices.CoolantSystemStatus.Check)
                {
                    this.textBlock1.Text += "\nStep 3: Backup coolant system requires manual check";
                }
                if (backupCoolantSystemStatus == SwitchDevices.CoolantSystemStatus.Fail)
                {
                    this.textBlock1.Text += "\nStep 3: Backup reported with primary coolant system";
                }
            }
            catch (SwitchDevices.CoolantTemperatureReadException M)
            {
                this.textBlock1.Text += "\nStep 3: Backup reported with primary coolant system";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }
            catch (SwitchDevices.CoolantPressureReadException M)
            {
                this.textBlock1.Text += "\nStep 3: Backup reported with primary coolant system";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }


            // Step 4 - Record the core temperature prior to shutting down the reactor
            try
            {
                coreTemaperatureBeforeShutdown = sd.GetCoreTemperature();
                this.textBlock1.Text += "\nStep 4: Core temperature before shutdown: " + coreTemaperatureBeforeShutdown;
            }
            catch (SwitchDevices.CoreTemperatureReadException M)
            {
                this.textBlock1.Text += "\nStep 4: Failed to read core temperature before shutdown";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }


            // Step 5 - Insert the control rods into the reactor
            try
            {
                rodClusterSuccessFailureResult = sd.InsertRodCluster();
                if (rodClusterSuccessFailureResult == SwitchDevices.SuccessFailureResult.Success)
                {
                    this.textBlock1.Text += "\nStep 5: Control rods successfully inserted";
                }
                if (rodClusterSuccessFailureResult == SwitchDevices.SuccessFailureResult.Fail)
                {
                    this.textBlock1.Text += "\nStep 5: Control rod insertion failed";
                }
            }
            catch (SwitchDevices.RodClusterReleaseException  M)
            {
                this.textBlock1.Text += "\nStep 5: Control rod insertion failed";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }



            // Step 6 - Record the core temperature after shutting down the reactor
            try
            {
                coreTemaperatureAfterShutdown = sd.GetCoreTemperature();
                this.textBlock1.Text += "\nStep 6: Core temperature after shutdown: " + coreTemaperatureAfterShutdown;
            }
            catch (SwitchDevices.CoreTemperatureReadException M)
            {
                this.textBlock1.Text += "\nStep 6: Failed to read core temperature after shutdown";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }


            // Step 7 - Record the core radiation levels after shutting down the reactor
            try
            {
                coreRadiationAfterShutdown = sd.GetRadiationLevel();
                this.textBlock1.Text += "\nStep 7: Core radiation level after shutdown: " + coreRadiationAfterShutdown;
            }
            catch (SwitchDevices.CoreRadiationLevelReadException M)
            {
                this.textBlock1.Text += "\nStep 6: Failed to read core radiation level after shutdown";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }


            // Step 8 - Broadcast "Shutdown Complete" message
            try
            {
                sd.SignalShutdownComplete();
                this.textBlock1.Text += "\nStep 8: Broadcasting shutdown complete message";
            }
            catch (SwitchDevices.SignallingException M)
            {
                this.textBlock1.Text += "\nStep 8: Fail of broadcasting shutdown complete message";
                this.textBlock1.Text += "\nERROR CODE: " + M.Message + '\n';
            }            
            
            this.textBlock1.Text += "\nTest sequence complete: " + DateTime.Now.ToLongTimeString();
        }
    }
}
