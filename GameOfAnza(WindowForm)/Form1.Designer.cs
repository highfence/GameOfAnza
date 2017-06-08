namespace GameOfAnza_WindowForm_
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.SearchBox = new System.Windows.Forms.TextBox();
			this.SearchInstLabel = new System.Windows.Forms.Label();
			this.SearchButton = new System.Windows.Forms.Button();
			this.ResultBox = new System.Windows.Forms.ListBox();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.AnzaImage = new System.Windows.Forms.PictureBox();
			this.AnzaScoreLabel = new System.Windows.Forms.Label();
			this.TabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AnzaImage)).BeginInit();
			this.SuspendLayout();
			// 
			// SearchBox
			// 
			this.SearchBox.Location = new System.Drawing.Point(26, 34);
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new System.Drawing.Size(206, 21);
			this.SearchBox.TabIndex = 0;
			this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
			this.SearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyDown);
			// 
			// SearchInstLabel
			// 
			this.SearchInstLabel.AutoSize = true;
			this.SearchInstLabel.Location = new System.Drawing.Point(12, 12);
			this.SearchInstLabel.Name = "SearchInstLabel";
			this.SearchInstLabel.Size = new System.Drawing.Size(169, 12);
			this.SearchInstLabel.TabIndex = 1;
			this.SearchInstLabel.Text = "찾으시는 노선을 입력해주세요";
			// 
			// SearchButton
			// 
			this.SearchButton.Location = new System.Drawing.Point(238, 34);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(75, 23);
			this.SearchButton.TabIndex = 2;
			this.SearchButton.Text = "Search";
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// ResultBox
			// 
			this.ResultBox.FormattingEnabled = true;
			this.ResultBox.ItemHeight = 12;
			this.ResultBox.Location = new System.Drawing.Point(26, 54);
			this.ResultBox.Name = "ResultBox";
			this.ResultBox.Size = new System.Drawing.Size(206, 100);
			this.ResultBox.TabIndex = 3;
			this.ResultBox.SelectedIndexChanged += new System.EventHandler(this.ResultBox_SelectedIndexChanged);
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.tabPage1);
			this.TabControl.Controls.Add(this.tabPage2);
			this.TabControl.Location = new System.Drawing.Point(26, 63);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(1146, 686);
			this.TabControl.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.AnzaScoreLabel);
			this.tabPage1.Controls.Add(this.AnzaImage);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1138, 660);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "앉아점수";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1138, 660);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "노선별 점수";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// AnzaImage
			// 
			this.AnzaImage.Location = new System.Drawing.Point(182, 32);
			this.AnzaImage.Name = "AnzaImage";
			this.AnzaImage.Size = new System.Drawing.Size(175, 175);
			this.AnzaImage.TabIndex = 0;
			this.AnzaImage.TabStop = false;
			// 
			// AnzaScoreLabel
			// 
			this.AnzaScoreLabel.AutoSize = true;
			this.AnzaScoreLabel.Font = new System.Drawing.Font("D2Coding", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AnzaScoreLabel.Location = new System.Drawing.Point(409, 94);
			this.AnzaScoreLabel.Name = "AnzaScoreLabel";
			this.AnzaScoreLabel.Size = new System.Drawing.Size(168, 55);
			this.AnzaScoreLabel.TabIndex = 1;
			this.AnzaScoreLabel.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.TabControl);
			this.Controls.Add(this.ResultBox);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.SearchInstLabel);
			this.Controls.Add(this.SearchBox);
			this.Name = "Form1";
			this.Text = "Game Of Anza";
			this.TabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AnzaImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox SearchBox;
		private System.Windows.Forms.Label SearchInstLabel;
		private System.Windows.Forms.Button SearchButton;
		private System.Windows.Forms.ListBox ResultBox;
		private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label AnzaScoreLabel;
		private System.Windows.Forms.PictureBox AnzaImage;
	}
}

