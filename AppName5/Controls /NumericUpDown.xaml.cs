using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppName5.Controls
{
	public partial class NumericUpDown : ContentView
	{
		#region Value BindableProperty
		public static readonly BindableProperty ValueProperty =
			BindableProperty.Create(
				nameof(Value),          // 対象のプロパティ名 (文字列)
				typeof(double),         // 対象のプロパティの型
				typeof(NumericUpDown),  // プロパティを定義する型（自クラス）
				0.0,                    // プロパティのデフォルト値
				propertyChanged: (bindable, oldValue, newValue) =>
				{ // 変更通知ハンドラ
				((NumericUpDown)bindable).Value = (double)newValue;
				},
				defaultBindingMode: BindingMode.TwoWay  // デフォルトのバインディングモード
			);

		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set
			{
				SetValue(ValueProperty, value);
				// 値が変更されるたびに Label のテキストを変更する
				this.valueLabel.Text = value.ToString();
			}
		}
		#endregion

		public NumericUpDown()
		{
			InitializeComponent();
			this.valueLabel.Text = Value.ToString(); // 初期テキストをセット
		}

		// 以下はボタンのイベントハンドラ定義

		void OnUpClicked(object sender, EventArgs e)
		{
			this.Value += 1;
		}

		void OnDownClicked(object sender, EventArgs e)
		{
			this.Value -= 1;
		}
	}
}
