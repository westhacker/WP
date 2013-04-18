using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;

namespace HelloWord
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        string data;
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            rectangle1.Visibility = System.Windows.Visibility.Visible;
            textBlock1.Visibility = System.Windows.Visibility.Visible;
            textBox1.Visibility = System.Windows.Visibility.Visible;
            textBlock2.Visibility = System.Windows.Visibility.Collapsed;
            textBox1.Focus();
            
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            savedata();
            rectangle1.Visibility = System.Windows.Visibility.Visible;
            textBlock1.Visibility = System.Windows.Visibility.Collapsed;
            textBox1.Visibility = System.Windows.Visibility.Collapsed;
            textBlock2.Visibility = System.Windows.Visibility.Visible;
            readdata();
            if (data == "xwy") textBlock2.Text = string.Format("徐伟月{0},你这个进化不完全的生命体,基因突变的外星人,幼稚园程度的高中生,先天蒙古症的青蛙头, 圣母峰雪人的弃婴,化粪池堵塞的凶手, 非洲人搞上黑猪的后裔,阴阳失调的黑猩猩, 被诺亚方舟压过的河马,和蟑螂共存活的超个体,生命力腐烂的半植物, 会发出臭味的垃圾人,上帝失手摔下来的旧洗衣机,沉积千年的腐植质,科学家也不敢研究的原始物种, 宇宙毁灭必备的原料, 找女朋友得去动物园甚至要离开地球,只要你抬头臭氧层就会破洞,要移民火星是为了要离开你,18辈子都没干好事才会认识你,连丢进太阳都嫌不够环保！", data);
            else if (data == "cxx") textBlock2.Text = string.Format("程晓行{0},你自从得了神经病，整个人都精神多了！", data);
            else if (data == "whb") textBlock2.Text = string.Format("王红宾{0},你会阻碍社会主义的发展！", data);
            else if (data == "nmm") textBlock2.Text = string.Format("南美美{0},生活就像超级女声，走到最后的都是纯爷们儿！", data);
            else textBlock2.Text = string.Format("白浩然{0},人傻不能复生啊！", data);
        }


        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            textBlock1.Visibility = System.Windows.Visibility.Collapsed;
            textBox1.Visibility = System.Windows.Visibility.Collapsed;
            textBlock2.Visibility = System.Windows.Visibility.Collapsed;
            rectangle1.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void savedata()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            string filename = "simple.txt";
            using(var file = appStorage.OpenFile(filename,System.IO.FileMode.OpenOrCreate,System.IO.FileAccess.Write)){
                using(var writer = new StreamWriter(file)){
                    writer.Write(textBox1.Text);
                }
            }
        }

        private void readdata()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            string filename = "simple.txt";
            using (StreamReader sr = new StreamReader(appStorage.OpenFile(filename, FileMode.Open, FileAccess.Read)))
            {
                data = sr.ReadToEnd();
            }

        }
    }
}