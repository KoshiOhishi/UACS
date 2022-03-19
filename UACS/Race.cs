using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UACS
{
    /// <summary>
    /// 時期
    /// </summary>
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

    /// <summary>
    /// 適性
    /// </summary>
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

    /// <summary>
    /// レース出走制限数
    /// </summary>
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
            if (uacs.cb_G3.Checked)
            {
                races.Add(new Race(uacs.cb_G3.Text, "", "", EDate));
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
            //東海S
            if (uacs.cb_G2_1_tokai.Checked)
            {
                races.Add(new Race(uacs.cb_G2_1_tokai.Text, "ダート", "マイル", EDate.シニア1月後半));
            }
            //アメリカJCC
            if (uacs.cb_G2_2_amerika.Checked)
            {
                races.Add(new Race(uacs.cb_G2_2_amerika.Text, "芝", "中距離", EDate.シニア1月後半));
            }
            //京都記念
            if (uacs.cb_G2_3_kyoto.Checked)
            {
                races.Add(new Race(uacs.cb_G2_3_kyoto.Text, "芝", "中距離", EDate.シニア2月前半));
            }
            //中山記念
            if (uacs.cb_G2_4_nakayama.Checked)
            {
                races.Add(new Race(uacs.cb_G2_4_nakayama.Text, "芝", "マイル", EDate.シニア2月後半));
            }
            //チューリップ賞
            if (uacs.cb_G2_5_tulip.Checked)
            {
                races.Add(new Race(uacs.cb_G2_5_tulip.Text, "芝", "マイル", EDate.クラシック3月前半));
            }
            //弥生賞
            if (uacs.cb_G2_6_yayoi.Checked)
            {
                races.Add(new Race(uacs.cb_G2_6_yayoi.Text, "芝", "中距離", EDate.クラシック3月前半));
            }
            //金鯱賞
            if (uacs.cb_G2_7_kinko.Checked)
            {
                races.Add(new Race(uacs.cb_G2_7_kinko.Text, "芝", "中距離", EDate.シニア3月前半));
            }
            //フィリーズレビュー
            if (uacs.cb_G2_8_phillies.Checked)
            {
                races.Add(new Race(uacs.cb_G2_8_phillies.Text, "芝", "短距離", EDate.クラシック3月前半));
            }
            //阪神大賞典
            if (uacs.cb_G2_9_hanshinDai.Checked)
            {
                races.Add(new Race(uacs.cb_G2_9_hanshinDai.Text, "芝", "長距離", EDate.シニア3月後半));
            }
            //スプリングS
            if (uacs.cb_G2_10_spring.Checked)
            {
                races.Add(new Race(uacs.cb_G2_10_spring.Text, "芝", "マイル", EDate.クラシック3月後半));
            }
            //日経賞
            if (uacs.cb_G2_11_nikkeiPrize.Checked)
            {
                races.Add(new Race(uacs.cb_G2_11_nikkeiPrize.Text, "芝", "長距離", EDate.シニア3月後半));
            }
            //阪神ウマ娘S
            if (uacs.cb_G2_12_hanshinUma.Checked)
            {
                races.Add(new Race(uacs.cb_G2_12_hanshinUma.Text, "芝", "マイル", EDate.シニア4月前半));
            }
            //ニュージーランドT
            if (uacs.cb_G2_13_newZealand.Checked)
            {
                races.Add(new Race(uacs.cb_G2_13_newZealand.Text, "芝", "マイル", EDate.クラシック4月前半));
            }
            //マイラーズC
            if (uacs.cb_G2_14_milers.Checked)
            {
                races.Add(new Race(uacs.cb_G2_14_milers.Text, "芝", "マイル", EDate.シニア4月後半));
            }
            //フローラS
            if (uacs.cb_G2_15_flora.Checked)
            {
                races.Add(new Race(uacs.cb_G2_15_flora.Text, "芝", "中距離", EDate.クラシック4月後半));
            }
            //青葉賞
            if (uacs.cb_G2_16_aoba.Checked)
            {
                races.Add(new Race(uacs.cb_G2_16_aoba.Text, "芝", "中距離", EDate.クラシック4月後半));
            }
            //京都新聞杯
            if (uacs.cb_G2_17_kyotoSinb.Checked)
            {
                races.Add(new Race(uacs.cb_G2_17_kyotoSinb.Text, "芝", "中距離", EDate.クラシック5月前半));
            }
            //京王杯スプリングC
            if (uacs.cb_G2_18_keioSpr.Checked)
            {
                races.Add(new Race(uacs.cb_G2_18_keioSpr.Text, "芝", "短距離", EDate.シニア5月前半));
            }
            //目黒記念
            if (uacs.cb_G2_19_meguro.Checked)
            {
                races.Add(new Race(uacs.cb_G2_19_meguro.Text, "芝", "長距離", EDate.シニア5月後半));
            }
            //札幌記念
            if (uacs.cb_G2_20_sapporo.Checked)
            {
                races.Add(new Race(uacs.cb_G2_20_sapporo.Text, "芝", "中距離", EDate.クラシック8月後半, EDate.シニア8月後半));
            }
            //セントウルS
            if (uacs.cb_G2_21_centaur.Checked)
            {
                races.Add(new Race(uacs.cb_G2_21_centaur.Text, "芝", "短距離", EDate.クラシック9月前半, EDate.シニア9月前半));
            }
            //ローズS
            if (uacs.cb_G2_22_rose.Checked)
            {
                races.Add(new Race(uacs.cb_G2_22_rose.Text, "芝", "マイル", EDate.クラシック9月前半));
            }
            //セントライト記念
            if (uacs.cb_G2_23_sentlight.Checked)
            {
                races.Add(new Race(uacs.cb_G2_23_sentlight.Text, "芝", "中距離", EDate.クラシック9月後半));
            }
            //神戸新聞杯
            if (uacs.cb_G2_24_kobe.Checked)
            {
                races.Add(new Race(uacs.cb_G2_24_kobe.Text, "芝", "中距離", EDate.クラシック9月後半));
            }
            //オールカマー
            if (uacs.cb_G2_25_allComer.Checked)
            {
                races.Add(new Race(uacs.cb_G2_25_allComer.Text, "芝", "中距離", EDate.クラシック9月後半, EDate.シニア9月後半));
            }
            //毎日王冠
            if (uacs.cb_G2_26_mainichi.Checked)
            {
                races.Add(new Race(uacs.cb_G2_26_mainichi.Text, "芝", "マイル", EDate.クラシック10月前半, EDate.シニア10月前半));
            }
            //京都大賞典
            if (uacs.cb_G2_27_kyotoDai.Checked)
            {
                races.Add(new Race(uacs.cb_G2_27_kyotoDai.Text, "芝", "中距離", EDate.クラシック10月前半, EDate.シニア10月前半));
            }
            //府中ウマ娘S
            if (uacs.cb_G2_28_fuchuUma.Checked)
            {
                races.Add(new Race(uacs.cb_G2_28_fuchuUma.Text, "芝", "マイル", EDate.クラシック10月前半, EDate.シニア10月前半));
            }
            //富士S
            if (uacs.cb_G2_29_fuji.Checked)
            {
                races.Add(new Race(uacs.cb_G2_29_fuji.Text, "芝", "マイル", EDate.クラシック10月後半, EDate.シニア10月後半));
            }
            //スワンS
            if (uacs.cb_G2_30_swan.Checked)
            {
                races.Add(new Race(uacs.cb_G2_30_swan.Text, "芝", "短距離", EDate.クラシック10月後半, EDate.シニア10月後半));
            }
            //京王杯ジュニアS
            if (uacs.cb_G2_31_keioJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G2_31_keioJunior.Text, "芝", "短距離", EDate.ジュニア11月前半));
            }
            //アルゼンチン共和国杯
            if (uacs.cb_G2_32_argentine.Checked)
            {
                races.Add(new Race(uacs.cb_G2_32_argentine.Text, "芝", "長距離", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //デイリー杯ジュニアS
            if (uacs.cb_G2_33_daily.Checked)
            {
                races.Add(new Race(uacs.cb_G2_33_daily.Text, "芝", "マイル", EDate.ジュニア11月前半));
            }
            //ステイヤーズS
            if (uacs.cb_G2_34_staiyers.Checked)
            {
                races.Add(new Race(uacs.cb_G2_34_staiyers.Text, "芝", "長距離", EDate.クラシック12月前半, EDate.シニア12月前半));
            }
            //阪神C
            if (uacs.cb_G2_35_hanshinC.Checked)
            {
                races.Add(new Race(uacs.cb_G2_35_hanshinC.Text, "芝", "短距離", EDate.クラシック12月後半, EDate.シニア12月後半));
            }
            #endregion
            #region GⅢ
            //京都金杯
            if (uacs.cb_G3_0_kyotokin.Checked)
            {
                races.Add(new Race(uacs.cb_G3_0_kyotokin.Text, "芝", "マイル", EDate.シニア1月前半));
            }
            //中山金杯
            if (uacs.cb_G3_1_nakayamakin.Checked)
            {
                races.Add(new Race(uacs.cb_G3_1_nakayamakin.Text, "芝", "中距離", EDate.シニア1月前半));
            }
            //シンザン記念
            if (uacs.cb_G3_2_sinzan.Checked)
            {
                races.Add(new Race(uacs.cb_G3_2_sinzan.Text, "芝", "マイル", EDate.クラシック1月前半));
            }
            //フェアリーS
            if (uacs.cb_G3_3_fairy.Checked)
            {
                races.Add(new Race(uacs.cb_G3_3_fairy.Text, "芝", "マイル", EDate.クラシック1月前半));
            }
            //愛知杯
            if (uacs.cb_G3_4_aichi.Checked)
            {
                races.Add(new Race(uacs.cb_G3_4_aichi.Text, "芝", "中距離", EDate.シニア1月前半));
            }
            //京成杯
            if (uacs.cb_G3_5_keiseihai.Checked)
            {
                races.Add(new Race(uacs.cb_G3_5_keiseihai.Text, "芝", "中距離", EDate.クラシック1月前半));
            }
            //シルクロードS
            if (uacs.cb_G3_6_silkroad.Checked)
            {
                races.Add(new Race(uacs.cb_G3_6_silkroad.Text, "芝", "短距離", EDate.シニア1月後半));
            }
            //根岸S
            if (uacs.cb_G3_7_negishi.Checked)
            {
                races.Add(new Race(uacs.cb_G3_7_negishi.Text, "ダート", "短距離", EDate.シニア1月後半));
            }
            //きさらぎ賞
            if (uacs.cb_G3_8_kisaragi.Checked)
            {
                races.Add(new Race(uacs.cb_G3_8_kisaragi.Text, "芝", "マイル", EDate.クラシック2月前半));
            }
            //東京新聞杯
            if (uacs.cb_G3_9_tokyoShinbun.Checked)
            {
                races.Add(new Race(uacs.cb_G3_9_tokyoShinbun.Text, "芝", "マイル", EDate.シニア2月前半));
            }
            //クイーンC
            if (uacs.cb_G3_10_queenC.Checked)
            {
                races.Add(new Race(uacs.cb_G3_10_queenC.Text, "芝", "マイル", EDate.クラシック2月前半));
            }
            //共同通信杯
            if (uacs.cb_G3_11_kyodoTushin.Checked)
            {
                races.Add(new Race(uacs.cb_G3_11_kyodoTushin.Text, "芝", "マイル", EDate.クラシック2月前半));
            }
            //京都ウマ娘S
            if (uacs.cb_G3_12_kyotoUma.Checked)
            {
                races.Add(new Race(uacs.cb_G3_12_kyotoUma.Text, "芝", "短距離", EDate.シニア2月後半));
            }
            //ダイヤモンドS
            if (uacs.cb_G3_13_diamond.Checked)
            {
                races.Add(new Race(uacs.cb_G3_13_diamond.Text, "芝", "長距離", EDate.シニア2月後半));
            }
            //小倉大賞典
            if (uacs.cb_G3_14_kokuraDai.Checked)
            {
                races.Add(new Race(uacs.cb_G3_14_kokuraDai.Text, "芝", "マイル", EDate.シニア2月後半));
            }
            //アーリントンC
            if (uacs.cb_G3_15_arinton.Checked)
            {
                races.Add(new Race(uacs.cb_G3_15_arinton.Text, "芝", "マイル", EDate.クラシック4月前半));
            }
            //阪急杯
            if (uacs.cb_G3_16_hankyu.Checked)
            {
                races.Add(new Race(uacs.cb_G3_16_hankyu.Text, "芝", "短距離", EDate.シニア2月後半));
            }
            //オーシャンS
            if (uacs.cb_G3_17_osean.Checked)
            {
                races.Add(new Race(uacs.cb_G3_17_osean.Text, "芝", "短距離", EDate.シニア3月前半));
            }
            //中山ウマ娘S
            if (uacs.cb_G3_18_nakayamaUma.Checked)
            {
                races.Add(new Race(uacs.cb_G3_18_nakayamaUma.Text, "芝", "マイル", EDate.シニア3月前半));
            }
            //ファルコンS
            if (uacs.cb_G3_19_falcon.Checked)
            {
                races.Add(new Race(uacs.cb_G3_19_falcon.Text, "芝", "短距離", EDate.クラシック3月後半));
            }
            //フラワーC
            if (uacs.cb_G3_20_flower.Checked)
            {
                races.Add(new Race(uacs.cb_G3_20_flower.Text, "芝", "マイル", EDate.クラシック3月後半));
            }
            //毎日杯
            if (uacs.cb_G3_21_mainichi.Checked)
            {
                races.Add(new Race(uacs.cb_G3_21_mainichi.Text, "芝", "マイル", EDate.クラシック3月後半));
            }
            //マーチS
            if (uacs.cb_G3_22_march.Checked)
            {
                races.Add(new Race(uacs.cb_G3_22_march.Text, "ダート", "マイル", EDate.シニア3月後半));
            }
            //ダービー卿CT
            if (uacs.cb_G3_23_derbyKyo.Checked)
            {
                races.Add(new Race(uacs.cb_G3_23_derbyKyo.Text, "芝", "マイル", EDate.シニア4月前半));
            }
            //アンタレスS
            if (uacs.cb_G3_24_antares.Checked)
            {
                races.Add(new Race(uacs.cb_G3_24_antares.Text, "ダート", "マイル", EDate.シニア4月前半));
            }
            //福島ウマ娘S
            if (uacs.cb_G3_25_fukushima.Checked)
            {
                races.Add(new Race(uacs.cb_G3_25_fukushima.Text, "芝", "マイル", EDate.シニア4月後半));
            }
            //新潟大賞典
            if (uacs.cb_G3_26_nigataDai.Checked)
            {
                races.Add(new Race(uacs.cb_G3_26_nigataDai.Text, "芝", "中距離", EDate.シニア5月前半));
            }
            //平安S
            if (uacs.cb_G3_27_heian.Checked)
            {
                races.Add(new Race(uacs.cb_G3_27_heian.Text, "ダート", "中距離", EDate.シニア5月後半));
            }
            //葵S
            if (uacs.cb_G3_28_aoi.Checked)
            {
                races.Add(new Race(uacs.cb_G3_28_aoi.Text, "芝", "短距離", EDate.クラシック5月後半));
            }
            //成尾記念
            if (uacs.cb_G3_29_naruo.Checked)
            {
                races.Add(new Race(uacs.cb_G3_29_naruo.Text, "芝", "中距離", EDate.クラシック6月前半, EDate.シニア6月前半));
            }
            //マーメイドS
            if (uacs.cb_G3_30_mermaid.Checked)
            {
                races.Add(new Race(uacs.cb_G3_30_mermaid.Text, "芝", "中距離", EDate.クラシック6月前半, EDate.シニア6月前半));
            }
            //エプソムC
            if (uacs.cb_G3_31_epsom.Checked)
            {
                races.Add(new Race(uacs.cb_G3_31_epsom.Text, "芝", "マイル", EDate.クラシック6月前半, EDate.シニア6月前半));
            }
            //ユニコーンS
            if (uacs.cb_G3_32_unicorn.Checked)
            {
                races.Add(new Race(uacs.cb_G3_32_unicorn.Text, "ダート", "マイル", EDate.クラシック6月後半));
            }
            //函館スプリントS
            if (uacs.cb_G3_33_hakodateSpr.Checked)
            {
                races.Add(new Race(uacs.cb_G3_33_hakodateSpr.Text, "芝", "短距離", EDate.クラシック6月後半, EDate.シニア6月後半));
            }
            //CBC賞
            if (uacs.cb_G3_34_cbc.Checked)
            {
                races.Add(new Race(uacs.cb_G3_34_cbc.Text, "芝", "短距離", EDate.クラシック7月前半, EDate.シニア7月前半));
            }
            //ラジオNIKKEI賞
            if (uacs.cb_G3_35_radioNIKKEI.Checked)
            {
                races.Add(new Race(uacs.cb_G3_35_radioNIKKEI.Text, "芝", "マイル", EDate.クラシック7月前半));
            }
            //プロキオンS
            if (uacs.cb_G3_36_procyon.Checked)
            {
                races.Add(new Race(uacs.cb_G3_36_procyon.Text, "ダート", "短距離", EDate.クラシック7月前半, EDate.シニア7月前半));
            }
            //七夕賞
            if (uacs.cb_G3_37_tanabata.Checked)
            {
                races.Add(new Race(uacs.cb_G3_37_tanabata.Text, "芝", "中距離", EDate.クラシック7月前半, EDate.シニア7月前半));
            }
            //函館記念
            if (uacs.cb_G3_38_hakodateKinen.Checked)
            {
                races.Add(new Race(uacs.cb_G3_38_hakodateKinen.Text, "芝", "中距離", EDate.クラシック7月前半, EDate.シニア7月前半));
            }
            //中京記念
            if (uacs.cb_G3_39_cyukyo.Checked)
            {
                races.Add(new Race(uacs.cb_G3_39_cyukyo.Text, "芝", "マイル", EDate.クラシック7月後半, EDate.シニア7月後半));
            }
            //函館ジュニアS
            if (uacs.cb_G3_40_hakodateJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G3_40_hakodateJunior.Text, "芝", "短距離", EDate.ジュニア7月後半));
            }
            //アイビスSD
            if (uacs.cb_G3_41_aibis.Checked)
            {
                races.Add(new Race(uacs.cb_G3_41_aibis.Text, "芝", "短距離", EDate.クラシック7月後半, EDate.シニア7月後半));
            }
            //クイーンS
            if (uacs.cb_G3_42_queenS.Checked)
            {
                races.Add(new Race(uacs.cb_G3_42_queenS.Text, "芝", "マイル", EDate.クラシック7月後半, EDate.シニア7月後半));
            }
            //小倉記念
            if (uacs.cb_G3_43_kokuraKinen.Checked)
            {
                races.Add(new Race(uacs.cb_G3_43_kokuraKinen.Text, "芝", "中距離", EDate.クラシック8月前半, EDate.シニア8月前半));
            }
            //レパードS
            if (uacs.cb_G3_44_repard.Checked)
            {
                races.Add(new Race(uacs.cb_G3_44_repard.Text, "ダート", "マイル", EDate.クラシック8月前半));
            }
            //関屋記念
            if (uacs.cb_G3_45_sekiya.Checked)
            {
                races.Add(new Race(uacs.cb_G3_45_sekiya.Text, "芝", "マイル", EDate.クラシック8月前半, EDate.シニア8月前半));
            }
            //エルムS
            if (uacs.cb_G3_46_erum.Checked)
            {
                races.Add(new Race(uacs.cb_G3_46_erum.Text, "ダート", "マイル", EDate.クラシック8月前半, EDate.シニア8月前半));
            }
            //北九州記念
            if (uacs.cb_G3_47_kitakyusyu.Checked)
            {
                races.Add(new Race(uacs.cb_G3_47_kitakyusyu.Text, "芝", "短距離", EDate.クラシック8月後半, EDate.シニア8月後半));
            }
            //新潟ジュニアS
            if (uacs.cb_G3_48_nigataJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G3_48_nigataJunior.Text, "芝", "マイル", EDate.ジュニア8月後半));
            }
            //キーンランドC
            if (uacs.cb_G3_49_keenland.Checked)
            {
                races.Add(new Race(uacs.cb_G3_49_keenland.Text, "芝", "短距離", EDate.クラシック8月後半, EDate.シニア8月後半));
            }
            //札幌ジュニアS
            if (uacs.cb_G3_50_sapporoJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G3_50_sapporoJunior.Text, "芝", "マイル", EDate.ジュニア9月前半));
            }
            //小倉ジュニアS
            if (uacs.cb_G3_51_kokuraJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G3_51_kokuraJunior.Text, "芝", "短距離", EDate.ジュニア9月前半));
            }
            //新潟記念
            if (uacs.cb_G3_52_nigataKinen.Checked)
            {
                races.Add(new Race(uacs.cb_G3_52_nigataKinen.Text, "芝", "中距離", EDate.クラシック9月前半, EDate.シニア9月前半));
            }
            //紫苑S
            if (uacs.cb_G3_53_shion.Checked)
            {
                races.Add(new Race(uacs.cb_G3_53_shion.Text, "芝", "中距離", EDate.クラシック9月前半));
            }
            //京成杯オータムH
            if (uacs.cb_G3_54_keiseihaiAutumn.Checked)
            {
                races.Add(new Race(uacs.cb_G3_54_keiseihaiAutumn.Text, "芝", "マイル", EDate.クラシック9月前半, EDate.シニア9月前半));
            }
            //シリウスS
            if (uacs.cb_G3_55_sirius.Checked)
            {
                races.Add(new Race(uacs.cb_G3_55_sirius.Text, "ダート", "中距離", EDate.クラシック9月後半, EDate.シニア9月後半));
            }
            //サウジアラビアRC
            if (uacs.cb_G3_56_saudiArabia.Checked)
            {
                races.Add(new Race(uacs.cb_G3_56_saudiArabia.Text, "芝", "マイル", EDate.ジュニア10月前半));
            }
            //アルテミスS
            if (uacs.cb_G3_57_artemis.Checked)
            {
                races.Add(new Race(uacs.cb_G3_57_artemis.Text, "芝", "マイル", EDate.ジュニア10月後半));
            }
            //ファンタジーS
            if (uacs.cb_G3_58_fantasy.Checked)
            {
                races.Add(new Race(uacs.cb_G3_58_fantasy.Text, "芝", "短距離", EDate.ジュニア11月前半));
            }
            //みやこS
            if (uacs.cb_G3_59_miyako.Checked)
            {
                races.Add(new Race(uacs.cb_G3_59_miyako.Text, "ダート", "マイル", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //武蔵野S
            if (uacs.cb_G3_60_musashino.Checked)
            {
                races.Add(new Race(uacs.cb_G3_60_musashino.Text, "ダート", "マイル", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //福島記念
            if (uacs.cb_G3_61_fukushimaKinen.Checked)
            {
                races.Add(new Race(uacs.cb_G3_61_fukushimaKinen.Text, "芝", "中距離", EDate.クラシック11月前半, EDate.シニア11月前半));
            }
            //東スポ杯ジュニアS
            if (uacs.cb_G3_62_tospoJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G3_62_tospoJunior.Text, "芝", "マイル", EDate.ジュニア11月後半));
            }
            //京都ジュニアS
            if (uacs.cb_G3_63_kyotoJunior.Checked)
            {
                races.Add(new Race(uacs.cb_G3_63_kyotoJunior.Text, "芝", "中距離", EDate.ジュニア11月後半));
            }
            //京阪杯
            if (uacs.cb_G3_64_keihan.Checked)
            {
                races.Add(new Race(uacs.cb_G3_64_keihan.Text, "芝", "短距離", EDate.クラシック11月後半, EDate.シニア11月後半));
            }
            //チャレンジC
            if (uacs.cb_G3_65_challenge.Checked)
            {
                races.Add(new Race(uacs.cb_G3_65_challenge.Text, "芝", "中距離", EDate.クラシック12月前半, EDate.シニア12月前半));
            }
            //中日新聞杯
            if (uacs.cb_G3_66_chunichiShinbun.Checked)
            {
                races.Add(new Race(uacs.cb_G3_66_chunichiShinbun.Text, "芝", "中距離", EDate.クラシック12月前半, EDate.シニア12月前半));
            }
            //カペラS
            if (uacs.cb_G3_67_capella.Checked)
            {
                races.Add(new Race(uacs.cb_G3_67_capella.Text, "ダート", "短距離", EDate.クラシック12月前半, EDate.シニア12月前半));
            }
            //ターコイズステークス
            if (uacs.cb_G3_68_turquoise.Checked)
            {
                races.Add(new Race(uacs.cb_G3_68_turquoise.Text, "芝", "マイル", EDate.クラシック12月前半, EDate.シニア12月前半));
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// レース出走プランを文字列で取得
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

            while (races.Count != 0)
            {
                //適性レース個数確認用
                Dictionary<string, int> aptitude = new Dictionary<string, int>();
                aptitude.Add("芝", 0);
                aptitude.Add("ダート", 0);
                aptitude.Add("短距離", 0);
                aptitude.Add("マイル", 0);
                aptitude.Add("中距離", 0);
                aptitude.Add("長距離", 0);
                //上げる適性
                string growAptitude = "";
                //連続出走カウント
                int runCount = 0;
                //因子使用回数
                int useFactorPoint = 0;
                const int MAXFACTORPOINT = 12;

                //育成回数を記録
                result += orbitCount + "回目" + Environment.NewLine;

                #region 上げる適性を決める
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
                    if (bTurf == true) { aptitude["芝"]++; }
                    if (bDirt == true) { aptitude["ダート"]++; }

                    if (bSprint == true) { aptitude["短距離"]++; }
                    if (bMile == true) { aptitude["マイル"]++; }
                    if (bMiddle == true) { aptitude["中距離"]++; }
                    if (bLong == true) { aptitude["長距離"]++; }
                }

                //個数が多い順に並び替えて
                //C以下で一番多いレースの適性を上げる
                foreach (var v in aptitude.OrderByDescending(c => c.Value))
                {
                    //レース数が0の場合は次へ
                    if (v.Value == 0) { continue; }

                    //適性
                    if (v.Key == "芝" && uacs.pd_field_turf.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex ||
                        v.Key == "ダート" && uacs.pd_field_dirt.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex ||
                        v.Key == "短距離" && uacs.pd_distance_sprint.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex ||
                        v.Key == "マイル" && uacs.pd_distance_mile.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex ||
                        v.Key == "中距離" && uacs.pd_distance_middle.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex ||
                        v.Key == "長距離" && uacs.pd_distance_long.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex)
                    {
                        //2つめの適性上げのときはカンマを入れる
                        if (growAptitude != "")
                        {
                            growAptitude += ",";
                        }

                        //上げる適性を追加
                        growAptitude += v.Key;

                        //出走可能なレースがあるかチェック
                        if (IsExistsRunableRace(uacs, growAptitude, races))
                        {
                            //上級者モードが有効なら
                            if (uacs.menu_tools_hardmode.Checked)
                            {
                                //因子ポイント加算
                                useFactorPoint += GetAddFactorPoint(uacs, v.Key);

                                //因子ポイントが許容範囲以上になったら
                                if (useFactorPoint >= MAXFACTORPOINT)
                                {
                                    break;
                                }

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                //上げる適性がないときは「無し」と追記
                if (growAptitude == "") { growAptitude += "無し"; }

                //結果を出力
                result += "上げる適性:" + growAptitude + Environment.NewLine;
                #endregion

                #region 適性をもとに1周回分のレース出走プランを立てる
                //3年分回す
                for (int date = 0; date <= (int)EDate.シニア12月後半; date++)
                {
                    //設定された連続出走許容数を超える出場になる場合は1ターンお休み　（index3は無制限）
                    if (uacs.pd_option_raceCount.SelectedIndex != (int)ERaceCount.無制限 && runCount >= uacs.pd_option_raceCount.SelectedIndex + 2)
                    {
                        runCount = 0;
                        continue;
                    }

                    //未勝利レース分回す
                    foreach (var v in races)
                    {
                        //出走時期でなかったら次へ
                        if ((int)v.date1 != date && (int)v.date2 != date) { continue; }

                        //キャラ適性と一致していなかったら次へ
                        if (v.field == "芝" && uacs.pd_field_turf.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.field) == false) { continue; }
                        else if (v.field == "ダート" && uacs.pd_field_dirt.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.field) == false) { continue; }

                        if (v.distance == "短距離" && uacs.pd_distance_sprint.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }
                        else if (v.distance == "マイル" && uacs.pd_distance_mile.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }
                        else if (v.distance == "中距離" && uacs.pd_distance_middle.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }
                        else if (v.distance == "長距離" && uacs.pd_distance_long.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }

                        //適性、時期ともに合格なので結果に出力
                        result += (EDate)date + " " + v.name + " (" + v.field + " " + v.distance + ")" + Environment.NewLine;

                        //リストから削除
                        races.Remove(v);

                        //出走カウントインクリメント
                        runCount++;

                        break;
                    }
                }
                #endregion

                ////残りレース数
                //result += "残りレース" +  Environment.NewLine;
                //foreach(var v in races)
                //{
                //    result += v.name + Environment.NewLine;
                //}

                //周回数を増やす
                orbitCount++;

                //まだレースが残ってたら改行
                if (races.Count != 0)
                {
                    result += Environment.NewLine;
                }
            }

            //改行削る
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        /// <summary>
        /// レースリストの中に現在の適性で出走可能(B以上)なレースが存在するか
        /// </summary>
        /// <param name="uacs">プログラム本体</param>
        /// <param name="growAptitude">上げた適性</param>
        /// <param name="races">未勝利レースリスト</param>
        /// <returns></returns>
        static bool IsExistsRunableRace(UACS uacs, string growAptitude, List<Race> races)
        {
            //レース全部チェック
            foreach (var v in races)
            {
                if (v.field == "芝" && uacs.pd_field_turf.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.field) == false) { continue; }
                else if (v.field == "ダート" && uacs.pd_field_dirt.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.field) == false) { continue; }

                if (v.distance == "短距離" && uacs.pd_distance_sprint.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }
                else if (v.distance == "マイル" && uacs.pd_distance_mile.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }
                else if (v.distance == "中距離" && uacs.pd_distance_middle.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }
                else if (v.distance == "長距離" && uacs.pd_distance_long.SelectedIndex > uacs.pd_option_appopriate.SelectedIndex && growAptitude.Contains(v.distance) == false) { continue; }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 適性を上げるために必要な因子ポイント数を取得 (例えば1段階ＵＰなら1pt、2段階なら4pt)
        /// </summary>
        /// <param name="uacs">プログラム本体</param>
        /// <param name="factorName">因子名 芝 短距離など</param>
        /// <returns></returns>
        static int GetAddFactorPoint(UACS uacs, string factorName)
        {
            int mul = 0;

            switch (factorName)
            {
                case "芝":
                    mul = uacs.pd_field_turf.SelectedIndex - uacs.pd_option_appopriate.SelectedIndex;
                    break;

                case "ダート":
                    mul = uacs.pd_field_dirt.SelectedIndex - uacs.pd_option_appopriate.SelectedIndex;
                    break;

                case "短距離":
                    mul = uacs.pd_distance_sprint.SelectedIndex - uacs.pd_option_appopriate.SelectedIndex;
                    break;

                case "マイル":
                    mul = uacs.pd_distance_mile.SelectedIndex - uacs.pd_option_appopriate.SelectedIndex;
                    break;

                case "中距離":
                    mul = uacs.pd_distance_middle.SelectedIndex - uacs.pd_option_appopriate.SelectedIndex;
                    break;

                case "長距離":
                    mul = uacs.pd_distance_long.SelectedIndex - uacs.pd_option_appopriate.SelectedIndex;
                    break;
            }

            return mul * 3;
        }
    }
}
