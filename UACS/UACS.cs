using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using CenterMessageBox;

namespace UACS
{
    public partial class UACS : Form
    {
        //トロフィー未取得レースリスト
        List<Race> unwinningRaces = new List<Race>();

        //バージョン取得
        static System.Diagnostics.FileVersionInfo ver =
        System.Diagnostics.FileVersionInfo.GetVersionInfo(
        System.Reflection.Assembly.GetExecutingAssembly().Location);
        string version = ver.FileVersion;

        //フック
        private IntPtr HHook;

        public UACS()
        {
            InitializeComponent();

            //初期適性
            pd_field_turf.SelectedIndex = (int)EAppropriate.A;       //芝
            pd_field_dirt.SelectedIndex = (int)EAppropriate.E;       //ダート

            pd_distance_sprint.SelectedIndex = (int)EAppropriate.D;  //短距離
            pd_distance_mile.SelectedIndex = (int)EAppropriate.D;    //マイル
            pd_distance_middle.SelectedIndex = (int)EAppropriate.A;  //中距離
            pd_distance_long.SelectedIndex = (int)EAppropriate.A;    //長距離

            //オプション初期設定
            pd_option_raceCount.SelectedIndex = (int)ERaceCount.two;
            pd_option_appopriate.SelectedIndex = (int)EAppropriate.B;

            //バージョン表示ラベルへ現在のバージョンセット(下一桁は削る)
            label_version.Text = "version:" + version.Substring(0, version.LastIndexOf("."));
        }

        /// <summary>
        /// Runボタンが押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run(object sender, EventArgs e)
        {
            //トロフィー未取得状況をロード
            Race.LoadUnwinningRaces(this, ref unwinningRaces);

            //トロフィー未取得状況からレース出走プランを生成
            string strPlan = Race.GetRacePlan(this, unwinningRaces);

            //出力
            tb_Result.Text = strPlan;
        }

