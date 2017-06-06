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
			this.SuspendLayout();
			// 
			// SearchBox
			// 
			this.SearchBox.Location = new System.Drawing.Point(26, 34);
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new System.Drawing.Size(206, 21);
			this.SearchBox.TabIndex = 0;
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.SearchInstLabel);
			this.Controls.Add(this.SearchBox);
			this.Name = "Form1";
			this.Text = "Game Of Anza";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox SearchBox;
		private System.Windows.Forms.Label SearchInstLabel;
		private System.Windows.Forms.Button SearchButton;
	}
}

