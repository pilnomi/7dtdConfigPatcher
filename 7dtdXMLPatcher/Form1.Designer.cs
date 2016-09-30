﻿namespace _7dtdXMLPatcher
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSteamLocation = new System.Windows.Forms.Label();
            this.lblGameFolder = new System.Windows.Forms.Label();
            this.btnUpdateBiomesXML = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbBiomesAffected = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSteamLocation
            // 
            this.lblSteamLocation.AutoSize = true;
            this.lblSteamLocation.Location = new System.Drawing.Point(125, 20);
            this.lblSteamLocation.Name = "lblSteamLocation";
            this.lblSteamLocation.Size = new System.Drawing.Size(88, 13);
            this.lblSteamLocation.TabIndex = 0;
            this.lblSteamLocation.Text = "lblSteamLocation";
            // 
            // lblGameFolder
            // 
            this.lblGameFolder.AutoSize = true;
            this.lblGameFolder.Location = new System.Drawing.Point(125, 40);
            this.lblGameFolder.Name = "lblGameFolder";
            this.lblGameFolder.Size = new System.Drawing.Size(74, 13);
            this.lblGameFolder.TabIndex = 1;
            this.lblGameFolder.Text = "lblGameFolder";
            // 
            // btnUpdateBiomesXML
            // 
            this.btnUpdateBiomesXML.Location = new System.Drawing.Point(84, 66);
            this.btnUpdateBiomesXML.Name = "btnUpdateBiomesXML";
            this.btnUpdateBiomesXML.Size = new System.Drawing.Size(129, 30);
            this.btnUpdateBiomesXML.TabIndex = 2;
            this.btnUpdateBiomesXML.Text = "Update Biomes XML";
            this.btnUpdateBiomesXML.UseVisualStyleBackColor = true;
            this.btnUpdateBiomesXML.Click += new System.EventHandler(this.btnUpdateBiomesXML_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(208, 137);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(676, 229);
            this.dataGridView1.TabIndex = 3;
            // 
            // lbBiomesAffected
            // 
            this.lbBiomesAffected.FormattingEnabled = true;
            this.lbBiomesAffected.Location = new System.Drawing.Point(12, 137);
            this.lbBiomesAffected.Name = "lbBiomesAffected";
            this.lbBiomesAffected.Size = new System.Drawing.Size(190, 229);
            this.lbBiomesAffected.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Default Steam Install:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Game Location:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 421);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbBiomesAffected);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnUpdateBiomesXML);
            this.Controls.Add(this.lblGameFolder);
            this.Controls.Add(this.lblSteamLocation);
            this.Name = "frmMain";
            this.Text = "7dtd XML Patcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSteamLocation;
        private System.Windows.Forms.Label lblGameFolder;
        private System.Windows.Forms.Button btnUpdateBiomesXML;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckedListBox lbBiomesAffected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

