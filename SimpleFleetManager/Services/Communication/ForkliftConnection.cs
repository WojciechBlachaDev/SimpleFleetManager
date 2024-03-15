using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFleetManager.Services.Communication
{
    public class ForkliftConnection : IForkliftConnection
    {
        public ForkliftConnection() { }
        #region Ping test
        public static async Task<bool> IsForkliftAvaible(string ipAddress)
        {
            try
            {
                if (ipAddress != null)
                {
                    var ping = new Ping();
                    var reply = await ping.SendPingAsync(ipAddress);
                    return reply.Status == IPStatus.Success;
                }
                    
            }
            catch (PingException)
            {
                return false;
            }
            return false;
        }
        #endregion
        #region Connect task
        public async Task<bool> Connect(Forklift forklift)
        {
            try
            {
                if(forklift.ForkliftIpAddress != null)
                {
                    if (!await IsForkliftAvaible(forklift.ForkliftIpAddress))
                    {
                        return false;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                forklift.Client ??= new();
                var timeout = new CancellationTokenSource(5000);
                if (forklift.ForkliftIpAddress != null)
                {
                    await forklift.Client.ConnectAsync(forklift.ForkliftIpAddress, forklift.Port, timeout.Token);
                    if (forklift.Client.Connected)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (SocketException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        #endregion
        #region Reconnect task
        public async Task<bool> Reconnect(Forklift forklift, int retryInterval = 5000, int maxRetries = 5)
        {
            if (forklift.Client != null)
            {
                forklift.Client.Close();
                forklift.Client.Dispose();
            }
            forklift.Client = new();
            int retryCounter = 0;
            while (!forklift.Client.Connected && retryCounter < maxRetries)
            {
                try
                {
                    await Connect(forklift);
                    if (forklift.Client.Connected)
                    {
                        await Task.Run(() =>
                        {
                            Task dataExchange = HandleDataExchange(forklift);
                        });
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                await Task.Delay(retryInterval);
                retryCounter++;
            }
            if (retryCounter > maxRetries)
            {
                // Tu dodac informacje o przekroczeniu licznika prob polaczeniowych
            }
            return false;
        }
        #endregion
        #region Disconnect task
        public Task<bool> Disconnect(Forklift forklift)
        {
            try
            {
                if (forklift.Client != null)
                {
                    if (forklift.Client.Connected)
                    {
                        forklift.Client.Close();
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
                return Task.FromResult(false);
            }
            catch (Exception) 
            {
                return Task.FromResult(false);
            }
        }
        #endregion
        #region Data exchange task
        public async Task HandleDataExchange(Forklift forklift)
        {
            if (forklift.Client != null && forklift.Client.Connected)
            {
                NetworkStream stream = forklift.Client.GetStream();
                byte[] buffer = new byte[65535];
                int bytesReaded;
                try
                {
                    while((bytesReaded = await stream.ReadAsync(buffer)) != 0)
                    {
                        #region Read data from connected forklift
                        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesReaded);
                        dataReceived = dataReceived.Replace("$", "");
                        var endMessageFilter = new List<string>(dataReceived.Split('!'));
                        var forkliftData = new List<string>(endMessageFilter[0].Split('&'));
                        #endregion
                        #region Readed data spliting into sections and assigning to Forklift variables
                        forklift.DataOut ??= new();
                        if (forkliftData != null && forklift.DataIn != null)
                        {
                            #region Data split into thematic strings
                            string poseDataStrig = string.Empty;
                            string sensorDataString = string.Empty;
                            string encoderDataString = string.Empty;
                            string plcErrorStatusString = string.Empty;
                            string plcErrorCodesString = string.Empty;
                            string safetyDataString = string.Empty;
                            string scangridStatusString = string.Empty;
                            string scangridMeasuringString = string.Empty;
                            string actualWorkstatesString = string.Empty;
                            string ethernetStatusString = string.Empty;
                            string actualTebConfigString = string.Empty;
                            string actualTaskString = string.Empty;
                            if (forkliftData[0] != null)
                            {
                                poseDataStrig = forkliftData[0];
                            }
                            if (forkliftData[1] != null)
                            {
                                sensorDataString = forkliftData[1];
                            }
                            if (forkliftData[2] != null)
                            {
                                encoderDataString = forkliftData[2];
                            }
                            if (forkliftData[3] != null)
                            {
                                plcErrorStatusString = forkliftData[3];
                            }
                            if (forkliftData[4] != null)
                            {
                                plcErrorCodesString = forkliftData[4];
                            }
                            if (forkliftData[5] != null)
                            {
                                safetyDataString = forkliftData[5];
                            }
                            if (forkliftData[6] != null)
                            {
                                scangridStatusString = forkliftData[6];
                            }
                            if (forkliftData[7] != null)
                            {
                                scangridMeasuringString = forkliftData[7];
                            }
                            if (forkliftData[8] != null)
                            {
                                actualWorkstatesString = forkliftData[8];
                            }
                            if (forkliftData[9] != null)
                            {
                                ethernetStatusString = forkliftData[9];
                            }
                            if (forkliftData[10] != null)
                            {
                                actualTebConfigString = forkliftData[10];
                            }
                            if (forkliftData[11] != null)
                            {
                                actualTaskString = forkliftData[11];
                            }
                            #endregion
                            #region Creating tables from strings above
                            var poseData = new List<string>(poseDataStrig.Split('#'));
                            var sensorsData = new List<string>(sensorDataString.Split('#'));
                            var encoderData = new List<string>(encoderDataString.Split('#'));
                            var plcErorrStatus = new List<string>(plcErrorStatusString.Split('#'));
                            var plcErrorCodes = new List<string>(plcErrorCodesString.Split('#'));
                            var safetyData = new List<string>(safetyDataString.Split('#'));
                            var scangridStatus = new List<string>(scangridStatusString.Split('#'));
                            var scangridMeasuring = new List<string>(scangridMeasuringString.Split('#'));
                            var actualWorkstates = new List<string>(actualWorkstatesString.Split('#'));
                            var ethernetStatus = new List<string>(ethernetStatusString.Split('#'));
                            var actualTebConfig = new List<string>(actualTebConfigString.Split('#'));
                            var actualTask = new List<string>(actualTaskString.Split('#'));
                            #endregion
                            #region Pose data assign to forklift
                            forklift.DataOut.ActualPosition ??= new();
                            if (poseData.Count == 3)
                            {
                                forklift.DataOut.ActualPosition.X = Convert.ToDouble(poseData[0]);
                                forklift.DataOut.ActualPosition.Y = Convert.ToDouble(poseData[1]);
                                forklift.DataOut.ActualPosition.R = Convert.ToDouble(poseData[2]);
                            }
                            #endregion
                            #region Sensor data assign to forklift
                            forklift.DataOut.Sensors ??= new();
                            if (sensorsData.Count > 10)
                            {
                                sensorsData.RemoveAt(sensorsData.Count - 1);
                            }
                            if (sensorsData.Count == 10)
                            {
                                forklift.DataOut.Sensors.BatteryVoltage = Convert.ToDouble(sensorsData[0]);
                                forklift.DataOut.Sensors.BatteryPercentage = Convert.ToDouble(sensorsData[1]);
                                forklift.DataOut.Sensors.ChargingNeed = Convert.ToBoolean(sensorsData[2]);
                                forklift.DataOut.Sensors.ForksHeight = Convert.ToDouble(sensorsData[3]);
                                forklift.DataOut.Sensors.ForksHeightLimiter = Convert.ToBoolean(sensorsData[4]);
                                forklift.DataOut.Sensors.Weight = Convert.ToDouble(sensorsData[5]);
                                forklift.DataOut.Sensors.WeightSaved = Convert.ToDouble(sensorsData[6]);
                                forklift.DataOut.Sensors.TiltAxis1 = Convert.ToDouble(sensorsData[7]);
                                forklift.DataOut.Sensors.TiltAxis2 = Convert.ToDouble(sensorsData[8]);
                                forklift.DataOut.Sensors.ServoAngle = Convert.ToDouble(sensorsData[9]);
                            }
                            #endregion
                            #region Encoder data assign to forklift
                            forklift.DataOut.Encoder ??= new();
                            if (encoderData.Count > 8)
                            {
                                encoderData.RemoveAt(0);
                                encoderData.RemoveAt(encoderData.Count - 1);
                            }
                            if (encoderData.Count == 8)
                            {
                                forklift.DataOut.Encoder.Speed = Convert.ToDouble(encoderData[0]);
                                forklift.DataOut.Encoder.IsMovingForward = Convert.ToBoolean(encoderData[1]);
                                forklift.DataOut.Encoder.IsMovingBackward = Convert.ToBoolean(encoderData[2]);
                                forklift.DataOut.Encoder.IsStandstill = Convert.ToBoolean(encoderData[3]);
                                forklift.DataOut.Encoder.CurrentPosition = Convert.ToInt32(encoderData[4]);
                            }
                            #endregion
                            #region PLC error statuses assign to forklift
                            forklift.DataOut.PlcErrorStatus ??= new();
                            if (plcErorrStatus.Count > 12)
                            {
                                plcErorrStatus.RemoveAt(0);
                                plcErorrStatus.RemoveAt(plcErorrStatus.Count - 1);
                            }
                            if (plcErorrStatus.Count >= 12)
                            {
                                forklift.DataOut.PlcErrorStatus.BatteryVoltageRead = Convert.ToBoolean(plcErorrStatus[0]);
                                forklift.DataOut.PlcErrorStatus.ForksHeightSensor = Convert.ToBoolean(plcErorrStatus[1]);
                                forklift.DataOut.PlcErrorStatus.ManualModeSpeedRegulator = Convert.ToBoolean(plcErorrStatus[2]);
                                forklift.DataOut.PlcErrorStatus.PressureSensor = Convert.ToBoolean(plcErorrStatus[3]);
                                forklift.DataOut.PlcErrorStatus.PowerToCurtisWrite = Convert.ToBoolean(plcErorrStatus[4]);
                                forklift.DataOut.PlcErrorStatus.ScangridLeftReadMeasurement = Convert.ToBoolean(plcErorrStatus[5]);
                                forklift.DataOut.PlcErrorStatus.ScangridRightReadMeasurement = Convert.ToBoolean(plcErorrStatus[6]);
                                forklift.DataOut.PlcErrorStatus.ServoHalt = Convert.ToBoolean(plcErorrStatus[7]);
                                forklift.DataOut.PlcErrorStatus.ServoMove = Convert.ToBoolean(plcErorrStatus[8]);
                                forklift.DataOut.PlcErrorStatus.ServoPositionRead = Convert.ToBoolean(plcErorrStatus[9]);
                                forklift.DataOut.PlcErrorStatus.TiltSensorAxis1 = Convert.ToBoolean(plcErorrStatus[10]);
                                forklift.DataOut.PlcErrorStatus.TiltSensorAxis2 = Convert.ToBoolean(plcErorrStatus[11]);
                            }
                            #endregion
                            #region PLC error codes assign to forklift
                            forklift.DataOut.PlcErrorCodes ??= new();
                            if (plcErrorCodes.Count > 12)
                            {
                                plcErrorCodes.RemoveAt(0);
                                plcErrorCodes.RemoveAt(plcErrorCodes.Count - 1);
                            }
                            if (plcErrorCodes.Count >= 12)
                            {
                                forklift.DataOut.PlcErrorCodes.BatteryVoltageRead = Convert.ToInt32(plcErrorCodes[0]);
                                forklift.DataOut.PlcErrorCodes.ForksHeightSensor = Convert.ToInt32(plcErrorCodes[1]);
                                forklift.DataOut.PlcErrorCodes.ManualModeSpeedRegulator = Convert.ToInt32(plcErrorCodes[2]);
                                forklift.DataOut.PlcErrorCodes.PressureSensor = Convert.ToInt32(plcErrorCodes[3]);
                                forklift.DataOut.PlcErrorCodes.PowerToCurtisWrite = Convert.ToInt32(plcErrorCodes[4]);
                                forklift.DataOut.PlcErrorCodes.ScangridLeftReadMeasurement = Convert.ToInt32(plcErrorCodes[5]);
                                forklift.DataOut.PlcErrorCodes.ScangridRightReadMeasurement = Convert.ToInt32(plcErrorCodes[6]);
                                forklift.DataOut.PlcErrorCodes.ServoHalt = Convert.ToInt32(plcErrorCodes[7]);
                                forklift.DataOut.PlcErrorCodes.ServoMove = Convert.ToInt32(plcErrorCodes[8]);
                                forklift.DataOut.PlcErrorCodes.ServoPositionRead = Convert.ToInt32(plcErrorCodes[9]);
                                forklift.DataOut.PlcErrorCodes.TiltSensorAxis1 = Convert.ToInt32(plcErrorCodes[10]);
                                forklift.DataOut.PlcErrorCodes.TiltSensorAxis2 = Convert.ToInt32(plcErrorCodes[11]);
                            }
                            #endregion
                            #region Safety data ssign to forklift
                            forklift.DataOut.Safety ??= new();
                            forklift.DataOut.Safety.LidarLeft ??= new();
                            forklift.DataOut.Safety.LidarRight ??= new();
                            if (safetyData.Count > 24)
                            {
                                safetyData.RemoveAt(0);
                                safetyData.RemoveAt(safetyData.Count - 1);
                            }
                            if (safetyData.Count == 24)
                            {
                                forklift.DataOut.Safety.CpuOk = Convert.ToBoolean(safetyData[0]);
                                forklift.DataOut.Safety.EncoderOk = Convert.ToBoolean(safetyData[1]);
                                forklift.DataOut.Safety.SafeSpeed = Convert.ToBoolean(safetyData[2]);
                                forklift.DataOut.Safety.Standstill = Convert.ToBoolean(safetyData[3]);
                                forklift.DataOut.Safety.ButtonemergencyLeft = Convert.ToBoolean(safetyData[4]);
                                forklift.DataOut.Safety.ButtonemergencyRight = Convert.ToBoolean(safetyData[5]);
                                forklift.DataOut.Safety.LidarLeft.IsActive = Convert.ToBoolean(safetyData[6]);
                                forklift.DataOut.Safety.LidarLeft.DeviceError = Convert.ToBoolean(safetyData[7]);
                                forklift.DataOut.Safety.LidarLeft.AppError = Convert.ToBoolean(safetyData[8]);
                                forklift.DataOut.Safety.LidarLeft.ContaminationError = Convert.ToBoolean(safetyData[9]);
                                forklift.DataOut.Safety.LidarLeft.ContaminationWarning = Convert.ToBoolean(safetyData[10]);
                                forklift.DataOut.Safety.LidarLeft.SwitchMonitoringCaseError = Convert.ToBoolean(safetyData[11]);
                                forklift.DataOut.Safety.LidarLeft.EmergencyStopZoneStatus = Convert.ToBoolean(safetyData[12]);
                                forklift.DataOut.Safety.LidarLeft.SoftStopZoneStatus = Convert.ToBoolean(safetyData[13]);
                                forklift.DataOut.Safety.LidarLeft.ReducedSpeedZoneStatus = Convert.ToBoolean(safetyData[14]);
                                forklift.DataOut.Safety.LidarRight.IsActive = Convert.ToBoolean(safetyData[15]);
                                forklift.DataOut.Safety.LidarRight.DeviceError = Convert.ToBoolean(safetyData[16]);
                                forklift.DataOut.Safety.LidarRight.AppError = Convert.ToBoolean(safetyData[17]);
                                forklift.DataOut.Safety.LidarRight.ContaminationError = Convert.ToBoolean(safetyData[18]);
                                forklift.DataOut.Safety.LidarRight.ContaminationWarning = Convert.ToBoolean(safetyData[19]);
                                forklift.DataOut.Safety.LidarRight.SwitchMonitoringCaseError = Convert.ToBoolean(safetyData[20]);
                                forklift.DataOut.Safety.LidarRight.EmergencyStopZoneStatus = Convert.ToBoolean(safetyData[21]);
                                forklift.DataOut.Safety.LidarRight.SoftStopZoneStatus = Convert.ToBoolean(safetyData[22]);
                                forklift.DataOut.Safety.LidarRight.ReducedSpeedZoneStatus = Convert.ToBoolean(safetyData[23]);
                            }
                            #endregion
                            #region Scangrid statuses assign to forklift
                            forklift.DataOut.ScangridRight ??= new();
                            forklift.DataOut.ScangridRight.Status ??= new();
                            forklift.DataOut.ScangridLeft ??= new();
                            forklift.DataOut.ScangridLeft.Status ??= new();
                            if (scangridStatus.Count > 22)
                            {
                                scangridStatus.RemoveAt(0);
                                scangridStatus.RemoveAt(scangridStatus.Count - 1);
                            }
                            if (scangridStatus.Count == 22)
                            {
                                forklift.DataOut.ScangridLeft.Status.WorkStatus = Convert.ToBoolean(scangridStatus[0]);
                                forklift.DataOut.ScangridLeft.Status.VoltageError = Convert.ToBoolean(scangridStatus[1]);
                                forklift.DataOut.ScangridLeft.Status.ExternalLightResistanceError = Convert.ToBoolean(scangridStatus[2]);
                                forklift.DataOut.ScangridLeft.Status.ContaminationError = Convert.ToBoolean(scangridStatus[3]);
                                forklift.DataOut.ScangridLeft.Status.ContaminationWarning = Convert.ToBoolean(scangridStatus[4]);
                                forklift.DataOut.ScangridLeft.Status.SleeepModeStatus = Convert.ToBoolean(scangridStatus[5]);
                                forklift.DataOut.ScangridLeft.Status.MonitoringCaseSwitchInputValid = Convert.ToBoolean(scangridStatus[6]);
                                forklift.DataOut.ScangridLeft.Status.MonitoringCaseSwitchCanValid = Convert.ToBoolean(scangridStatus[7]);
                                forklift.DataOut.ScangridLeft.Status.SafetyOutputSignal = Convert.ToBoolean(scangridStatus[8]);
                                forklift.DataOut.ScangridLeft.Status.ProtectionFieldStatus = Convert.ToBoolean(scangridStatus[9]);
                                forklift.DataOut.ScangridLeft.Status.WarningFieldStatus = Convert.ToBoolean(scangridStatus[10]);
                                forklift.DataOut.ScangridRight.Status.WorkStatus = Convert.ToBoolean(scangridStatus[11]);
                                forklift.DataOut.ScangridRight.Status.VoltageError = Convert.ToBoolean(scangridStatus[12]);
                                forklift.DataOut.ScangridRight.Status.ExternalLightResistanceError = Convert.ToBoolean(scangridStatus[13]);
                                forklift.DataOut.ScangridRight.Status.ContaminationError = Convert.ToBoolean(scangridStatus[14]);
                                forklift.DataOut.ScangridRight.Status.ContaminationWarning = Convert.ToBoolean(scangridStatus[15]);
                                forklift.DataOut.ScangridRight.Status.SleeepModeStatus = Convert.ToBoolean(scangridStatus[16]);
                                forklift.DataOut.ScangridRight.Status.MonitoringCaseSwitchInputValid = Convert.ToBoolean(scangridStatus[17]);
                                forklift.DataOut.ScangridRight.Status.MonitoringCaseSwitchCanValid = Convert.ToBoolean(scangridStatus[18]);
                                forklift.DataOut.ScangridRight.Status.SafetyOutputSignal = Convert.ToBoolean(scangridStatus[19]);
                                forklift.DataOut.ScangridRight.Status.ProtectionFieldStatus = Convert.ToBoolean(scangridStatus[20]);
                                forklift.DataOut.ScangridRight.Status.WarningFieldStatus = Convert.ToBoolean(scangridStatus[21]);
                            }
                            #endregion
                            #region Scangrid measuring data assign to forklift
                            forklift.DataOut.ScangridRight.Ranges ??= [];
                            forklift.DataOut.ScangridLeft.Ranges ??= [];
                            if (scangridMeasuring.Count > 64)
                            {
                                scangridMeasuring.RemoveAt(0);
                                scangridMeasuring.RemoveAt(scangridMeasuring.Count - 1);
                            }
                            if (scangridMeasuring.Count == 64)
                            {
                                for (int i = 0; i < 32 ; i++)
                                {
                                    forklift.DataOut.ScangridLeft.Ranges[i] = Convert.ToInt32(scangridMeasuring[i]);
                                }
                                for (int i = 32;i < 64 ; i++)
                                {
                                    forklift.DataOut.ScangridRight.Ranges[i - 32] = Convert.ToInt32(scangridMeasuring[i]);
                                }
                            }
                            #endregion
                            #region Actual workstates assign to forklift
                            forklift.DataOut.ActualWorkstates ??= new();
                            if (actualWorkstates.Count > 14)
                            {
                                actualWorkstates.RemoveAt(0);
                                actualWorkstates.RemoveAt(actualWorkstates.Count - 1);
                            }
                            if (actualWorkstates.Count == 14)
                            {
                                forklift.DataOut.ActualWorkstates.S01 = Convert.ToBoolean(actualWorkstates[0]);
                                forklift.DataOut.ActualWorkstates.S02 = Convert.ToBoolean(actualWorkstates[1]);
                                forklift.DataOut.ActualWorkstates.S03 = Convert.ToBoolean(actualWorkstates[2]);
                                forklift.DataOut.ActualWorkstates.S1 = Convert.ToBoolean(actualWorkstates[3]);
                                forklift.DataOut.ActualWorkstates.S2 = Convert.ToBoolean(actualWorkstates[4]);
                                forklift.DataOut.ActualWorkstates.S3 = Convert.ToBoolean(actualWorkstates[5]);
                                forklift.DataOut.ActualWorkstates.S4 = Convert.ToBoolean(actualWorkstates[6]);
                                forklift.DataOut.ActualWorkstates.S40 = Convert.ToBoolean(actualWorkstates[7]);
                                forklift.DataOut.ActualWorkstates.S41 = Convert.ToBoolean(actualWorkstates[8]);
                                forklift.DataOut.ActualWorkstates.S42 = Convert.ToBoolean(actualWorkstates[9]);
                                forklift.DataOut.ActualWorkstates.S43 = Convert.ToBoolean(actualWorkstates[10]);
                                forklift.DataOut.ActualWorkstates.S44 = Convert.ToBoolean(actualWorkstates[11]);
                                forklift.DataOut.ActualWorkstates.S45 = Convert.ToBoolean(actualWorkstates[12]);
                                forklift.DataOut.ActualWorkstates.S46 = Convert.ToBoolean(actualWorkstates[13]);
                            }
                            #endregion
                            #region Ethernet test statuses assign to forklift
                            forklift.DataOut.Ethernet ??= new();
                            if (ethernetStatus.Count > 10)
                            {
                                ethernetStatus.RemoveAt(0);
                                ethernetStatus.RemoveAt(ethernetStatus.Count - 1);
                            }
                            if (ethernetStatus.Count == 10) 
                            {
                                forklift.DataOut.Ethernet.LidarLoc = Convert.ToBoolean(ethernetStatus[0]);
                                forklift.DataOut.Ethernet.Plc = Convert.ToBoolean(ethernetStatus[1]);
                                forklift.DataOut.Ethernet.Visionary = Convert.ToBoolean(ethernetStatus[2]);
                                forklift.DataOut.Ethernet.SafetyModbus = Convert.ToBoolean(ethernetStatus[3]);
                                forklift.DataOut.Ethernet.GatewayLan = Convert.ToBoolean(ethernetStatus[4]);
                                forklift.DataOut.Ethernet.LidarLeft = Convert.ToBoolean(ethernetStatus[5]);
                                forklift.DataOut.Ethernet.LidarRight = Convert.ToBoolean(ethernetStatus[6]);
                                forklift.DataOut.Ethernet.SafetyEthernet = Convert.ToBoolean(ethernetStatus[7]);
                                forklift.DataOut.Ethernet.GatewayWiFi = Convert.ToBoolean(ethernetStatus[8]);
                                forklift.DataOut.Ethernet.Server = Convert.ToBoolean(ethernetStatus[9]);
                            }
                            #endregion
                            #region Actual teb config assign to Forklift
                            forklift.DataOut.ActualTebConfig ??= new();
                            if (actualTebConfig.Count > 19)
                            {
                                actualTebConfig.RemoveAt(0);
                                actualTebConfig.RemoveAt(actualTebConfig.Count - 1);
                            }
                            if (actualTebConfig.Count == 19)
                            {
                                forklift.DataOut.ActualTebConfig.MaxVelForward = Convert.ToDouble(actualTebConfig[0]);
                                forklift.DataOut.ActualTebConfig.MaxVelBackward = Convert.ToDouble(actualTebConfig[1]);
                                forklift.DataOut.ActualTebConfig.MaxVelAngular = Convert.ToDouble(actualTebConfig[2]);
                                forklift.DataOut.ActualTebConfig.MaxAccForwardBackward = Convert.ToDouble(actualTebConfig[3]);
                                forklift.DataOut.ActualTebConfig.MaxAccAngular = Convert.ToDouble(actualTebConfig[4]);
                                forklift.DataOut.ActualTebConfig.TurningRadius = Convert.ToDouble(actualTebConfig[5]);
                                forklift.DataOut.ActualTebConfig.Wheelbase = Convert.ToDouble(actualTebConfig[6]);
                                forklift.DataOut.ActualTebConfig.GoalLinearTolerance = Convert.ToDouble(actualTebConfig[7]);
                                forklift.DataOut.ActualTebConfig.GoalAngularTolerance = Convert.ToDouble(actualTebConfig[8]);
                                forklift.DataOut.ActualTebConfig.MinObstacleDistance = Convert.ToDouble(actualTebConfig[9]);
                                forklift.DataOut.ActualTebConfig.ObstacleInflationRadius = Convert.ToDouble(actualTebConfig[10]);
                                forklift.DataOut.ActualTebConfig.DynamicObstacleInflationRadius = Convert.ToDouble(actualTebConfig[11]);
                                forklift.DataOut.ActualTebConfig.DtRef = Convert.ToDouble(actualTebConfig[12]);
                                forklift.DataOut.ActualTebConfig.DtHysteresis = Convert.ToDouble(actualTebConfig[13]);
                                forklift.DataOut.ActualTebConfig.IncludeDynamicObstacles = Convert.ToBoolean(actualTebConfig[14]);
                                forklift.DataOut.ActualTebConfig.IncludeCostmapObstacles = Convert.ToBoolean(actualTebConfig[15]);
                                forklift.DataOut.ActualTebConfig.OscillationRecovery = Convert.ToBoolean(actualTebConfig[16]);
                                forklift.DataOut.ActualTebConfig.AllowInitWithBackwardMotion = Convert.ToBoolean(actualTebConfig[17]);
                                forklift.DataOut.ActualTebConfig.Save = Convert.ToBoolean(actualTebConfig[18]);
                            }
                            #endregion




                        }
                        #endregion
                    }
                }
                catch (Exception)
                {
                    await Reconnect(forklift);
                }
            }
            else
            {
                await Reconnect(forklift);
            }
        }
        #endregion
    }
}
