/*
 * Erstellt mit SharpDevelop.
 * Benutzer: 
johannes
 * Datum: 29.12.2010
 * Zeit: 21:39
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace GSharpViewer
{
	partial class frmMain
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Layers");
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnLoad = new System.Windows.Forms.ToolStripButton();
			this.btnPan = new System.Windows.Forms.ToolStripButton();
			this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
			this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
			this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
			this.btnInfo = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnAbout = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabMap = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.mapImage1 = new SharpMap.Forms.MapImage();
			this.tabAttributes = new System.Windows.Forms.TabPage();
			this.dgvAttributes = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cnmRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.cnmRemoveLyr = new System.Windows.Forms.ToolStripMenuItem();
			this.cnmZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabMap.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mapImage1)).BeginInit();
			this.tabAttributes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1,
									this.toolStripStatusLabel3,
									this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 357);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(473, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
			this.toolStripStatusLabel1.Text = "Status";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(97, 17);
			this.toolStripStatusLabel3.Text = "                              ";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btnLoad,
									this.btnPan,
									this.btnZoomIn,
									this.btnZoomOut,
									this.btnFullExtent,
									this.btnInfo,
									this.toolStripSeparator1,
									this.btnAbout});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(473, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnLoad
			// 
			this.btnLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(40, 22);
			this.btnLoad.Text = "Laden";
			this.btnLoad.Click += new System.EventHandler(this.toolStripbtnLoad_Click);
			// 
			// btnPan
			// 
			this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPan.Name = "btnPan";
			this.btnPan.Size = new System.Drawing.Size(29, 22);
			this.btnPan.Text = "Pan";
			this.btnPan.Click += new System.EventHandler(this.BtnPanClick);
			// 
			// btnZoomIn
			// 
			this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(47, 22);
			this.btnZoomIn.Text = "ZoomIn";
			this.btnZoomIn.Click += new System.EventHandler(this.BtnZoomInClick);
			// 
			// btnZoomOut
			// 
			this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.Size = new System.Drawing.Size(55, 22);
			this.btnZoomOut.Text = "ZoomOut";
			this.btnZoomOut.Click += new System.EventHandler(this.BtnZoomOutClick);
			// 
			// btnFullExtent
			// 
			this.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnFullExtent.Name = "btnFullExtent";
			this.btnFullExtent.Size = new System.Drawing.Size(59, 22);
			this.btnFullExtent.Text = "FullExtent";
			this.btnFullExtent.Click += new System.EventHandler(this.BtnFullExtentClick);
			// 
			// btnInfo
			// 
			this.btnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnInfo.Name = "btnInfo";
			this.btnInfo.Size = new System.Drawing.Size(31, 22);
			this.btnInfo.Text = "Info";
			this.btnInfo.Click += new System.EventHandler(this.BtnInfoClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnAbout
			// 
			this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
			this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(40, 22);
			this.btnAbout.Text = "About";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(473, 332);
			this.panel1.TabIndex = 2;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabMap);
			this.tabControl1.Controls.Add(this.tabAttributes);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(473, 332);
			this.tabControl1.TabIndex = 0;
			// 
			// tabMap
			// 
			this.tabMap.Controls.Add(this.splitContainer1);
			this.tabMap.Location = new System.Drawing.Point(4, 22);
			this.tabMap.Name = "tabMap";
			this.tabMap.Padding = new System.Windows.Forms.Padding(3);
			this.tabMap.Size = new System.Drawing.Size(465, 306);
			this.tabMap.TabIndex = 0;
			this.tabMap.Text = "Karte";
			this.tabMap.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.mapImage1);
			this.splitContainer1.Size = new System.Drawing.Size(459, 300);
			this.splitContainer1.SplitterDistance = 112;
			this.splitContainer1.TabIndex = 1;
			// 
			// treeView1
			// 
			this.treeView1.AllowDrop = true;
			this.treeView1.CheckBoxes = true;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeView1.HideSelection = false;
			this.treeView1.Indent = 25;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			treeNode1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			treeNode1.Checked = true;
			treeNode1.ForeColor = System.Drawing.Color.Black;
			treeNode1.Name = "Layers";
			treeNode1.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			treeNode1.Text = "Layers";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
									treeNode1});
			this.treeView1.Size = new System.Drawing.Size(112, 300);
			this.treeView1.TabIndex = 0;
			// 
			// mapImage1
			// 
			this.mapImage1.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
			this.mapImage1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.mapImage1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.mapImage1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapImage1.FineZoomFactor = 10;
			this.mapImage1.Location = new System.Drawing.Point(0, 0);
			this.mapImage1.Name = "mapImage1";
			this.mapImage1.PanOnClick = false;
			this.mapImage1.QueryLayerIndex = -1;
			this.mapImage1.Size = new System.Drawing.Size(343, 300);
			this.mapImage1.TabIndex = 0;
			this.mapImage1.TabStop = false;
			this.mapImage1.WheelZoomMagnitude = 2;
			this.mapImage1.ZoomOnDblClick = false;
			this.mapImage1.MapQueriedDataSet += new SharpMap.Forms.MapImage.MapQueryDataSetHandler(this.MapImage1MapQueriedDataSet);
			this.mapImage1.SizeChanged += new System.EventHandler(this.MapImage1SizeChanged);
			// 
			// tabAttributes
			// 
			this.tabAttributes.Controls.Add(this.dgvAttributes);
			this.tabAttributes.Location = new System.Drawing.Point(4, 22);
			this.tabAttributes.Name = "tabAttributes";
			this.tabAttributes.Padding = new System.Windows.Forms.Padding(3);
			this.tabAttributes.Size = new System.Drawing.Size(465, 306);
			this.tabAttributes.TabIndex = 1;
			this.tabAttributes.Text = "Attribute";
			this.tabAttributes.UseVisualStyleBackColor = true;
			// 
			// dgvAttributes
			// 
			this.dgvAttributes.AllowUserToAddRows = false;
			this.dgvAttributes.AllowUserToDeleteRows = false;
			this.dgvAttributes.AllowUserToOrderColumns = true;
			this.dgvAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvAttributes.Cursor = System.Windows.Forms.Cursors.Default;
			this.dgvAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvAttributes.Location = new System.Drawing.Point(3, 3);
			this.dgvAttributes.Name = "dgvAttributes";
			this.dgvAttributes.ReadOnly = true;
			this.dgvAttributes.ShowEditingIcon = false;
			this.dgvAttributes.Size = new System.Drawing.Size(459, 300);
			this.dgvAttributes.TabIndex = 0;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.cnmRemoveAll,
									this.cnmRemoveLyr,
									this.cnmZoomToLayer});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(144, 70);
			// 
			// cnmRemoveAll
			// 
			this.cnmRemoveAll.Enabled = false;
			this.cnmRemoveAll.Name = "cnmRemoveAll";
			this.cnmRemoveAll.Size = new System.Drawing.Size(143, 22);
			this.cnmRemoveAll.Text = "Remove All";
			// 
			// cnmRemoveLyr
			// 
			this.cnmRemoveLyr.Enabled = false;
			this.cnmRemoveLyr.Name = "cnmRemoveLyr";
			this.cnmRemoveLyr.Size = new System.Drawing.Size(143, 22);
			this.cnmRemoveLyr.Text = "Remove Layer";
			// 
			// cnmZoomToLayer
			// 
			this.cnmZoomToLayer.Enabled = false;
			this.cnmZoomToLayer.Name = "cnmZoomToLayer";
			this.cnmZoomToLayer.Size = new System.Drawing.Size(143, 22);
			this.cnmZoomToLayer.Text = "Zoom to Layer";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "ESRI Shapefile|*.shp";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(473, 379);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "frmMain";
			this.Text = "G#.Viewer";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabMap.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mapImage1)).EndInit();
			this.tabAttributes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripMenuItem cnmZoomToLayer;
		private System.Windows.Forms.ToolStripMenuItem cnmRemoveLyr;
		private System.Windows.Forms.ToolStripMenuItem cnmRemoveAll;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripButton btnAbout;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.DataGridView dgvAttributes;
		private System.Windows.Forms.ToolStripButton btnInfo;
		private System.Windows.Forms.ToolStripButton btnFullExtent;
		private System.Windows.Forms.ToolStripButton btnZoomOut;
		private System.Windows.Forms.ToolStripButton btnZoomIn;
		private System.Windows.Forms.ToolStripButton btnPan;
		private System.Windows.Forms.ToolStripButton btnLoad;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private SharpMap.Forms.MapImage mapImage1;
		private System.Windows.Forms.TabPage tabAttributes;
		private System.Windows.Forms.TabPage tabMap;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		
	}
}
