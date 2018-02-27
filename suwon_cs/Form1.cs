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
using System.Threading;
using System.Media;
namespace suwon_cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string url = "";
        CookieContainer cookies = new CookieContainer();
        private void button1_Click(object sender, EventArgs e)
        {
            wp = new SoundPlayer("./mp3/water.wav");
            wp.PlaySync();
            
            statusStrip1.Items[0].ForeColor = Color.Blue;
            statusStrip1.Items[0].Text = "로그인중";
            string userid = textBox1.Text;
            string userpw = textBox2.Text;

            WebRequest request = WebRequest.Create("https://sugang.suwon.ac.kr/sugang/login?attribute=login");
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Method = "POST";
            ((HttpWebRequest)request).CookieContainer = new CookieContainer();
            string Data = string.Format("userid={0}&password={1}", userid, userpw);
            byte[] sendData = UTF8Encoding.UTF8.GetBytes(Data);
            request.ContentLength = sendData.Length;
            Stream reqs = request.GetRequestStream();
            reqs.Write(sendData, 0, sendData.Length);
            reqs.Close();
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string str_parse = sr.ReadToEnd();
            richTextBox1.Text = str_parse;

            try
            {
                str_parse = str_parse.Split('\"')[77];
            }
            catch
            {
                string err_msg = str_parse.Split(new string[] { "alert(\"" }, StringSplitOptions.None)[1];
                err_msg = err_msg.Split('\"')[0];
                statusStrip1.Items[0].ForeColor = Color.Red;
                statusStrip1.Items[0].Text = string.Format("로그인 실패 : {0}", err_msg);

                return;
            }
            string url_menu = "https://sugang.suwon.ac.kr" + str_parse;
            WebRequest req = WebRequest.Create(url_menu);
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)req).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Method = "GET";
            ((HttpWebRequest)req).CookieContainer = ((HttpWebRequest)request).CookieContainer;

            resp = (HttpWebResponse)req.GetResponse();
            sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

            str_parse = sr.ReadToEnd();
            richTextBox1.Text = str_parse;
            str_parse = str_parse.Split(new string[] { "\" 	수강신청\", \"" }, StringSplitOptions.None)[1];
            str_parse = str_parse.Split('\"')[0];
            string url_sugang = "https://sugang.suwon.ac.kr" + str_parse;
            richTextBox1.Text = str_parse;

            WebRequest req2 = WebRequest.Create(url_sugang);
            req2.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req2.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)req2).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            req2.Method = "GET";
            ((HttpWebRequest)req2).CookieContainer = ((HttpWebRequest)req).CookieContainer;

            resp = (HttpWebResponse)req2.GetResponse();
            sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

            str_parse = sr.ReadToEnd();
            richTextBox1.Text = str_parse;

            str_parse = str_parse.Split(new string[] { "var OpenWin = \"" }, StringSplitOptions.None)[1];
            str_parse = str_parse.Split('\"')[0];
            string url_sugang_page = "https://sugang.suwon.ac.kr" + str_parse;

            WebRequest req3 = WebRequest.Create(url_sugang_page);
            req3.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req3.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)req3).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            req3.Method = "GET";
            ((HttpWebRequest)req3).CookieContainer = ((HttpWebRequest)req2).CookieContainer;

            resp = (HttpWebResponse)req3.GetResponse();
            sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

            str_parse = sr.ReadToEnd();
            richTextBox1.Text = str_parse;

            if (str_parse.IndexOf("alert") > 0)
            {
                string err_msg = str_parse.Split(new string[] { "alert(\"" }, StringSplitOptions.None)[1];
                err_msg = err_msg.Split('\"')[0];
                richTextBox1.Text = err_msg;
                statusStrip1.Items[0].Text = string.Format("오류 메시지 출력");

                return;
            }
            string url_gwamok = str_parse.Split(new string[] { "name=\"gwamok\"	scrolling=\"auto\" marginwidth=\"10\"  marginheight=\"0\"  src=\"" }, StringSplitOptions.None)[1];
            url_gwamok = "https://sugang.suwon.ac.kr" + url_gwamok.Split('\"')[0];
            url = url_gwamok;
            WebRequest req4 = WebRequest.Create(url_gwamok);
            req4.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req4.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)req4).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            req4.Method = "GET";
            ((HttpWebRequest)req4).CookieContainer = ((HttpWebRequest)req3).CookieContainer;

            resp = (HttpWebResponse)req4.GetResponse();
            sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            cookies = ((HttpWebRequest)req4).CookieContainer;
            str_parse = sr.ReadToEnd();
            richTextBox1.Text = str_parse;

            sr.Close();
            resp.Close();
            statusStrip1.Items[0].ForeColor = Color.Green;
            statusStrip1.Items[0].Text = "로그인 완료 - 과목을 조회한 후 원하는 과목을 선택후 신청버튼을 누릅니다.";
            button2.Enabled = true;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        string sReload = "0";
        string orgGroupCd = "201";
        string univCd = "";
        string dpmjCd = "";
        string young = "";
        string mjorCd = "";
        string gradeCd = "3";
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                wp = new SoundPlayer("./mp3/water.wav");
                wp.PlaySync();
            }
            if (comboBox1.Text == "교양교직")
            {
                univCd = "2000001";
            }
            else if (comboBox1.Text == "교양대학")
            {
                univCd = "2000279";
            }
            else if (comboBox1.Text == "IT대학")
            {
                univCd = "2000208";
            }
            else if (comboBox1.Text == "ICT융합대학")
            {
                univCd = "2000510";
            }

            if (comboBox2.Text == "교양교직 (주)")
            {
                dpmjCd = "2000002";
            }
            else if (comboBox2.Text == "교양교직 (야)")
            {
                dpmjCd = "2000003";
            }
            else if (comboBox2.Text == "연계전공 (주)")
            {
                dpmjCd = "2000004";
            }
            else if (comboBox2.Text == "교양 (주)" || comboBox2.Text == "교양 (야)")
            {
                if (comboBox2.Text == "교양 (주)")
                    dpmjCd = "2000286";
                else
                {
                    dpmjCd = "2000287";
                }
                if (comboBox3.Text == "전체영역")
                {
                    young = "";
                }
                else if (comboBox3.Text == "영역없음")
                {
                    young = "";
                }
                else if (comboBox3.Text == "1영역(글로벌의사소통)")
                {
                    young = "31";
                }
                else if (comboBox3.Text == "2영역(문화현상과 현대문명)")
                {
                    young = "32";
                }
                else if (comboBox3.Text == "3영역(역사와 사회적 현실)")
                {
                    young = "33";
                }
                else if (comboBox3.Text == "4영역(인간과 지성)")
                {
                    young = "34";
                }
                else if (comboBox3.Text == "5영역(자연 및 과학)")
                {
                    young = "35";
                }
                else if (comboBox3.Text == "6영역(예술과 건강)")
                {
                    young = "36";
                }



            }
            else if (comboBox2.Text == "연계전공 (주)")
            {
                dpmjCd = "2000004";

                if (comboBox3.Text == "실내건축디자인")
                {
                    mjorCd = "2000005";
                }
                else if (comboBox2.Text == "e-비지니스")
                {
                    mjorCd = "2000006";
                }
                else if (comboBox2.Text == "국제관계학")
                {
                    mjorCd = "2000007";
                }
                else if (comboBox2.Text == "빅데이터융합")
                {
                    mjorCd = "2000509";
                }
            }
            else if (comboBox2.Text == "컴퓨터학과 (주)")
            {
                dpmjCd = "2000209";
            }
            else if (comboBox2.Text == "정보통신공학과 (주)")
            {
                dpmjCd = "2000210";
            }
            else if (comboBox2.Text == "정보보호학과 (주)")
            {
                dpmjCd = "2000212";
            }
            else if (comboBox2.Text == "정보미디어학과 (주)")
            {
                dpmjCd = "2000213";
            }
            else if (comboBox2.Text == "컴퓨터미디어학부 (주)")
            {
                dpmjCd = "2000329";
            }
            else if (comboBox2.Text == "정보통신보안학부 (주)")
            {
                dpmjCd = "2000332";
            }
            else if (comboBox2.Text == "수학과 (주)")
            {
                dpmjCd = "2000422";
            }
            else if (comboBox1.Text == "IT대학" && comboBox2.Text == "컴퓨터학부 (주)")
            {
                dpmjCd = "2000423";
            }
            else if (comboBox1.Text == "IT대학" && comboBox2.Text == "정보통신학부 (주)")
            {
                dpmjCd = "2000426";
            }
            else if (comboBox2.Text == "데이터과학부 (주)")
            {
                dpmjCd = "2000511";
            }
            else if (comboBox1.Text == "ICT융합대학" && comboBox2.Text == "컴퓨터학부 (주)")
            {
                dpmjCd = "2000512";
                if (comboBox3.Text == "미디어SW")
                {
                    mjorCd = "2000515";
                }
                else if (comboBox3.Text == "컴퓨터SW")
                {
                    mjorCd = "2000514";
                }
            }
            else if (comboBox1.Text == "ICT융합대학" && comboBox2.Text == "정보통신학부 (주)")
            {
                dpmjCd = "2000513";
                if (comboBox3.Text == "정보통신")
                {
                    mjorCd = "2000516";
                }
                else if (comboBox3.Text == "정보보호")
                {
                    mjorCd = "2000517";
                }
            }


            if (comboBox4.Text == "전체")
            {
                gradeCd = "";
            }
            else
            {
                gradeCd = comboBox4.Text.Split(new string[] { "학년" }, StringSplitOptions.None)[0];

            }



            string form_data = string.Format("sReload={0}&orgGroupCd={1}&univCd={2}&dpmjCd={3}&young={4}&mjorCd={5}&gradeCd={6}", sReload, orgGroupCd, univCd, dpmjCd, young, mjorCd, gradeCd);

            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Method = "POST";
            ((HttpWebRequest)request).CookieContainer = cookies;
            byte[] sendData = UTF8Encoding.UTF8.GetBytes(form_data);
            request.ContentLength = sendData.Length;
            Stream reqs = request.GetRequestStream();
            reqs.Write(sendData, 0, sendData.Length);
            reqs.Close();
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string str_parse = sr.ReadToEnd();
            richTextBox1.Text = str_parse;
            int index = -1;
            if (listView1.SelectedIndices.Count > 0)
                index = listView1.SelectedIndices[0];

            listView1.Items.Clear();
            string[] arr = str_parse.Split(new string[] { "<td align=\"center\" width=\"4%\" class=\"se_input\">" }, StringSplitOptions.None);
            for (int i = 1; i < arr.Length; i++)
            {
                string img = arr[i].Split(new string[] { "'/static/image/btn/" }, StringSplitOptions.None)[1];
                string gooboon = arr[i].Split(new string[] { "&nbsp;" }, StringSplitOptions.None)[1];
                string sub = arr[i].Split(new string[] { "&nbsp;" }, StringSplitOptions.None)[3];
                string prof = arr[i].Split(new string[] { "&nbsp;" }, StringSplitOptions.None)[10];
                string when = arr[i].Split(new string[] { "&nbsp;" }, StringSplitOptions.None)[12];
                string pars = "";
                img = img.Split('\'')[0];
                if (img.IndexOf("exceed_btn") > -1)
                {
                    img = "초과";
                }
                else
                {
                    img = "가능";
                    pars = arr[i].Split(new string[] { "save_it(" }, StringSplitOptions.None)[1];
                    pars = pars.Split(')')[0];

                }
                gooboon = gooboon.Split('\t')[0];
                sub = sub.Split('<')[0];
                prof = prof.Split('<')[0];
                when = when.Split('<')[0];

                ListViewItem item = new ListViewItem(img);
                item.SubItems.Add(gooboon);
                item.SubItems.Add(sub);
                item.SubItems.Add(prof);
                item.SubItems.Add(when);
                item.SubItems.Add(pars);
                if (item.Text == "가능")
                {
                    item.ForeColor = Color.Green;
                }
                else
                {
                    item.ForeColor = Color.Red;
                }
                listView1.Items.Add(item);
            }
            if (index > -1)
            {
                listView1.Items[index].Selected = true;
                listView1.Select();
            }

            if (button3.Enabled == false)
                button3.Enabled = true;
        }
        Thread th;
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "신청시작")
            {
                button3.Text = "신청중지";
                th = new Thread(start_sugang);
                try
                {
                    th.Start();

                }
                catch
                {
                    MessageBox.Show("오류가 발생했습니다. 프로그램을 다시 실행시켜주세요.");
                }
            }
            else
            {
                button3.Text = "신청시작";
                statusStrip1.Items[0].ForeColor = Color.Orange;
                statusStrip1.Items[0].Text = "수강신청 대기중";
                try
                {
                    th.Abort();
                }
                catch
                {
                    MessageBox.Show("오류가 발생했습니다. 프로그램을 다시 실행시켜주세요.");
                }

            }

        }
        delegate void callback();

        public void start_sugang()
        {

            while (true)
            {
                statusStrip1.Items[0].ForeColor = Color.Purple;
                statusStrip1.Items[0].Text = string.Format("{0} 수강신청중..", listView1.SelectedItems[0].SubItems[2].Text);
                if (listView1.SelectedItems.Count < 1)
                {
                    ;
                }
                else if (listView1.SelectedItems[0].Text == "가능")
                {
                    string str = listView1.SelectedItems[0].SubItems[5].Text;
                    statusStrip1.Items[0].Text = str;
                    string url_post = str.Split('\'')[1].Split('=')[1];
                    string dpmj_cd = str.Split('\'')[5];
                    string fac_dvcd = str.Split('\'')[7];
                    string subjt_cd = str.Split('\'')[9];
                    string dicl_no = str.Split('\'')[11];
                    string chk = "1";
                    string form_data = string.Format("url={0}&dpmj_cd={1}&fac_dvcd={2}&subjt_cd={3}&dicl_no={4}&chk={5}", url_post, dpmj_cd, fac_dvcd, subjt_cd, dicl_no, chk);
                    form_data = "https://sugang.suwon.ac.kr/sugang/filter?" + form_data;

                    WebRequest request = WebRequest.Create(form_data);
                    request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    request.Credentials = CredentialCache.DefaultCredentials;
                    ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    request.Method = "POST";
                    ((HttpWebRequest)request).CookieContainer = cookies;
                    byte[] sendData = UTF8Encoding.UTF8.GetBytes(form_data);
                    request.ContentLength = sendData.Length;
                    Stream reqs = request.GetRequestStream();
                    reqs.Write(sendData, 0, sendData.Length);
                    reqs.Close();
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                    string str_parse = sr.ReadToEnd();
                    richTextBox1.Text = str_parse;

                    chk = "2";
                    string final_url = "https://sugang.suwon.ac.kr/sugang/filter?" + string.Format("url={0}&subjt_cd={1}&dicl_no={2}&dpmj_cd={3}&fac_dvcd={4}&chk={5}", url_post, subjt_cd, dicl_no, dpmj_cd, fac_dvcd, chk);
                    statusStrip1.Items[0].Text = final_url;

                    WebRequest req2 = WebRequest.Create(final_url);
                    req2.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req2.Credentials = CredentialCache.DefaultCredentials;
                    ((HttpWebRequest)req2).UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    req2.Method = "GET";
                    ((HttpWebRequest)req2).CookieContainer = ((HttpWebRequest)request).CookieContainer;

                    resp = (HttpWebResponse)req2.GetResponse();
                    sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

                    str_parse = sr.ReadToEnd();
                    richTextBox1.Text = str_parse;

                    if (str_parse.IndexOf("저장") > -1)
                    {
                        statusStrip1.Items[0].ForeColor = Color.SkyBlue;
                        statusStrip1.Items[0].Text = "수강신청에 성공하였습니다.";
                        button3.Text = "신청시작";
                        wp = new SoundPlayer("./mp3/call.wav");
                        wp.PlaySync();
                        return;
                    }
                    else
                    {
                        statusStrip1.Items[0].ForeColor = Color.Red;
                        MessageBox.Show("수강신청 실패 (원인을 직접 확인할 것)");
                        button3.Text = "신청시작";
                        return;
                    }


                }
                else
                {
                    button2_Click(null, null);

                }
                Thread.Sleep(1000);

            }

        }
        SoundPlayer wp;
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SendToBack();
            checkBox1.Checked = true;
            statusStrip1.Items[0].ForeColor = Color.Orange;
            comboBox1.Items.Add("교양교직");
            comboBox1.Items.Add("교양대학");
            comboBox1.Items.Add("IT대학");
            comboBox1.Items.Add("ICT융합대학");
            comboBox4.Items.Add("전체");
            comboBox4.Items.Add("1학년");
            comboBox4.Items.Add("2학년");
            comboBox4.Items.Add("3학년");
            comboBox4.Items.Add("4학년");

            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();

            if (comboBox1.Text == "교양교직")
            {
                comboBox2.Items.Add("교양교직 (주)");
                comboBox2.Items.Add("교양교직 (야)");
                comboBox2.Items.Add("연계전공 (주)");
            }
            else if (comboBox1.Text == "교양대학")
            {
                comboBox2.Items.Add("교양 (주)");
                comboBox2.Items.Add("교양 (야)");
            }
            else if (comboBox1.Text == "IT대학")
            {
                comboBox2.Items.Add("컴퓨터학과 (주)");
                comboBox2.Items.Add("정보통신공학과 (주)");
                comboBox2.Items.Add("정보보호학과 (주)");
                comboBox2.Items.Add("정보미디어학과 (주)");
                comboBox2.Items.Add("컴퓨터미디어학부 (주)");
                comboBox2.Items.Add("정보통신보안학부 (주)");
                comboBox2.Items.Add("수학과 (주)");
                comboBox2.Items.Add("컴퓨터학부 (주)");
                comboBox2.Items.Add("정보통신학부 (주)");
            }
            else if (comboBox1.Text == "ICT융합대학")
            {
                comboBox2.Items.Add("데이터과학부 (주)");
                comboBox2.Items.Add("컴퓨터학부 (주)");
                comboBox2.Items.Add("정보통신학부 (주)");
            }
            else
            {

                comboBox2.Items.Add("학부선택");
            }


            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            if (comboBox1.Text == "교양교직")
            {
                if (comboBox2.Text == "교양교직 (주)")
                {

                }
                else if (comboBox2.Text == "연계전공 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("실내건축디자인");
                    comboBox3.Items.Add("e-비지니스");
                    comboBox3.Items.Add("국제관계학");
                    comboBox3.Items.Add("빅데이터융합");
                    comboBox3.SelectedIndex = 0;
                }
            }
            else if (comboBox1.Text == "교양대학")
            {
                comboBox3.Items.Add("전체영역");
                comboBox3.Items.Add("영역없음");
                comboBox3.Items.Add("1영역(글로벌의사소통)");
                comboBox3.Items.Add("2영역(문화현상과 현대문명)");
                comboBox3.Items.Add("3영역(역사와 사회적 현실)");
                comboBox3.Items.Add("4영역(인간과 지성)");
                comboBox3.Items.Add("5영역(자연 및 과학)");
                comboBox3.Items.Add("6영역(예술과 건강)");

                comboBox3.SelectedIndex = 0;
            }
            else if (comboBox1.Text == "IT대학")
            {
                if (comboBox2.Text == "컴퓨터미디어학부 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("컴퓨터학");
                    comboBox3.Items.Add("정보미디어학");
                }
                else if (comboBox2.Text == "정보통신보안학부 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("정보통신공학");
                    comboBox3.Items.Add("정보보호학");
                }
                else if (comboBox2.Text == "컴퓨터학부 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("컴퓨터SW");
                    comboBox3.Items.Add("미디오SW");
                }
                else if (comboBox2.Text == "정보통신학부 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("정보통신");
                    comboBox3.Items.Add("정보보호");
                }
                else
                {
                    comboBox3.Items.Add("전체");
                }
                comboBox3.SelectedIndex = 0;
            }
            else if (comboBox1.Text == "ICT융합대학")
            {
                if (comboBox2.Text == "컴퓨터학부 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("컴퓨터SW");
                    comboBox3.Items.Add("미디오SW");
                }
                else if (comboBox2.Text == "정보통신학부 (주)")
                {
                    comboBox3.Items.Add("전체");
                    comboBox3.Items.Add("정보통신");
                    comboBox3.Items.Add("정보보호");
                }
                else
                {
                    comboBox3.Items.Add("전체");
                }
                comboBox3.SelectedIndex = 0;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }
        
        

        private void textBox2_Enter(object sender, EventArgs e)
        {

            textBox2.Text = "";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";

        }
    }
}
