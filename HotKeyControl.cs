using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EditHotKeyControl
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EditHotKeyControl"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EditHotKeyControl;assembly=EditHotKeyControl"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = TextKey, Type = typeof(TextBox))]
    public class HotKeyControl : Control
    {
        private const string TextKey = "TextKey";
        private TextBox textKey;

        public string Text { set { this.textKey.Text = value;} }
        static HotKeyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HotKeyControl), new FrameworkPropertyMetadata(typeof(HotKeyControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (textKey != null)
            {
                textKey.KeyDown -= KeyDown;
            }

            textKey = (TextBox)GetTemplateChild(TextKey);
            if (textKey != null)
            {
                textKey.KeyDown += KeyDown;
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder modKeyStr = new StringBuilder();
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                modKeyStr.Append("Ctrl");
            }
            if ((Keyboard.Modifiers & ModifierKeys.Alt) > 0)
            {
                modKeyStr.Append(modKeyStr.Length > 0 ? " + Alt" : "Alt");
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                modKeyStr.Append(modKeyStr.Length > 0 ? " + Shift" : "Shift");
            }
            Key key = e.Key;

            string keyStr = key.ToString();
            if ((int)key >= 116 && (int)key <= 121)
            {
                key = Key.None;
                keyStr = string.Empty;
            }
            textKey.Text = modKeyStr.ToString() + ((modKeyStr.Length > 0 && key != Key.None) ? " + " : "") + keyStr;
        }
    }
}
