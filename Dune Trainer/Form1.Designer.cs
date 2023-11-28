namespace Dune_Trainer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.updateTable = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Pointer2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitType2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteInvincible = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Pointer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BuildingType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Health = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddInvincible = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1003, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pointer,
            this.UnitType,
            this.BuildingType,
            this.Health,
            this.AddInvincible});
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1764, 851);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // updateTable
            // 
            this.updateTable.Location = new System.Drawing.Point(12, 909);
            this.updateTable.Name = "updateTable";
            this.updateTable.Size = new System.Drawing.Size(104, 47);
            this.updateTable.TabIndex = 2;
            this.updateTable.Text = "Update";
            this.updateTable.UseVisualStyleBackColor = true;
            this.updateTable.Click += new System.EventHandler(this.updateTable_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pointer2,
            this.UnitType2,
            this.DeleteInvincible});
            this.dataGridView2.Location = new System.Drawing.Point(12, 962);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(1764, 311);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "Atreides",
            "Harkonnen",
            "Ordos",
            "Emperor",
            "Fremen",
            "Smugglers",
            "Mercenaries",
            "Sandworm"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 28);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "Atreides";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Pointer2
            // 
            this.Pointer2.HeaderText = "Pointer";
            this.Pointer2.MinimumWidth = 8;
            this.Pointer2.Name = "Pointer2";
            this.Pointer2.ReadOnly = true;
            this.Pointer2.Width = 150;
            // 
            // UnitType2
            // 
            this.UnitType2.HeaderText = "UnitType";
            this.UnitType2.MinimumWidth = 8;
            this.UnitType2.Name = "UnitType2";
            this.UnitType2.ReadOnly = true;
            this.UnitType2.Width = 150;
            // 
            // DeleteInvincible
            // 
            this.DeleteInvincible.HeaderText = "DeleteInvincible";
            this.DeleteInvincible.MinimumWidth = 8;
            this.DeleteInvincible.Name = "DeleteInvincible";
            this.DeleteInvincible.Width = 150;
            // 
            // Pointer
            // 
            this.Pointer.HeaderText = "Pointer";
            this.Pointer.MinimumWidth = 8;
            this.Pointer.Name = "Pointer";
            this.Pointer.ReadOnly = true;
            this.Pointer.Width = 150;
            // 
            // UnitType
            // 
            this.UnitType.HeaderText = "UnitType";
            this.UnitType.MinimumWidth = 8;
            this.UnitType.Name = "UnitType";
            this.UnitType.Width = 150;
            // 
            // BuildingType
            // 
            this.BuildingType.HeaderText = "BuildingType";
            this.BuildingType.MinimumWidth = 8;
            this.BuildingType.Name = "BuildingType";
            this.BuildingType.Width = 150;
            // 
            // Health
            // 
            this.Health.HeaderText = "Health";
            this.Health.MinimumWidth = 8;
            this.Health.Name = "Health";
            this.Health.Width = 150;
            // 
            // AddInvincible
            // 
            this.AddInvincible.HeaderText = "AddInvincible";
            this.AddInvincible.MinimumWidth = 8;
            this.AddInvincible.Name = "AddInvincible";
            this.AddInvincible.ReadOnly = true;
            this.AddInvincible.Text = "AddInvincible";
            this.AddInvincible.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 1320);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.updateTable);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button updateTable;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pointer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitType2;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteInvincible;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pointer;
        private System.Windows.Forms.DataGridViewComboBoxColumn UnitType;
        private System.Windows.Forms.DataGridViewComboBoxColumn BuildingType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Health;
        private System.Windows.Forms.DataGridViewButtonColumn AddInvincible;
    }
}

