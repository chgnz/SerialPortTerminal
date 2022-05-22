using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialTerminal
{
    public interface ISerialPortView
    {
        string SerialPortStatusDisplay { get; set; }

        void AppendSerialData(string data);

        event Action<string> OnSerialDataTransmit;

        event EventHandler OnOpenCloseButtonPressed;
        event Action<string> OnSerialPortNameChanged;
        event Action<string> OnSerialBaudrateChanged;
        event Action<string> OnLineEndingsChanged;
        event Action<string, int> OnPeriodicDataToggle;
        event Action<int> OnUsbDeviceChange;

    }
}