        /// <summary>
        /// 更新があるか確認
        /// </summary>
        private void CheckUpdate(bool enablePopupNoUpdate = false)
        {
            string gitVersion = "";

            //GithubのAssemblyInfo.csを開く
            WebClient wc = new WebClient();
            Stream st;
            try
            {
                st = wc.OpenRead("https://raw.githubusercontent.com/KoshiOhishi/UACS/master/UACS/Properties/AssemblyInfo.cs");
            }
            catch(Exception e)
            {
                if (enablePopupNoUpdate)
                {
                    //エラーポップ表示
                    SetHook(this);
                    MessageBox.Show(this, "更新確認ができませんでした。\nネットワーク接続を確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            StreamReader sr = new StreamReader(st);

            //ファイル読み出し
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                //コメント行は無視
                if (line.StartsWith("//")) { continue; }

                //バージョン取得
                if (line.Contains("AssemblyVersion"))
                {
                    int startPos = line.IndexOf("AssemblyVersion(\"") + "AssemblyVersion(\"".Length;
                    int endPos = line.LastIndexOf("\"");
                    gitVersion = line.Substring(startPos, endPos - startPos);
                }
            }

            //自身のバージョンとGitに上がってるバージョンが違うならポップアップ表示
            if (version != gitVersion)
            {
                SetHook(this);
                DialogResult result = MessageBox.Show("新しいバージョンが公開されています。\n確認しますか？", "更新あり", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    //GitHubのリリースページに飛ぶ
                    System.Diagnostics.Process.Start("https://github.com/KoshiOhishi/UACS/releases");
                }
            }
            else if (enablePopupNoUpdate == true)
            {
                SetHook(this);
                MessageBox.Show("更新は必要ありません。", "更新なし", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            st.Close();
            wc.Dispose();

        }

        /// <summary>
        /// GⅠの全てにチェックを入れる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_G1_allOn_Click(object sender, EventArgs e)
        {
            #region 全部にチェック
            cb_G1_0_feb.Checked = true;
            cb_G1_1_takam.Checked = true;
            cb_G1_2_osk.Checked = true;
            cb_G1_3_ouka.Checked = true;
            cb_G1_4_satsuki.Checked = true;
            cb_G1_5_tenSpr.Checked = true;
            cb_G1_6_nhk.Checked = true;
            cb_G1_7_vic.Checked = true;
            cb_G1_8_orc.Checked = true;
            cb_G1_9_der.Checked = true;
            cb_G1_10_yas.Checked = true;
            cb_G1_11_takara.Checked = true;
            cb_G1_12_spr.Checked = true;
            cb_G1_13_syu.Checked = true;
            cb_G1_14_kik.Checked = true;
            cb_G1_15_tenAut.Checked = true;
            cb_G1_16_eriz.Checked = true;
            cb_G1_17_milCS.Checked = true;
            cb_G1_18_japanC.Checked = true;
            cb_G1_19_champ.Checked = true;
            cb_G1_20_hanJF.Checked = true;
            cb_G1_21_asaFS.Checked = true;
            cb_G1_22_arima.Checked = true;
            cb_G1_23_hope.Checked = true;
            cb_G1_24_tokyo.Checked = true;
            cb_G1_25_jbcC.Checked = true;
            cb_G1_26_jbcS.Checked = true;
            cb_G1_27_jbcR.Checked = true;
            cb_G1_28_japanD.Checked = true;
            cb_G1_29_teio.Checked = true;
            #endregion
        }

        /// <summary>
        /// GⅠの全てのチェックを外す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_G1_allOff_Click(object sender, EventArgs e)
        {
            #region 全部のチェックを外す
            cb_G1_0_feb.Checked = false;
            cb_G1_1_takam.Checked = false;
            cb_G1_2_osk.Checked = false;
            cb_G1_3_ouka.Checked = false;
            cb_G1_4_satsuki.Checked = false;
            cb_G1_5_tenSpr.Checked = false;
            cb_G1_6_nhk.Checked = false;
            cb_G1_7_vic.Checked = false;
            cb_G1_8_orc.Checked = false;
            cb_G1_9_der.Checked = false;
            cb_G1_10_yas.Checked = false;
            cb_G1_11_takara.Checked = false;
            cb_G1_12_spr.Checked = false;
            cb_G1_13_syu.Checked = false;
            cb_G1_14_kik.Checked = false;
            cb_G1_15_tenAut.Checked = false;
            cb_G1_16_eriz.Checked = false;
            cb_G1_17_milCS.Checked = false;
            cb_G1_18_japanC.Checked = false;
            cb_G1_19_champ.Checked = false;
            cb_G1_20_hanJF.Checked = false;
            cb_G1_21_asaFS.Checked = false;
            cb_G1_22_arima.Checked = false;
            cb_G1_23_hope.Checked = false;
            cb_G1_24_tokyo.Checked = false;
            cb_G1_25_jbcC.Checked = false;
            cb_G1_26_jbcS.Checked = false;
            cb_G1_27_jbcR.Checked = false;
            cb_G1_28_japanD.Checked = false;
            cb_G1_29_teio.Checked = false;
            #endregion
        }

        /// <summary>
        /// GⅡの全てにチェックを入れる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_G2_allOn_Click(object sender, EventArgs e)
        {
            #region 全部にチェック
            cb_G2_0_nikkeiSinsyun.Checked = true;
            cb_G2_1_tokai.Checked = true;
            cb_G2_2_amerika.Checked = true;
            cb_G2_3_kyoto.Checked = true;
            cb_G2_4_nakayama.Checked = true;
            cb_G2_5_tulip.Checked = true;
            cb_G2_6_yayoi.Checked = true;
            cb_G2_7_kinko.Checked = true;
            cb_G2_8_phillies.Checked = true;
            cb_G2_9_hanshinDai.Checked = true;
            cb_G2_10_spring.Checked = true;
            cb_G2_11_nikkeiPrize.Checked = true;
            cb_G2_12_hanshinUma.Checked = true;
            cb_G2_13_newZealand.Checked = true;
            cb_G2_14_milers.Checked = true;
            cb_G2_15_flora.Checked = true;
            cb_G2_16_aoba.Checked = true;
            cb_G2_17_kyotoSinb.Checked = true;
            cb_G2_18_keioSpr.Checked = true;
            cb_G2_19_meguro.Checked = true;
            cb_G2_20_sapporo.Checked = true;
            cb_G2_21_centaur.Checked = true;
            cb_G2_22_rose.Checked = true;
            cb_G2_23_sentlight.Checked = true;
            cb_G2_24_kobe.Checked = true;
            cb_G2_25_allComer.Checked = true;
            cb_G2_26_mainichi.Checked = true;
            cb_G2_27_kyotoDai.Checked = true;
            cb_G2_28_fuchuUma.Checked = true;
            cb_G2_29_fuji.Checked = true;
            cb_G2_30_swan.Checked = true;
            cb_G2_31_keioJunior.Checked = true;
            cb_G2_32_argentine.Checked = true;
            cb_G2_33_daily.Checked = true;
            cb_G2_34_staiyers.Checked = true;
            cb_G2_35_hanshinC.Checked = true;
            #endregion

        }

        /// <summary>
        /// GⅡの全てのチェックを外す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_G2_allOff_Click(object sender, EventArgs e)
        {
            #region 全部のチェックを外す
            cb_G2_0_nikkeiSinsyun.Checked = false;
            cb_G2_1_tokai.Checked = false;
            cb_G2_2_amerika.Checked = false;
            cb_G2_3_kyoto.Checked = false;
            cb_G2_4_nakayama.Checked = false;
            cb_G2_5_tulip.Checked = false;
            cb_G2_6_yayoi.Checked = false;
            cb_G2_7_kinko.Checked = false;
            cb_G2_8_phillies.Checked = false;
            cb_G2_9_hanshinDai.Checked = false;
            cb_G2_10_spring.Checked = false;
            cb_G2_11_nikkeiPrize.Checked = false;
            cb_G2_12_hanshinUma.Checked = false;
            cb_G2_13_newZealand.Checked = false;
            cb_G2_14_milers.Checked = false;
            cb_G2_15_flora.Checked = false;
            cb_G2_16_aoba.Checked = false;
            cb_G2_17_kyotoSinb.Checked = false;
            cb_G2_18_keioSpr.Checked = false;
            cb_G2_19_meguro.Checked = false;
            cb_G2_20_sapporo.Checked = false;
            cb_G2_21_centaur.Checked = false;
            cb_G2_22_rose.Checked = false;
            cb_G2_23_sentlight.Checked = false;
            cb_G2_24_kobe.Checked = false;
            cb_G2_25_allComer.Checked = false;
            cb_G2_26_mainichi.Checked = false;
            cb_G2_27_kyotoDai.Checked = false;
            cb_G2_28_fuchuUma.Checked = false;
            cb_G2_29_fuji.Checked = false;
            cb_G2_30_swan.Checked = false;
            cb_G2_31_keioJunior.Checked = false;
            cb_G2_32_argentine.Checked = false;
            cb_G2_33_daily.Checked = false;
            cb_G2_34_staiyers.Checked = false;
            cb_G2_35_hanshinC.Checked = false;
            #endregion
        }

        /// <summary>
        /// GⅢの全てにチェックを入れる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_G3_allOn_Click(object sender, EventArgs e)
        {
            #region 全部にチェック
            cb_G3_0_kyotokin.Checked = true;
            cb_G3_1_nakayamakin.Checked = true;
            cb_G3_2_sinzan.Checked = true;
            cb_G3_3_fairy.Checked = true;
            cb_G3_4_aichi.Checked = true;
            cb_G3_5_keiseihai.Checked = true;
            cb_G3_6_silkroad.Checked = true;
            cb_G3_7_negishi.Checked = true;
            cb_G3_8_kisaragi.Checked = true;
            cb_G3_9_tokyoShinbun.Checked = true;
            cb_G3_10_queenC.Checked = true;
            cb_G3_11_kyodoTushin.Checked = true;
            cb_G3_12_kyotoUma.Checked = true;
            cb_G3_13_diamond.Checked = true;
            cb_G3_14_kokuraDai.Checked = true;
            cb_G3_15_arinton.Checked = true;
            cb_G3_16_hankyu.Checked = true;
            cb_G3_17_osean.Checked = true;
            cb_G3_18_nakayamaUma.Checked = true;
            cb_G3_19_falcon.Checked = true;
            cb_G3_20_flower.Checked = true;
            cb_G3_21_mainichi.Checked = true;
            cb_G3_22_march.Checked = true;
            cb_G3_23_derbyKyo.Checked = true;
            cb_G3_24_antares.Checked = true;
            cb_G3_25_fukushima.Checked = true;
            cb_G3_26_nigataDai.Checked = true;
            cb_G3_27_heian.Checked = true;
            cb_G3_28_aoi.Checked = true;
            cb_G3_29_naruo.Checked = true;
            cb_G3_30_mermaid.Checked = true;
            cb_G3_31_epsom.Checked = true;
            cb_G3_32_unicorn.Checked = true;
            cb_G3_33_hakodateSpr.Checked = true;
            cb_G3_34_cbc.Checked = true;
            cb_G3_35_radioNIKKEI.Checked = true;
            cb_G3_36_procyon.Checked = true;
            cb_G3_37_tanabata.Checked = true;
            cb_G3_38_hakodateKinen.Checked = true;
            cb_G3_39_cyukyo.Checked = true;
            cb_G3_40_hakodateJunior.Checked = true;
            cb_G3_41_aibis.Checked = true;
            cb_G3_42_queenS.Checked = true;
            cb_G3_43_kokuraKinen.Checked = true;
            cb_G3_44_repard.Checked = true;
            cb_G3_45_sekiya.Checked = true;
            cb_G3_46_erum.Checked = true;
            cb_G3_47_kitakyusyu.Checked = true;
            cb_G3_48_nigataJunior.Checked = true;
            cb_G3_49_keenland.Checked = true;
            cb_G3_50_sapporoJunior.Checked = true;
            cb_G3_51_kokuraJunior.Checked = true;
            cb_G3_52_nigataKinen.Checked = true;
            cb_G3_53_shion.Checked = true;
            cb_G3_54_keiseihaiAutumn.Checked = true;
            cb_G3_55_sirius.Checked = true;
            cb_G3_56_saudiArabia.Checked = true;
            cb_G3_57_artemis.Checked = true;
            cb_G3_58_fantasy.Checked = true;
            cb_G3_59_miyako.Checked = true;
            cb_G3_60_musashino.Checked = true;
            cb_G3_61_fukushimaKinen.Checked = true;
            cb_G3_62_tospoJunior.Checked = true;
            cb_G3_63_kyotoJunior.Checked = true;
            cb_G3_64_keihan.Checked = true;
            cb_G3_65_challenge.Checked = true;
            cb_G3_66_chunichiShinbun.Checked = true;
            cb_G3_67_capella.Checked = true;
            cb_G3_68_turquoise.Checked = true;
            #endregion
        }

        /// <summary>
        /// GⅢの全てのチェックを外す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_G3_allOff_Click(object sender, EventArgs e)
        {
            #region 全部のチェックを外す
            cb_G3_0_kyotokin.Checked = false;
            cb_G3_1_nakayamakin.Checked = false;
            cb_G3_2_sinzan.Checked = false;
            cb_G3_3_fairy.Checked = false;
            cb_G3_4_aichi.Checked = false;
            cb_G3_5_keiseihai.Checked = false;
            cb_G3_6_silkroad.Checked = false;
            cb_G3_7_negishi.Checked = false;
            cb_G3_8_kisaragi.Checked = false;
            cb_G3_9_tokyoShinbun.Checked = false;
            cb_G3_10_queenC.Checked = false;
            cb_G3_11_kyodoTushin.Checked = false;
            cb_G3_12_kyotoUma.Checked = false;
            cb_G3_13_diamond.Checked = false;
            cb_G3_14_kokuraDai.Checked = false;
            cb_G3_15_arinton.Checked = false;
            cb_G3_16_hankyu.Checked = false;
            cb_G3_17_osean.Checked = false;
            cb_G3_18_nakayamaUma.Checked = false;
            cb_G3_19_falcon.Checked = false;
            cb_G3_20_flower.Checked = false;
            cb_G3_21_mainichi.Checked = false;
            cb_G3_22_march.Checked = false;
            cb_G3_23_derbyKyo.Checked = false;
            cb_G3_24_antares.Checked = false;
            cb_G3_25_fukushima.Checked = false;
            cb_G3_26_nigataDai.Checked = false;
            cb_G3_27_heian.Checked = false;
            cb_G3_28_aoi.Checked = false;
            cb_G3_29_naruo.Checked = false;
            cb_G3_30_mermaid.Checked = false;
            cb_G3_31_epsom.Checked = false;
            cb_G3_32_unicorn.Checked = false;
            cb_G3_33_hakodateSpr.Checked = false;
            cb_G3_34_cbc.Checked = false;
            cb_G3_35_radioNIKKEI.Checked = false;
            cb_G3_36_procyon.Checked = false;
            cb_G3_37_tanabata.Checked = false;
            cb_G3_38_hakodateKinen.Checked = false;
            cb_G3_39_cyukyo.Checked = false;
            cb_G3_40_hakodateJunior.Checked = false;
            cb_G3_41_aibis.Checked = false;
            cb_G3_42_queenS.Checked = false;
            cb_G3_43_kokuraKinen.Checked = false;
            cb_G3_44_repard.Checked = false;
            cb_G3_45_sekiya.Checked = false;
            cb_G3_46_erum.Checked = false;
            cb_G3_47_kitakyusyu.Checked = false;
            cb_G3_48_nigataJunior.Checked = false;
            cb_G3_49_keenland.Checked = false;
            cb_G3_50_sapporoJunior.Checked = false;
            cb_G3_51_kokuraJunior.Checked = false;
            cb_G3_52_nigataKinen.Checked = false;
            cb_G3_53_shion.Checked = false;
            cb_G3_54_keiseihaiAutumn.Checked = false;
            cb_G3_55_sirius.Checked = false;
            cb_G3_56_saudiArabia.Checked = false;
            cb_G3_57_artemis.Checked = false;
            cb_G3_58_fantasy.Checked = false;
            cb_G3_59_miyako.Checked = false;
            cb_G3_60_musashino.Checked = false;
            cb_G3_61_fukushimaKinen.Checked = false;
            cb_G3_62_tospoJunior.Checked = false;
            cb_G3_63_kyotoJunior.Checked = false;
            cb_G3_64_keihan.Checked = false;
            cb_G3_65_challenge.Checked = false;
            cb_G3_66_chunichiShinbun.Checked = false;
            cb_G3_67_capella.Checked = false;
            cb_G3_68_turquoise.Checked = false;
            #endregion
        }

        private void UACS_Shown(object sender, EventArgs e)
        {
            //更新があるか確認
            CheckUpdate();
        }

        /// <summary>
        /// 更新確認ボタンが押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_option_update_Click(object sender, EventArgs e)
        {
            //更新無しポップあり
            CheckUpdate(true);
        }


        //メッセージボックスを中央に表示するためのコード
        //参考:https://www.ipentec.com/document/csharp-show-message-box-in-center-of-owner-window
        private void SetHook(IWin32Window owner)
        {
            // フック設定
            IntPtr hInstance = WindowsAPI.GetWindowLong(this.Handle, (int)WindowsAPI.WindowLongParam.GWLP_HINSTANCE);
            IntPtr threadId = WindowsAPI.GetCurrentThreadId();
            HHook = WindowsAPI.SetWindowsHookEx((int)WindowsAPI.HookType.WH_CBT, new WindowsAPI.HOOKPROC(CBTProc), hInstance, threadId);
        }
        private IntPtr CBTProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == (int)WindowsAPI.HCBT.HCBT_ACTIVATE)
            {
                WindowsAPI.RECT rectOwner;
                WindowsAPI.RECT rectMsgBox;
                int x, y;

                // オーナーウィンドウの位置と大きさを取得
                WindowsAPI.GetWindowRect(this.Handle, out rectOwner);
                // MessageBoxの位置と大きさを取得
                WindowsAPI.GetWindowRect(wParam, out rectMsgBox);

                // MessageBoxの表示位置を計算
                x = rectOwner.Left + (rectOwner.Width - rectMsgBox.Width) / 2;
                y = rectOwner.Top + (rectOwner.Height - rectMsgBox.Height) / 2;

                //MessageBoxの位置を設定
                WindowsAPI.SetWindowPos(wParam, 0, x, y, 0, 0,
                  (uint)(WindowsAPI.SetWindowPosFlags.SWP_NOSIZE | WindowsAPI.SetWindowPosFlags.SWP_NOZORDER | WindowsAPI.SetWindowPosFlags.SWP_NOACTIVATE));

                // フック解除
                WindowsAPI.UnhookWindowsHookEx(HHook);
            }
            // 次のプロシージャへのポインタ
            return WindowsAPI.CallNextHookEx(HHook, nCode, wParam, lParam);
        }
    }
}
