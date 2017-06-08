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
			this.HeavenList = new System.Windows.Forms.Label();
			this.HellList = new System.Windows.Forms.Label();
			this.HeavenImg1 = new System.Windows.Forms.PictureBox();
			this.HeavenImg2 = new System.Windows.Forms.PictureBox();
			this.HeavenImg3 = new System.Windows.Forms.PictureBox();
			this.HellImg1 = new System.Windows.Forms.PictureBox();
			this.HellImg2 = new System.Windows.Forms.PictureBox();
			this.HellImg3 = new System.Windows.Forms.PictureBox();
			this.HeavenSt1 = new System.Windows.Forms.Label();
			this.HeavenScore1 = new System.Windows.Forms.Label();
			this.HeavenSt2 = new System.Windows.Forms.Label();
			this.HeavenScore2 = new System.Windows.Forms.Label();
			this.HeavenSt3 = new System.Windows.Forms.Label();
			this.HeavenScore3 = new System.Windows.Forms.Label();
			this.HellSt1 = new System.Windows.Forms.Label();
			this.HellScore1 = new System.Windows.Forms.Label();
			this.HellSt2 = new System.Windows.Forms.Label();
			this.HellScore2 = new System.Windows.Forms.Label();
			this.HellSt3 = new System.Windows.Forms.Label();
			this.HellScore3 = new System.Windows.Forms.Label();
			this.RouteGridView = new System.Windows.Forms.DataGridView();
			this.TabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AnzaImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeavenImg1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeavenImg2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeavenImg3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HellImg1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HellImg2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HellImg3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RouteGridView)).BeginInit();
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
			this.tabPage1.Controls.Add(this.HellScore3);
			this.tabPage1.Controls.Add(this.HellSt3);
			this.tabPage1.Controls.Add(this.HellScore2);
			this.tabPage1.Controls.Add(this.HellSt2);
			this.tabPage1.Controls.Add(this.HellScore1);
			this.tabPage1.Controls.Add(this.HellSt1);
			this.tabPage1.Controls.Add(this.HeavenScore3);
			this.tabPage1.Controls.Add(this.HeavenSt3);
			this.tabPage1.Controls.Add(this.HeavenScore2);
			this.tabPage1.Controls.Add(this.HeavenSt2);
			this.tabPage1.Controls.Add(this.HeavenScore1);
			this.tabPage1.Controls.Add(this.HeavenSt1);
			this.tabPage1.Controls.Add(this.HellImg3);
			this.tabPage1.Controls.Add(this.HellImg2);
			this.tabPage1.Controls.Add(this.HellImg1);
			this.tabPage1.Controls.Add(this.HeavenImg3);
			this.tabPage1.Controls.Add(this.HeavenImg2);
			this.tabPage1.Controls.Add(this.HeavenImg1);
			this.tabPage1.Controls.Add(this.HellList);
			this.tabPage1.Controls.Add(this.HeavenList);
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
			this.tabPage2.Controls.Add(this.RouteGridView);
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
			// HeavenList
			// 
			this.HeavenList.AutoSize = true;
			this.HeavenList.Font = new System.Drawing.Font("D2Coding", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenList.Location = new System.Drawing.Point(107, 231);
			this.HeavenList.Name = "HeavenList";
			this.HeavenList.Size = new System.Drawing.Size(241, 37);
			this.HeavenList.TabIndex = 2;
			this.HeavenList.Text = "앉기 쉬운 Top3";
			// 
			// HellList
			// 
			this.HellList.AutoSize = true;
			this.HellList.Font = new System.Drawing.Font("D2Coding", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellList.Location = new System.Drawing.Point(613, 231);
			this.HellList.Name = "HellList";
			this.HellList.Size = new System.Drawing.Size(273, 37);
			this.HellList.TabIndex = 3;
			this.HellList.Text = "앉기 어려운 Top3";
			// 
			// HeavenImg1
			// 
			this.HeavenImg1.Location = new System.Drawing.Point(105, 314);
			this.HeavenImg1.Name = "HeavenImg1";
			this.HeavenImg1.Size = new System.Drawing.Size(100, 100);
			this.HeavenImg1.TabIndex = 4;
			this.HeavenImg1.TabStop = false;
			// 
			// HeavenImg2
			// 
			this.HeavenImg2.Location = new System.Drawing.Point(115, 445);
			this.HeavenImg2.Name = "HeavenImg2";
			this.HeavenImg2.Size = new System.Drawing.Size(80, 80);
			this.HeavenImg2.TabIndex = 5;
			this.HeavenImg2.TabStop = false;
			// 
			// HeavenImg3
			// 
			this.HeavenImg3.Location = new System.Drawing.Point(119, 559);
			this.HeavenImg3.Name = "HeavenImg3";
			this.HeavenImg3.Size = new System.Drawing.Size(70, 70);
			this.HeavenImg3.TabIndex = 6;
			this.HeavenImg3.TabStop = false;
			// 
			// HellImg1
			// 
			this.HellImg1.Location = new System.Drawing.Point(621, 314);
			this.HellImg1.Name = "HellImg1";
			this.HellImg1.Size = new System.Drawing.Size(100, 100);
			this.HellImg1.TabIndex = 7;
			this.HellImg1.TabStop = false;
			// 
			// HellImg2
			// 
			this.HellImg2.Location = new System.Drawing.Point(631, 445);
			this.HellImg2.Name = "HellImg2";
			this.HellImg2.Size = new System.Drawing.Size(80, 80);
			this.HellImg2.TabIndex = 8;
			this.HellImg2.TabStop = false;
			// 
			// HellImg3
			// 
			this.HellImg3.Location = new System.Drawing.Point(636, 559);
			this.HellImg3.Name = "HellImg3";
			this.HellImg3.Size = new System.Drawing.Size(70, 70);
			this.HellImg3.TabIndex = 9;
			this.HellImg3.TabStop = false;
			// 
			// HeavenSt1
			// 
			this.HeavenSt1.AutoSize = true;
			this.HeavenSt1.Font = new System.Drawing.Font("D2Coding", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenSt1.Location = new System.Drawing.Point(244, 314);
			this.HeavenSt1.Name = "HeavenSt1";
			this.HeavenSt1.Size = new System.Drawing.Size(113, 37);
			this.HeavenSt1.TabIndex = 10;
			this.HeavenSt1.Text = "label1";
			// 
			// HeavenScore1
			// 
			this.HeavenScore1.AutoSize = true;
			this.HeavenScore1.Font = new System.Drawing.Font("D2Coding", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenScore1.Location = new System.Drawing.Point(246, 360);
			this.HeavenScore1.Name = "HeavenScore1";
			this.HeavenScore1.Size = new System.Drawing.Size(84, 28);
			this.HeavenScore1.TabIndex = 11;
			this.HeavenScore1.Text = "label2";
			// 
			// HeavenSt2
			// 
			this.HeavenSt2.AutoSize = true;
			this.HeavenSt2.Font = new System.Drawing.Font("D2Coding", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenSt2.Location = new System.Drawing.Point(245, 445);
			this.HeavenSt2.Name = "HeavenSt2";
			this.HeavenSt2.Size = new System.Drawing.Size(92, 31);
			this.HeavenSt2.TabIndex = 12;
			this.HeavenSt2.Text = "label3";
			// 
			// HeavenScore2
			// 
			this.HeavenScore2.AutoSize = true;
			this.HeavenScore2.Font = new System.Drawing.Font("D2Coding", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenScore2.Location = new System.Drawing.Point(247, 486);
			this.HeavenScore2.Name = "HeavenScore2";
			this.HeavenScore2.Size = new System.Drawing.Size(70, 24);
			this.HeavenScore2.TabIndex = 13;
			this.HeavenScore2.Text = "label4";
			// 
			// HeavenSt3
			// 
			this.HeavenSt3.AutoSize = true;
			this.HeavenSt3.Font = new System.Drawing.Font("D2Coding", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenSt3.Location = new System.Drawing.Point(247, 559);
			this.HeavenSt3.Name = "HeavenSt3";
			this.HeavenSt3.Size = new System.Drawing.Size(70, 24);
			this.HeavenSt3.TabIndex = 14;
			this.HeavenSt3.Text = "label5";
			// 
			// HeavenScore3
			// 
			this.HeavenScore3.AutoSize = true;
			this.HeavenScore3.Font = new System.Drawing.Font("D2Coding", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HeavenScore3.Location = new System.Drawing.Point(247, 592);
			this.HeavenScore3.Name = "HeavenScore3";
			this.HeavenScore3.Size = new System.Drawing.Size(57, 19);
			this.HeavenScore3.TabIndex = 15;
			this.HeavenScore3.Text = "label6";
			// 
			// HellSt1
			// 
			this.HellSt1.AutoSize = true;
			this.HellSt1.Font = new System.Drawing.Font("D2Coding", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellSt1.Location = new System.Drawing.Point(754, 313);
			this.HellSt1.Name = "HellSt1";
			this.HellSt1.Size = new System.Drawing.Size(113, 37);
			this.HellSt1.TabIndex = 16;
			this.HellSt1.Text = "label7";
			// 
			// HellScore1
			// 
			this.HellScore1.AutoSize = true;
			this.HellScore1.Font = new System.Drawing.Font("D2Coding", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellScore1.Location = new System.Drawing.Point(756, 360);
			this.HellScore1.Name = "HellScore1";
			this.HellScore1.Size = new System.Drawing.Size(84, 28);
			this.HellScore1.TabIndex = 17;
			this.HellScore1.Text = "label8";
			// 
			// HellSt2
			// 
			this.HellSt2.AutoSize = true;
			this.HellSt2.Font = new System.Drawing.Font("D2Coding", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellSt2.Location = new System.Drawing.Point(755, 445);
			this.HellSt2.Name = "HellSt2";
			this.HellSt2.Size = new System.Drawing.Size(92, 31);
			this.HellSt2.TabIndex = 18;
			this.HellSt2.Text = "label9";
			// 
			// HellScore2
			// 
			this.HellScore2.AutoSize = true;
			this.HellScore2.Font = new System.Drawing.Font("D2Coding", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellScore2.Location = new System.Drawing.Point(757, 486);
			this.HellScore2.Name = "HellScore2";
			this.HellScore2.Size = new System.Drawing.Size(80, 24);
			this.HellScore2.TabIndex = 19;
			this.HellScore2.Text = "label10";
			// 
			// HellSt3
			// 
			this.HellSt3.AutoSize = true;
			this.HellSt3.Font = new System.Drawing.Font("D2Coding", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellSt3.Location = new System.Drawing.Point(757, 559);
			this.HellSt3.Name = "HellSt3";
			this.HellSt3.Size = new System.Drawing.Size(80, 24);
			this.HellSt3.TabIndex = 20;
			this.HellSt3.Text = "label11";
			// 
			// HellScore3
			// 
			this.HellScore3.AutoSize = true;
			this.HellScore3.Font = new System.Drawing.Font("D2Coding", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.HellScore3.Location = new System.Drawing.Point(757, 592);
			this.HellScore3.Name = "HellScore3";
			this.HellScore3.Size = new System.Drawing.Size(65, 19);
			this.HellScore3.TabIndex = 21;
			this.HellScore3.Text = "label12";
			// 
			// RouteGridView
			// 
			this.RouteGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RouteGridView.Location = new System.Drawing.Point(6, 6);
			this.RouteGridView.Name = "RouteGridView";
			this.RouteGridView.RowTemplate.Height = 23;
			this.RouteGridView.Size = new System.Drawing.Size(531, 648);
			this.RouteGridView.TabIndex = 0;
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
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AnzaImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeavenImg1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeavenImg2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeavenImg3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HellImg1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HellImg2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HellImg3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RouteGridView)).EndInit();
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
		private System.Windows.Forms.PictureBox HellImg3;
		private System.Windows.Forms.PictureBox HellImg2;
		private System.Windows.Forms.PictureBox HellImg1;
		private System.Windows.Forms.PictureBox HeavenImg3;
		private System.Windows.Forms.PictureBox HeavenImg2;
		private System.Windows.Forms.PictureBox HeavenImg1;
		private System.Windows.Forms.Label HellList;
		private System.Windows.Forms.Label HeavenList;
		private System.Windows.Forms.Label HellScore3;
		private System.Windows.Forms.Label HellSt3;
		private System.Windows.Forms.Label HellScore2;
		private System.Windows.Forms.Label HellSt2;
		private System.Windows.Forms.Label HellScore1;
		private System.Windows.Forms.Label HellSt1;
		private System.Windows.Forms.Label HeavenScore3;
		private System.Windows.Forms.Label HeavenSt3;
		private System.Windows.Forms.Label HeavenScore2;
		private System.Windows.Forms.Label HeavenSt2;
		private System.Windows.Forms.Label HeavenScore1;
		private System.Windows.Forms.Label HeavenSt1;
		private System.Windows.Forms.DataGridView RouteGridView;
	}
}

