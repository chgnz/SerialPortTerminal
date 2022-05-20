namespace SerialTerminal
{
    class _lib
    {
        /// <summary>Compares if first x bytes are equal in two byte arrays</summary>
        /// <param name="arrayA">First byte array</param>
        /// <param name="arrayB">Second byte array</param>
        /// <param name="count">Count, how many bytes to compare</param>
        /// <return>true/false</return>
        public static bool compareByteArr(byte[] arrayA, byte[] arrayB, int count)
        {
            if (arrayA.Length < count || arrayB.Length < count)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                if (arrayA[i] != arrayB[i])
                    return false; // ja nav vienāds tads atgriežam nulli
            }
            return true;
        }

        /// <summary>Converts Hex string in to byte array</summary>
        /// <param name="hexString">HEX string. ex "010203040a0b"</param>
        /// <return>null/byte[]</return>
        public static byte[] convertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0 || hexString.Length == 0)
            {
                return null;
            }

            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexString.Length; index = index + 2)
            {
                string byteValue = hexString.Substring(index, 2);
                byte result;
                if (byte.TryParse(byteValue, System.Globalization.NumberStyles.HexNumber, null, out result))
                {
                    HexAsBytes[index / 2] = result;
                }
                else
                {
                    return null;
                }
            }
            return HexAsBytes;
        }
    }
}
