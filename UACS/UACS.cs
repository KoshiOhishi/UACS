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
            //label_version.Text = "version:" + version.Substring(0, version.LastIndexOf("."));

            //ツールチップテキスト
            menu_file_save.ToolTipText = "出力後に保存できるようになります。";
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

            //1文字以上出力されていたらSaveのコントロールを有効
            if (tb_Result.Text.Length > 0)
            {
                menu_file_save.Enabled = true;
                menu_file_save.ToolTipText = "出力済の出走レースリストを名前を付けて保存します。";
            }
            else
            {
                menu_file_save.Enabled = false;
                menu_file_save.ToolTipText = "出力後に保存できるようになります。";
            }
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

        /// <summary>
        /// 出力したデータを保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            //各種設定
            sfd.FileName = "UACS_output.txt";
            sfd.Filter = "txtファイル(*.txt)|*txt|すべてのファイル(*.*)|*.*";
            sfd.Title = "名前を付けて保存";
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream stream;
                stream = sfd.OpenFile();
                if (stream != null)
                {
                    StreamWriter sw = new StreamWriter(stream);
                    bool isError = false;
                    try
                    {
                        //ファイルに書き込む
                        sw.Write(tb_Result.Text);

                    }
                    catch (Exception ex)
                    {
                        isError = true;
                    }
                    finally
                    {
                        //閉じる
                        sw.Close();
                        stream.Close();
                    }

                    //ポップアップ表示
                    SetHook(this);
                    if (isError == true)
                    {
                        MessageBox.Show(this, "保存に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "保存が完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// 上級者モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_settings_hardmode_Click(object sender, EventArgs e)
        {
            //ON/OFF切り替え
            menu_tools_hardmode.Checked = !menu_tools_hardmode.Checked;

            //ONだったらチェックマーク表示
            if (menu_tools_hardmode.Checked)
            {
                menu_tools_hardmode.CheckState = CheckState.Checked;
            }
        }

        /// <summary>
        /// 更新を確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void menu_help_update_Click(object sender, EventArgs e)
        {
            //更新無しポップあり
            CheckUpdate(true);
        }

        /// <summary>
        /// 説明ページを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_help_explain_Click(object sender, EventArgs e)
        {
            //とりあえずGitHubのホームに飛ばす
            System.Diagnostics.Process.Start("https://github.com/KoshiOhishi/UACS");
        }

        /// <summary>
        /// 現在のバージョン表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_help_version_Click(object sender, EventArgs e)
        {
            //ポップアップ表示
            SetHook(this);
            MessageBox.Show(this, "UACS version " + version, "バージョン情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 未取得トロフィーデータ読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_file_trophyLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            //各種設定
            ofd.FileName = "UACS_Trophy.ngtd";
            ofd.Filter = "トロフィーデータファイル(*.ngtd)|*ngtd|すべてのファイル(*.*)|*.*";
            ofd.Title = "開く";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //チェック状況を読み込む
                if (LoadTrophyData(ofd.OpenFile()) == false)
                {
                    //ポップアップ表示
                    SetHook(this);
                    MessageBox.Show(this, "トロフィーデータの読み込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //ポップアップ表示
                    SetHook(this);
                    MessageBox.Show(this, "トロフィーデータを読み込みました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 未取得トロフィーデータ書き出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_file_trophySave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            //各種設定
            sfd.FileName = "UACS_Trophy.ngtd";
            sfd.Filter = "トロフィーデータファイル(*.ngtd)|*ngtd|すべてのファイル(*.*)|*.*";
            sfd.Title = "名前を付けて保存";
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream stream;
                stream = sfd.OpenFile();
                if (stream == null)
                {
                    //エラーポップアップ表示してリターン
                    SetHook(this);
                    MessageBox.Show(this, "トロフィーデータの書き込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //ファイルに書き込む
                BinaryWriter bw = new BinaryWriter(stream);
                //出力データ
                List<byte[]> output = GetTrophyData();
                bool isError = false;
                try
                {
                    //全要素書き込む
                    foreach (var v in output)
                    {
                        bw.Write(v);
                    }
                }
                catch(Exception ex)
                {
                    isError = true;
                }
                finally
                {
                    //閉じる
                    bw.Close();
                    stream.Close();
                }

                //ポップアップ表示
                if (isError == true)
                {
                    SetHook(this);
                    MessageBox.Show(this, "トロフィーデータの書き込みに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SetHook(this);
                    MessageBox.Show(this, "トロフィーデータを書き込みました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// トロフィーデータを読み込む
        /// </summary>
        /// <param name="stream">ファイルストリーム</param>
        /// <returns>成功したか否か</returns>
        private bool LoadTrophyData(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            //ヘッダー　整合性を取るため
            byte[] header = br.ReadBytes(8);
            //ロードに成功したか
            bool result = true;

            //整合性チェック
            if (Encoding.ASCII.GetString(header) != "UCASTROP")
            {
                br.Close();
                stream.Close();
                return false;
            }
            try
            {
                //データ部読み込み
                #region GⅠ
                cb_G1_0_feb.Checked = br.ReadBoolean();
                cb_G1_1_takam.Checked = br.ReadBoolean();
                cb_G1_2_osk.Checked = br.ReadBoolean();
                cb_G1_3_ouka.Checked = br.ReadBoolean();
                cb_G1_4_satsuki.Checked = br.ReadBoolean();
                cb_G1_5_tenSpr.Checked = br.ReadBoolean();
                cb_G1_6_nhk.Checked = br.ReadBoolean();
                cb_G1_7_vic.Checked = br.ReadBoolean();
                cb_G1_8_orc.Checked = br.ReadBoolean();
                cb_G1_9_der.Checked = br.ReadBoolean();
                cb_G1_10_yas.Checked = br.ReadBoolean();
                cb_G1_11_takara.Checked = br.ReadBoolean();
                cb_G1_12_spr.Checked = br.ReadBoolean();
                cb_G1_13_syu.Checked = br.ReadBoolean();
                cb_G1_14_kik.Checked = br.ReadBoolean();
                cb_G1_15_tenAut.Checked = br.ReadBoolean();
                cb_G1_16_eriz.Checked = br.ReadBoolean();
                cb_G1_17_milCS.Checked = br.ReadBoolean();
                cb_G1_18_japanC.Checked = br.ReadBoolean();
                cb_G1_19_champ.Checked = br.ReadBoolean();
                cb_G1_20_hanJF.Checked = br.ReadBoolean();
                cb_G1_21_asaFS.Checked = br.ReadBoolean();
                cb_G1_22_arima.Checked = br.ReadBoolean();
                cb_G1_23_hope.Checked = br.ReadBoolean();
                cb_G1_24_tokyo.Checked = br.ReadBoolean();
                cb_G1_25_jbcC.Checked = br.ReadBoolean();
                cb_G1_26_jbcS.Checked = br.ReadBoolean();
                cb_G1_27_jbcR.Checked = br.ReadBoolean();
                cb_G1_28_japanD.Checked = br.ReadBoolean();
                cb_G1_29_teio.Checked = br.ReadBoolean();
                #endregion
                #region GⅡ
                cb_G2_0_nikkeiSinsyun.Checked = br.ReadBoolean();
                cb_G2_1_tokai.Checked = br.ReadBoolean();
                cb_G2_2_amerika.Checked = br.ReadBoolean();
                cb_G2_3_kyoto.Checked = br.ReadBoolean();
                cb_G2_4_nakayama.Checked = br.ReadBoolean();
                cb_G2_5_tulip.Checked = br.ReadBoolean();
                cb_G2_6_yayoi.Checked = br.ReadBoolean();
                cb_G2_7_kinko.Checked = br.ReadBoolean();
                cb_G2_8_phillies.Checked = br.ReadBoolean();
                cb_G2_9_hanshinDai.Checked = br.ReadBoolean();
                cb_G2_10_spring.Checked = br.ReadBoolean();
                cb_G2_11_nikkeiPrize.Checked = br.ReadBoolean();
                cb_G2_12_hanshinUma.Checked = br.ReadBoolean();
                cb_G2_13_newZealand.Checked = br.ReadBoolean();
                cb_G2_14_milers.Checked = br.ReadBoolean();
                cb_G2_15_flora.Checked = br.ReadBoolean();
                cb_G2_16_aoba.Checked = br.ReadBoolean();
                cb_G2_17_kyotoSinb.Checked = br.ReadBoolean();
                cb_G2_18_keioSpr.Checked = br.ReadBoolean();
                cb_G2_19_meguro.Checked = br.ReadBoolean();
                cb_G2_20_sapporo.Checked = br.ReadBoolean();
                cb_G2_21_centaur.Checked = br.ReadBoolean();
                cb_G2_22_rose.Checked = br.ReadBoolean();
                cb_G2_23_sentlight.Checked = br.ReadBoolean();
                cb_G2_24_kobe.Checked = br.ReadBoolean();
                cb_G2_25_allComer.Checked = br.ReadBoolean();
                cb_G2_26_mainichi.Checked = br.ReadBoolean();
                cb_G2_27_kyotoDai.Checked = br.ReadBoolean();
                cb_G2_28_fuchuUma.Checked = br.ReadBoolean();
                cb_G2_29_fuji.Checked = br.ReadBoolean();
                cb_G2_30_swan.Checked = br.ReadBoolean();
                cb_G2_31_keioJunior.Checked = br.ReadBoolean();
                cb_G2_32_argentine.Checked = br.ReadBoolean();
                cb_G2_33_daily.Checked = br.ReadBoolean();
                cb_G2_34_staiyers.Checked = br.ReadBoolean();
                cb_G2_35_hanshinC.Checked = br.ReadBoolean();
                #endregion
                #region GⅢ
                cb_G3_0_kyotokin.Checked = br.ReadBoolean();
                cb_G3_1_nakayamakin.Checked = br.ReadBoolean();
                cb_G3_2_sinzan.Checked = br.ReadBoolean();
                cb_G3_3_fairy.Checked = br.ReadBoolean();
                cb_G3_4_aichi.Checked = br.ReadBoolean();
                cb_G3_5_keiseihai.Checked = br.ReadBoolean();
                cb_G3_6_silkroad.Checked = br.ReadBoolean();
                cb_G3_7_negishi.Checked = br.ReadBoolean();
                cb_G3_8_kisaragi.Checked = br.ReadBoolean();
                cb_G3_9_tokyoShinbun.Checked = br.ReadBoolean();
                cb_G3_10_queenC.Checked = br.ReadBoolean();
                cb_G3_11_kyodoTushin.Checked = br.ReadBoolean();
                cb_G3_12_kyotoUma.Checked = br.ReadBoolean();
                cb_G3_13_diamond.Checked = br.ReadBoolean();
                cb_G3_14_kokuraDai.Checked = br.ReadBoolean();
                cb_G3_15_arinton.Checked = br.ReadBoolean();
                cb_G3_16_hankyu.Checked = br.ReadBoolean();
                cb_G3_17_osean.Checked = br.ReadBoolean();
                cb_G3_18_nakayamaUma.Checked = br.ReadBoolean();
                cb_G3_19_falcon.Checked = br.ReadBoolean();
                cb_G3_20_flower.Checked = br.ReadBoolean();
                cb_G3_21_mainichi.Checked = br.ReadBoolean();
                cb_G3_22_march.Checked = br.ReadBoolean();
                cb_G3_23_derbyKyo.Checked = br.ReadBoolean();
                cb_G3_24_antares.Checked = br.ReadBoolean();
                cb_G3_25_fukushima.Checked = br.ReadBoolean();
                cb_G3_26_nigataDai.Checked = br.ReadBoolean();
                cb_G3_27_heian.Checked = br.ReadBoolean();
                cb_G3_28_aoi.Checked = br.ReadBoolean();
                cb_G3_29_naruo.Checked = br.ReadBoolean();
                cb_G3_30_mermaid.Checked = br.ReadBoolean();
                cb_G3_31_epsom.Checked = br.ReadBoolean();
                cb_G3_32_unicorn.Checked = br.ReadBoolean();
                cb_G3_33_hakodateSpr.Checked = br.ReadBoolean();
                cb_G3_34_cbc.Checked = br.ReadBoolean();
                cb_G3_35_radioNIKKEI.Checked = br.ReadBoolean();
                cb_G3_36_procyon.Checked = br.ReadBoolean();
                cb_G3_37_tanabata.Checked = br.ReadBoolean();
                cb_G3_38_hakodateKinen.Checked = br.ReadBoolean();
                cb_G3_39_cyukyo.Checked = br.ReadBoolean();
                cb_G3_40_hakodateJunior.Checked = br.ReadBoolean();
                cb_G3_41_aibis.Checked = br.ReadBoolean();
                cb_G3_42_queenS.Checked = br.ReadBoolean();
                cb_G3_43_kokuraKinen.Checked = br.ReadBoolean();
                cb_G3_44_repard.Checked = br.ReadBoolean();
                cb_G3_45_sekiya.Checked = br.ReadBoolean();
                cb_G3_46_erum.Checked = br.ReadBoolean();
                cb_G3_47_kitakyusyu.Checked = br.ReadBoolean();
                cb_G3_48_nigataJunior.Checked = br.ReadBoolean();
                cb_G3_49_keenland.Checked = br.ReadBoolean();
                cb_G3_50_sapporoJunior.Checked = br.ReadBoolean();
                cb_G3_51_kokuraJunior.Checked = br.ReadBoolean();
                cb_G3_52_nigataKinen.Checked = br.ReadBoolean();
                cb_G3_53_shion.Checked = br.ReadBoolean();
                cb_G3_54_keiseihaiAutumn.Checked = br.ReadBoolean();
                cb_G3_55_sirius.Checked = br.ReadBoolean();
                cb_G3_56_saudiArabia.Checked = br.ReadBoolean();
                cb_G3_57_artemis.Checked = br.ReadBoolean();
                cb_G3_58_fantasy.Checked = br.ReadBoolean();
                cb_G3_59_miyako.Checked = br.ReadBoolean();
                cb_G3_60_musashino.Checked = br.ReadBoolean();
                cb_G3_61_fukushimaKinen.Checked = br.ReadBoolean();
                cb_G3_62_tospoJunior.Checked = br.ReadBoolean();
                cb_G3_63_kyotoJunior.Checked = br.ReadBoolean();
                cb_G3_64_keihan.Checked = br.ReadBoolean();
                cb_G3_65_challenge.Checked = br.ReadBoolean();
                cb_G3_66_chunichiShinbun.Checked = br.ReadBoolean();
                cb_G3_67_capella.Checked = br.ReadBoolean();
                cb_G3_68_turquoise.Checked = br.ReadBoolean();
                #endregion
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                //クローズ
                br.Close();
                stream.Close();
            }

            return result;
        }

        /// <summary>
        /// 現在のチェック状況から保存する文字列を取得
        /// </summary>
        /// <returns>保存する文字列</returns>
        private List<byte[]> GetTrophyData()
        {
            //書き込むバイトコンテナ
            List<byte[]> result = new List<byte[]>();
            //ヘッダー記述
            result.Add(Encoding.ASCII.GetBytes("UCASTROP"));

            //ここからデータ部
            #region GⅠ
            result.Add(BitConverter.GetBytes(cb_G1_0_feb.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_1_takam.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_2_osk.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_3_ouka.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_4_satsuki.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_5_tenSpr.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_6_nhk.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_7_vic.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_8_orc.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_9_der.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_10_yas.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_11_takara.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_12_spr.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_13_syu.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_14_kik.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_15_tenAut.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_16_eriz.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_17_milCS.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_18_japanC.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_19_champ.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_20_hanJF.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_21_asaFS.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_22_arima.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_23_hope.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_24_tokyo.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_25_jbcC.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_26_jbcS.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_27_jbcR.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_28_japanD.Checked));
            result.Add(BitConverter.GetBytes(cb_G1_29_teio.Checked));
            #endregion
            #region GⅡ
            result.Add(BitConverter.GetBytes(cb_G2_0_nikkeiSinsyun.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_1_tokai.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_2_amerika.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_3_kyoto.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_4_nakayama.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_5_tulip.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_6_yayoi.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_7_kinko.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_8_phillies.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_9_hanshinDai.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_10_spring.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_11_nikkeiPrize.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_12_hanshinUma.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_13_newZealand.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_14_milers.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_15_flora.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_16_aoba.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_17_kyotoSinb.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_18_keioSpr.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_19_meguro.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_20_sapporo.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_21_centaur.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_22_rose.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_23_sentlight.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_24_kobe.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_25_allComer.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_26_mainichi.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_27_kyotoDai.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_28_fuchuUma.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_29_fuji.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_30_swan.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_31_keioJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_32_argentine.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_33_daily.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_34_staiyers.Checked));
            result.Add(BitConverter.GetBytes(cb_G2_35_hanshinC.Checked));
            #endregion
            #region GⅢ
            result.Add(BitConverter.GetBytes(cb_G3_0_kyotokin.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_1_nakayamakin.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_2_sinzan.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_3_fairy.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_4_aichi.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_5_keiseihai.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_6_silkroad.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_7_negishi.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_8_kisaragi.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_9_tokyoShinbun.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_10_queenC.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_11_kyodoTushin.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_12_kyotoUma.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_13_diamond.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_14_kokuraDai.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_15_arinton.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_16_hankyu.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_17_osean.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_18_nakayamaUma.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_19_falcon.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_20_flower.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_21_mainichi.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_22_march.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_23_derbyKyo.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_24_antares.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_25_fukushima.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_26_nigataDai.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_27_heian.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_28_aoi.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_29_naruo.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_30_mermaid.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_31_epsom.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_32_unicorn.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_33_hakodateSpr.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_34_cbc.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_35_radioNIKKEI.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_36_procyon.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_37_tanabata.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_38_hakodateKinen.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_39_cyukyo.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_40_hakodateJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_41_aibis.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_42_queenS.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_43_kokuraKinen.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_44_repard.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_45_sekiya.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_46_erum.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_47_kitakyusyu.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_48_nigataJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_49_keenland.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_50_sapporoJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_51_kokuraJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_52_nigataKinen.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_53_shion.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_54_keiseihaiAutumn.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_55_sirius.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_56_saudiArabia.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_57_artemis.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_58_fantasy.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_59_miyako.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_60_musashino.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_61_fukushimaKinen.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_62_tospoJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_63_kyotoJunior.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_64_keihan.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_65_challenge.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_66_chunichiShinbun.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_67_capella.Checked));
            result.Add(BitConverter.GetBytes(cb_G3_68_turquoise.Checked));
            #endregion
            return result;
        }
    }
}
