using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UACS
{
    public enum EDate
    {
        無し = -1,
        ジュニア7月後半,
        ジュニア8月前半,
        ジュニア8月後半,
        ジュニア9月前半,
        ジュニア9月後半,
        ジュニア10月前半,
        ジュニア10月後半,
        ジュニア11月前半,
        ジュニア11月後半,
        ジュニア12月前半,
        ジュニア12月後半,
        クラシック1月前半,
        クラシック1月後半,
        クラシック2月前半,
        クラシック2月後半,
        クラシック3月前半,
        クラシック3月後半,
        クラシック4月前半,
        クラシック4月後半,
        クラシック5月前半,
        クラシック5月後半,
        クラシック6月前半,
        クラシック6月後半,
        クラシック7月前半,
        クラシック7月後半,
        クラシック8月前半,
        クラシック8月後半,
        クラシック9月前半,
        クラシック9月後半,
        クラシック10月前半,
        クラシック10月後半,
        クラシック11月前半,
        クラシック11月後半,
        クラシック12月前半,
        クラシック12月後半,
        シニア1月前半,
        シニア1月後半,
        シニア2月前半,
        シニア2月後半,
        シニア3月前半,
        シニア3月後半,
        シニア4月前半,
        シニア4月後半,
        シニア5月前半,
        シニア5月後半,
        シニア6月前半,
        シニア6月後半,
        シニア7月前半,
        シニア7月後半,
        シニア8月前半,
        シニア8月後半,
        シニア9月前半,
        シニア9月後半,
        シニア10月前半,
        シニア10月後半,
        シニア11月前半,
        シニア11月後半,
        シニア12月前半,
        シニア12月後半,
    }

    public enum EAppropriate
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
    }

    public enum ERaceCount
    {
        two,
        three,
        four,
        無制限,
    }

    class Race
    {
        /// <summary>
        /// レース名
        /// </summary>
        public string name { get; }
        /// <summary>
        /// バ場
        /// </summary>
        public string field { get; }
        /// <summary>
        /// 距離
        /// </summary>
        public string distance { get; }
        /// <summary>
        /// 開催時期1
        /// </summary>
        public EDate date1 { get; }
        /// <summary>
        /// 開催時期2
        /// </summary>
        public EDate date2 { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">レース名</param>
        /// <param name="field">バ場</param>
        /// <param name="distance">距離</param>
        /// <param name="date1">開催時期1</param>
        /// <param name="date2">開催時期2 2回目開催がない場合-1を指定</param>
        Race(string name, string field, string distance, EDate date1, EDate date2 = EDate.無し)
        {
            this.name = name;
            this.field = field;
            this.distance = distance;
            this.date1 = date1;
            this.date2 = date2;
        }

        /// <summary>
        /// 未勝利レースロード
        /// </summary>
        /// <param name="uacs">プログラム本体</param>
        /// <param name="races">リスト</param>
        public static void LoadUnwinningRaces(UACS uacs, ref List<Race> races)
        {
            /*
            if (uacs.cb_G2.Checked)
            {
                races.Add(new Race(uacs.cb_G2.Text, "", "", EDate));
            }
            */

            //リセット
            races.Clear();

            #region 各レース読み込み
            #region GⅠ
            //フェブラリーS
            if (uacs.cb_G1_0_feb.Checked)
            {
                races.Add(new Race(uacs.cb_G1_0_feb.Text, "ダート", "マイル", EDate.シニア2月後半));
            }
            //高松宮記念
            if (uacs.cb_G1_1_takam.Checked)
            {
                races.Add(new Race(uacs.cb_G1_1_takam.Text, "芝", "短距離", EDate.シニア3月後半));
            }
            //大阪杯
            if (uacs.cb_G1_2_osk.Checked)
            {
                races.Add(new Race(uacs.cb_G1_2_osk.Text, "芝", "中距離", EDate.シニア3月後半));
            }
            //桜花賞
            if (uacs.cb_G1_3_ouka.Checked)
            {
                races.Add(new Race(uacs.cb_G1_3_ouka.Text, "芝", "マイル", EDate.クラシック4月前半));
            }
            //皐月賞
            if (uacs.cb_G1_4_satsuki.Checked)
            {
                races.Add(new Race(uacs.cb_G1_4_satsuki.Text, "芝", "中距離", EDate.クラシック4月前半));
            }
            //天皇賞（春）
            if (uacs.cb_G1_5_tenSpr.Checked)
            {
                races.Add(new Race(uacs.cb_G1_5_tenSpr.Text, "芝", "長距離", EDate.シニア4月後半));
            }
            //NHKマイルC
            if (uacs.cb_G1_6_nhk.Checked)
            {
                races.Add(new Race(uacs.cb_G1_6_nhk.Text, "芝", "マイル", EDate.クラシック5月前半));
            }
            //ヴィクトリアM
            if (uacs.cb_G1_7_vic.Checked)
            {
                races.Add(new Race(uacs.cb_G1_7_vic.Text, "芝", "マイル", EDate.シニア5月前半));
            }
            //オークス
            if (uacs.cb_G1_8_orc.Checked)
            {
                races.Add(new Race(uacs.cb_G1_8_orc.Text, "芝", "中距離", EDate.クラシック5月後半));
            }
            //日本ダービー
            if (uacs.cb_G1_9_der.Checked)
            {
                races.Add(new Race(uacs.cb_G1_9_der.Text, "芝", "中距離", EDate.クラシック5月後半));
            }
            //安田記念
            if (uacs.cb_G1_10_yas.Checked)
            {
                races.Add(new Race(uacs.cb_G1_10_yas.Text, "芝", "マイル", EDate.クラシック6月前半, EDate.シニア6月前半));
            }
            //宝塚記念
            if (uacs.cb_G1_11_takara.Checked)
            {
                races.Add(new Race(uacs.cb_G1_11_takara.Text, "芝", "中距離", EDate.クラシック6月後半, EDate.シニア6月後半));
            }
            //スプリンターズS
            if (uacs.cb_G1_12_spr.Checked)
            {
                races.Add(new Race(uacs.cb_G1_12_spr.Text, "芝", "短距離", EDate.クラシック9月後半, EDate.シニア9月後半));
            }
            //秋華賞
            if (uacs.cb_G1_13_syu.Checked)
            {
                races.Add(new Race(uacs.cb_G1_13_syu.Text, "芝", "中距離", EDate.クラシック10月後半));
            }
            //菊花賞
            if (uacs.cb_G1_14_kik.Checked)
            {
                races.Add(new Race(uacs.cb_G1_14_kik.Text, "芝", "長距離", EDate.クラシック10月後半));
            }
            //天皇賞（秋）
            if (uacs.cb_G1_15_tenAut.Checked)
            {
                races.Add(new Race(uacs.cb_G1_15_tenAut.Text, "芝", "中距離", EDate.クラシック10月後半, EDate.シニア10月後半));
            }
            //エリザベス女王杯
            if (uacs.cb_G1_16_eriz.Checked)
            {
                races.Add(new Race(uacs.cb_G1_16_eriz.Text, "芝", "中距離", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //マイルCS
            if (uacs.cb_G1_17_milCS.Checked)
            {
                races.Add(new Race(uacs.cb_G1_17_milCS.Text, "芝", "マイル", EDate.クラシック11月後半, EDate.シニア11月後半));
            }
            //ジャパンC
            if (uacs.cb_G1_18_japanC.Checked)
            {
                races.Add(new Race(uacs.cb_G1_18_japanC.Text, "芝", "中距離", EDate.クラシック11月後半, EDate.シニア11月後半));
            }
            //チャンピオンズC
            if (uacs.cb_G1_19_champ.Checked)
            {
                races.Add(new Race(uacs.cb_G1_19_champ.Text, "ダート", "マイル", EDate.クラシック12月前半, EDate.シニア12月前半));
            }
            //阪神JF
            if (uacs.cb_G1_20_hanJF.Checked)
            {
                races.Add(new Race(uacs.cb_G1_20_hanJF.Text, "芝", "マイル", EDate.ジュニア12月前半));
            }
            //朝日杯FS
            if (uacs.cb_G1_21_asaFS.Checked)
            {
                races.Add(new Race(uacs.cb_G1_21_asaFS.Text, "芝", "マイル", EDate.ジュニア12月前半));
            }
            //有馬記念
            if (uacs.cb_G1_22_arima.Checked)
            {
                races.Add(new Race(uacs.cb_G1_22_arima.Text, "芝", "長距離", EDate.クラシック12月後半, EDate.シニア12月後半));
            }
            //ホープフルS
            if (uacs.cb_G1_23_hope.Checked)
            {
                races.Add(new Race(uacs.cb_G1_23_hope.Text, "芝", "中距離", EDate.ジュニア12月後半));
            }
            //東京大賞典
            if (uacs.cb_G1_24_tokyo.Checked)
            {
                races.Add(new Race(uacs.cb_G1_24_tokyo.Text, "芝", "中距離", EDate.クラシック12月後半, EDate.シニア12月後半));
            }
            //JBCクラシック
            if (uacs.cb_G1_25_jbcC.Checked)
            {
                races.Add(new Race(uacs.cb_G1_25_jbcC.Text, "ダート", "中距離", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //JBCスプリント
            if (uacs.cb_G1_26_jbcS.Checked)
            {
                races.Add(new Race(uacs.cb_G1_26_jbcS.Text, "ダート", "短距離", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //JBCレディスクラシック
            if (uacs.cb_G1_27_jbcR.Checked)
            {
                races.Add(new Race(uacs.cb_G1_27_jbcR.Text, "ダート", "マイル", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //ジャパンダートダービー
            if (uacs.cb_G1_28_japanD.Checked)
            {
                races.Add(new Race(uacs.cb_G1_28_japanD.Text, "ダート", "中距離", EDate.クラシック7月前半));
            }
            //帝王賞
            if (uacs.cb_G1_29_teio.Checked)
            {
                races.Add(new Race(uacs.cb_G1_29_teio.Text, "ダート", "中距離", EDate.シニア6月後半));
            }
            #endregion
            #region GⅡ
            //日経新春杯
            if (uacs.cb_G2_0_nikkeiSinsyun.Checked)
            {
                races.Add(new Race(uacs.cb_G2_0_nikkeiSinsyun.Text, "芝", "中距離", EDate.シニア1月前半));
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uacs">プログラム本体</param>
        /// <param name="races">リスト</param>
        /// <returns></returns>
        public static string GetRacePlan(UACS uacs, List<Race> races)
        {
            //結果
            string result = "";
            //周回数
            int orbitCount = 1;

            while (races.Count != 0 && orbitCount < 10)
            {
                //適正レース個数確認用
                Dictionary<string, int> appropriates = new Dictionary<string, int>();
                appropriates.Add("芝", 0);
                appropriates.Add("ダート", 0);
                appropriates.Add("短距離", 0);
                appropriates.Add("マイル", 0);
                appropriates.Add("中距離", 0);
                appropriates.Add("長距離", 0);
                //上げる適正
                string growAppropriates = "";
                //連続出走カウント
                int runCount = 0;


                //育成回数を記録
                result += orbitCount + "回目" + Environment.NewLine;

                #region 上げる適正を決める
                //未取得トロフィーレースの中で一番レースに出れる適性の組み合わせを取得
                //3年分回す
                for (int date = 0; date <= (int)EDate.シニア12月後半; date++)
                {
                    bool bTurf = false,
                         bDirt = false,
                         bSprint = false,
                         bMile = false,
                         bMiddle = false,
                         bLong = false;

                    //未勝利レース分回す
                    for (int i = 0; i < races.Count; i++)
                    {
                        //出走時期でなかったら次へ
                        if ((int)races[i].date1 != date && (int)races[i].date2 != date) { continue; }

                        if (races[i].field == "芝") { bTurf = true; }
                        else if (races[i].field == "ダート") { bDirt = true; }

                        if (races[i].distance == "短距離") { bSprint = true; }
                        else if (races[i].distance == "マイル") { bMile = true; }
                        else if (races[i].distance == "中距離") { bMiddle = true; }
                        else if (races[i].distance == "長距離") { bLong = true; }
                    }

                    //加算
                    if (bTurf == true) { appropriates["芝"]++; }
                    if (bDirt == true) { appropriates["ダート"]++; }

                    if (bSprint == true) { appropriates["短距離"]++; }
                    if (bMile == true) { appropriates["マイル"]++; }
                    if (bMiddle == true) { appropriates["中距離"]++; }
                    if (bLong == true) { appropriates["長距離"]++; }
                }

                //個数が多い順に並び替えて
                //C以下で一番多いレースの適性を上げる
                foreach (var v in appropriates.OrderByDescending(c => c.Value))
                {
                    if (v.Key == "芝" && uacs.pd_field_turf.SelectedIndex > (int)EAppropriate.B ||
                        v.Key == "ダート" && uacs.pd_field_dirt.SelectedIndex > (int)EAppropriate.B ||
                        v.Key == "短距離" && uacs.pd_distance_sprint.SelectedIndex > (int)EAppropriate.B ||
                        v.Key == "マイル" && uacs.pd_distance_mile.SelectedIndex > (int)EAppropriate.B ||
                        v.Key == "中距離" && uacs.pd_distance_middle.SelectedIndex > (int)EAppropriate.B ||
                        v.Key == "長距離" && uacs.pd_distance_long.SelectedIndex > (int)EAppropriate.B)
                    {
                        if (growAppropriates != "")
                        {
                            growAppropriates += ",";
                        }

                        growAppropriates += v.Key;

                        if (IsExistsRunableRace(uacs, growAppropriates, races))
                        {
                            break;
                        }
                    }
                }

                //結果を出力
                result += "上げる適正:" + growAppropriates + Environment.NewLine;
                #endregion

                #region 適性をもとに1周回分のレース出走プランを立てる
                //3年分回す
                for (int date = 0; date <= (int)EDate.シニア12月後半; date++)
                {
                    //設定された連続出走許容数を超える出場になる場合は1ターンお休み　（index3は無制限）
                    if (uacs.pd_raceCount.SelectedIndex != (int)ERaceCount.無制限 && runCount >= uacs.pd_raceCount.SelectedIndex + 2)
                    {
                        runCount = 0;
                        continue;
                    }

                    //未勝利レース分回す
                    foreach (var v in races)
                    {
                        //出走時期でなかったら次へ
                        if ((int)v.date1 != date && (int)v.date2 != date) { continue; }

                        //キャラ適正と一致していなかったら次へ
                        if (v.field == "芝" && uacs.pd_field_turf.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.field) == false) { continue; }
                        else if (v.field == "ダート" && uacs.pd_field_dirt.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.field) == false) { continue; }

                        if (v.distance == "短距離" && uacs.pd_distance_sprint.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }
                        else if (v.distance == "マイル" && uacs.pd_distance_mile.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }
                        else if (v.distance == "中距離" && uacs.pd_distance_middle.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }
                        else if (v.distance == "長距離" && uacs.pd_distance_long.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }

                        //適正、時期ともに合格なので結果に出力
                        result += (EDate)date + " " + v.name + Environment.NewLine;

                        //リストから削除
                        races.Remove(v);

                        //出走カウントインクリメント
                        runCount++;

                        break;
                    }
                }
                #endregion

                //残りレース数
                result += "残りレース" +  Environment.NewLine;
                foreach(var v in races)
                {
                    result += v.name + Environment.NewLine;
                }

                //周回数を増やす
                orbitCount++;

                //改行
                result += Environment.NewLine;
            }

            return result;
        }

        /// <summary>
        /// レースリストの中に現在の適正で出走可能(B以上)なレースが存在するか
        /// </summary>
        /// <param name="growAppropriates"></param>
        /// <param name="races"></param>
        /// <returns></returns>
        static bool IsExistsRunableRace(UACS uacs, string growAppropriates, List<Race> races)
        {
            //レース全部チェック
            foreach (var v in races)
            {
                if (v.field == "芝" && uacs.pd_field_turf.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.field) == false) { continue; }
                else if (v.field == "ダート" && uacs.pd_field_dirt.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.field) == false) { continue; }

                if (v.distance == "短距離" && uacs.pd_distance_sprint.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }
                else if (v.distance == "マイル" && uacs.pd_distance_mile.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }
                else if (v.distance == "中距離" && uacs.pd_distance_middle.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }
                else if (v.distance == "長距離" && uacs.pd_distance_long.SelectedIndex > (int)EAppropriate.B && growAppropriates.Contains(v.distance) == false) { continue; }

                return true;
            }

            return false;
        }
    }
}
