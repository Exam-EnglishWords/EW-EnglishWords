namespace Admin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tbEWord = new TextBox();
            tbUWord = new TextBox();
            button1 = new Button();
            label4 = new Label();
            tbDelete = new TextBox();
            dtnDelete = new Button();
            lvUK = new ListView();
            lvUA = new ListView();
            btnUpdateList = new Button();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 53);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 0;
            label1.Text = "New English word";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(204, 53);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 1;
            label2.Text = "Translate to Ukrainian";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(144, 53);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 2;
            label3.Text = "--->>>";
            // 
            // tbEWord
            // 
            tbEWord.Location = new Point(12, 71);
            tbEWord.Name = "tbEWord";
            tbEWord.Size = new Size(132, 23);
            tbEWord.TabIndex = 3;
            // 
            // tbUWord
            // 
            tbUWord.Location = new Point(204, 71);
            tbUWord.Name = "tbUWord";
            tbUWord.Size = new Size(132, 23);
            tbUWord.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(101, 100);
            button1.Name = "button1";
            button1.Size = new Size(151, 23);
            button1.TabIndex = 5;
            button1.Text = "Add words";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(597, 53);
            label4.Name = "label4";
            label4.Size = new Size(171, 15);
            label4.TabIndex = 6;
            label4.Text = "Enter English word Id for delete";
            // 
            // tbDelete
            // 
            tbDelete.Location = new Point(657, 71);
            tbDelete.Name = "tbDelete";
            tbDelete.Size = new Size(40, 23);
            tbDelete.TabIndex = 7;
            // 
            // dtnDelete
            // 
            dtnDelete.Location = new Point(642, 100);
            dtnDelete.Name = "dtnDelete";
            dtnDelete.Size = new Size(75, 23);
            dtnDelete.TabIndex = 8;
            dtnDelete.Text = "Delete";
            dtnDelete.UseVisualStyleBackColor = true;
            dtnDelete.Click += dtnDelete_Click;
            // 
            // lvUK
            // 
            lvUK.Location = new Point(12, 201);
            lvUK.Name = "lvUK";
            lvUK.Size = new Size(393, 246);
            lvUK.TabIndex = 9;
            lvUK.TileSize = new Size(10, 20);
            lvUK.UseCompatibleStateImageBehavior = false;
            // 
            // lvUA
            // 
            lvUA.Location = new Point(411, 201);
            lvUA.Name = "lvUA";
            lvUA.Size = new Size(377, 246);
            lvUA.TabIndex = 10;
            lvUA.UseCompatibleStateImageBehavior = false;
            // 
            // btnUpdateList
            // 
            btnUpdateList.Location = new Point(288, 172);
            btnUpdateList.Name = "btnUpdateList";
            btnUpdateList.Size = new Size(238, 23);
            btnUpdateList.TabIndex = 11;
            btnUpdateList.Text = "Update lists";
            btnUpdateList.UseVisualStyleBackColor = true;
            btnUpdateList.Click += btnUpdateList_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 183);
            label5.Name = "label5";
            label5.Size = new Size(80, 15);
            label5.TabIndex = 12;
            label5.Text = "English words";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(681, 183);
            label6.Name = "label6";
            label6.Size = new Size(92, 15);
            label6.TabIndex = 13;
            label6.Text = "Ukrainian words";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(btnUpdateList);
            Controls.Add(lvUA);
            Controls.Add(lvUK);
            Controls.Add(dtnDelete);
            Controls.Add(tbDelete);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(tbUWord);
            Controls.Add(tbEWord);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox tbEWord;
        private TextBox tbUWord;
        private Button button1;
        private Label label4;
        private TextBox tbDelete;
        private Button dtnDelete;
        private ListView lvUK;
        private ListView lvUA;
        private Button btnUpdateList;
        private Label label5;
        private Label label6;
    }
}
