using System.Runtime.InteropServices;

public class RusRandomer {
    [DllImport("RusRandom", EntryPoint = "hello", CallingConvention = CallingConvention.Cdecl)]
    public static extern int hello(); // ����112

    /*  [DllImport("RusRandom", EntryPoint = "randNum", CallingConvention = CallingConvention.Cdecl)]*/
    /// <summary>
    /// ����һ���������
    /// </summary>
    /// <param name="left">������Сֵ</param>
    /// <param name="right">�������ֵ</param>
    /// <returns>���ɵ��������</returns>
    [DllImport("RusRandom")]
    public static extern int randNum(int left, int right);

}
