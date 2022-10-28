using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


/*
    #アプリの仕様
    実行すると、現在の時刻と電池残量を、exeと同フォルダ内の{デバイス名}.csvに追記する。
    たとえば、スタートアップにこのプログラムを指定すれば、毎起動時に電池残量が記録される。

    #今後の考慮
    書き込み先の検討(一括管理するなら、サーバー上DBに書けると良いね)
 */

namespace batterylogger
{
    class Program
    {
        static void Main(string[] args)
        {
            //マシン名
            Console.WriteLine("MachineName: {0}", Environment.MachineName);

            //時刻
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            //バッテリー残量（割合）
            float blp = SystemInformation.PowerStatus.BatteryLifePercent;
            Console.WriteLine("バッテリー残量は、{0}%です", blp * 100);

            OutputLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "," + (blp * 100).ToString());

            //Console.ReadLine();

        }

        static void OutputLog(string log)
        {
            //書き込み場所
            //ネットワーク上ならネットワークの確認
            //同一ファイルに全員が書き込むなら少し考慮が必要
            //今は{マシン名}+.csv
            var fileName = Environment.MachineName+".csv";

            try
            {
                //ファイルをオープンする
                using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    sw.WriteLine(log);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
